using NccsvConverter.NccsvParser.Models;
using NccsvDataSet = NccsvConverter.NccsvParser.Models.DataSet;
using NccsvConverter.NccsvSerializer.SchemaDatasetJsonSerializer.Models;
using Schema.NET;
using SchemaDataSet = Schema.NET.Dataset;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NccsvConverter.NccsvSerializer.SchemaDatasetJsonSerializer;

public class Serializer
{
    // Given a parsed and validated (Nccsv)DataSet from NccsvParser, ToJson returns
    // a json-ld string with Schema.org vocabulary.
    public static string ToJson(NccsvDataSet nccsvDataSet)
    {
        SchemaDataSet schemaDataSet = ToSchemaDataSet(nccsvDataSet);
        var json = JsonSerializer.Serialize(schemaDataSet, new JsonSerializerOptions()
            {WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault});
        return json;
    }


    // Given a parsed and validated (Nccsv)DataSet from NccsvParser, ToSchemaDataSet maps certain properties
    // to Schema.org vocabulary properties and returns a Schema.NET (Schema)Dataset object. 
    private static SchemaDataSet ToSchemaDataSet(NccsvDataSet nccsvDataSet)
    {
        var schemaDataSet = new SchemaDataSet
        {
            Name = nccsvDataSet.MetaData.Title,
            Description = nccsvDataSet.MetaData.Summary,
            License = GetLicense(nccsvDataSet),
            DateCreated = GetDateCreated(nccsvDataSet),
            Keywords = GetKeywords(nccsvDataSet),
            Creator = GetCreator(nccsvDataSet),
            VariableMeasured = GetVariables(nccsvDataSet),
            SpatialCoverage = GetSpatialCoverage(nccsvDataSet),
            Identifier = GetIdentifier(nccsvDataSet)
        };

        return schemaDataSet;
    }


    // Checks a nccsv dataset for the "license" global attribute, and if found, returns either as a
    // Uri or, if not possible, as a Schema.NET CreativeWork text.
    // If not found, returns the Values<ICreativeWork,Uri> default value.
    private static Values<ICreativeWork,Uri> GetLicense(NccsvDataSet nccsvDataSet)
    {
        var result = Uri.TryCreate(nccsvDataSet.MetaData
                .GlobalAttributes.FirstOrDefault(g => g
                .Key.Equals("license"))
                .Value, 
            UriKind.Absolute, 
            out Uri? uri);
        if (result)
            return uri;
        else if (nccsvDataSet.MetaData.GlobalAttributes.ContainsKey("license"))
            return new CreativeWork {Text = nccsvDataSet.MetaData
                .GlobalAttributes.FirstOrDefault(g => g
                .Key.Equals("license"))
                .Value};
        else
            return new Values<ICreativeWork, Uri>();
    }


    // Checks a nccsv dataset for the "date_created" global attribute, and if found, returns
    // as a DateTime. If not found, returns null.
    private static DateTime? GetDateCreated(NccsvDataSet nccsvDataSet)
    {
        var result = DateTime.TryParse(nccsvDataSet.MetaData
                .GlobalAttributes.FirstOrDefault(g => g
                .Key.Equals("date_created"))
                .Value, 
            out DateTime dateTime);
        if (result)
            return dateTime;
        else
            return null;
    }


    // Checks a nccsv dataset for the "keywords" global attribute, and if found, returns as a string.
    // If not found, returns string.Empty.
    private static string GetKeywords(NccsvDataSet nccsvDataSet)
    {
        return nccsvDataSet.MetaData
            .GlobalAttributes.FirstOrDefault(g => g
            .Key.Equals("keywords"))
            .Value;
    }


    // Checks a nccsv dataset for the "creator" global attributes, and if found, checks type to establish if
    // the return type will be Schema.NET Organization or Person. If no "creator_type" attribute exists,
    // type will be assumed to be Person. If not found, returns the Values<IOrganization, IPerson> default value.
    private static Values<IOrganization,IPerson> GetCreator(NccsvDataSet nccsvDataSet)
    {
        if (nccsvDataSet.MetaData
            .GlobalAttributes.Any(g => g
            .Key.StartsWith("creator")))
        {
            if (nccsvDataSet.MetaData
                .GlobalAttributes.FirstOrDefault(g => g
                .Key.Equals("creator_type"))
                .Value.Equals("institution"))
            {
                return new Organization
                {
                    Name = nccsvDataSet.MetaData
                        .GlobalAttributes.FirstOrDefault(g => g
                        .Key.Equals("creator_name"))
                        .Value,
                    Email = nccsvDataSet.MetaData
                        .GlobalAttributes.FirstOrDefault(g => g
                        .Key.Equals("creator_email"))
                        .Value,
                    Url = GetCreatorUrl(nccsvDataSet)
                };
            }

            return new Person
            {
                Name = nccsvDataSet.MetaData
                    .GlobalAttributes.FirstOrDefault(g => g
                    .Key.Equals("creator_name"))
                    .Value,
                Email = nccsvDataSet.MetaData
                    .GlobalAttributes.FirstOrDefault(g => g
                    .Key.Equals("creator_email"))
                    .Value,
                Url = GetCreatorUrl(nccsvDataSet),
                Affiliation = GetCreatorAffiliation(nccsvDataSet)
            };
        }
            
        return new Values<IOrganization, IPerson>();
    }


    // Checks a nccsv dataset for the "creator_url" global attribute, and if found,
    // returns as a Uri. If not found, returns null.
    private static Uri? GetCreatorUrl(NccsvDataSet nccsvDataSet)
    {
        var result = Uri.TryCreate(nccsvDataSet.MetaData
                .GlobalAttributes.FirstOrDefault(g => g
                .Key.Equals("creator_url"))
                .Value,
            UriKind.Absolute,
            out Uri? uri);
        if (result)
            return uri;
        else
            return null;
    }


    // Checks a nccsv dataset for the "creator_institution" or "institution" global attribute, and if found,
    // returns as a Schema.NET Organization. If not found, returns the Organization default value.
    private static Organization? GetCreatorAffiliation(NccsvDataSet nccsvDataSet)
    {
        if (nccsvDataSet.MetaData.GlobalAttributes.ContainsKey("creator_institution"))
        {
            return new Organization
            {
                Name = nccsvDataSet.MetaData
                    .GlobalAttributes.FirstOrDefault(g => g
                    .Key.Equals("creator_institution"))
                    .Value
            };
        }
        else if (nccsvDataSet.MetaData.GlobalAttributes.ContainsKey("institution"))
        {
            return new Organization
            {
                Name = nccsvDataSet.MetaData
                    .GlobalAttributes.FirstOrDefault(g => g
                    .Key.Equals("institution"))
                    .Value
            };
        }

        return null;
    }

    
    // From the nccsv dataset variables, returns as a list of ExtendedPropertyValue
    // (Schema.NET PropertyValue extended to include AdditionalProperty property)
    private static List<IPropertyValue> GetVariables(NccsvDataSet nccsvDataSet)
    {
        var values = new List<IPropertyValue>();

        foreach (var variable in nccsvDataSet.MetaData.Variables) 
        {
            values.Add(new ExtendedPropertyValue
            {
                Name = variable.VariableName,
                AlternateName = GetAlternateVariableName(variable),
                UnitText = GetVariableUnits(variable),
                AdditionalProperty = new PropertyValue
                {
                    Name = "dataType",
                    Value = variable.VariableDataType
                }
            });
        }

        return values;
    }


    // Checks a nccsv dataset variable for the "standard_name" attribute, and if found,
    // returns as a string. If not found, returns null.
    private static string GetAlternateVariableName(Variable variable)
    {
        if (variable.Attributes.Any(a => a.Key.Equals("standard_name")))
            return variable
                .Attributes.FirstOrDefault(a => a
                .Key.Equals("standard_name"))
                .Value[0];
        else
            return null;
    }


    // Checks a nccsv dataset variable for the "units" attribute, and if found,
    // returns as a string. If not found, returns null.
    private static string GetVariableUnits(Variable variable)
    {
        if (variable.Attributes.Any(a => a.Key.Equals("units")))
            return variable
                .Attributes.FirstOrDefault(a => a
                .Key.Equals("units"))
                .Value[0];
        else
            return null;
    }


    // Checks a nccsv dataset for the "geospatial" global attributes, and if found,
    // returns a Schema.NET Place with latitude and longitude values. If not found, returns null.
    private static Place? GetSpatialCoverage(NccsvDataSet nccsvDataSet)
    {
        if (nccsvDataSet.MetaData.GlobalAttributes.ContainsKey("geospatial_lat_min")
            && nccsvDataSet.MetaData.GlobalAttributes.ContainsKey("geospatial_lon_min")
            && nccsvDataSet.MetaData.GlobalAttributes.ContainsKey("geospatial_lat_max")
            && nccsvDataSet.MetaData.GlobalAttributes.ContainsKey("geospatial_lon_max"))
        {
            return new Place
            {
                Geo = new GeoShape
                {
                    Box = nccsvDataSet.MetaData.GlobalAttributes.FirstOrDefault(g => g.Key.Equals("geospatial_lat_min")).Value + " " +
                    nccsvDataSet.MetaData.GlobalAttributes.FirstOrDefault(g => g.Key.Equals("geospatial_lon_min")).Value + " " +
                    nccsvDataSet.MetaData.GlobalAttributes.FirstOrDefault(g => g.Key.Equals("geospatial_lat_max")).Value + " " +
                    nccsvDataSet.MetaData.GlobalAttributes.FirstOrDefault(g => g.Key.Equals("geospatial_lon_max")).Value
                }
            };
        }

        return null;
    }


    // Checks a nccsv dataset for the "id" global attribute, and if found,
    // returns as a string. If not found, returns string.Empty.
    private static string GetIdentifier(NccsvDataSet nccsvDataSet)
    {
        return nccsvDataSet.MetaData
                .GlobalAttributes.FirstOrDefault(g => g
                .Key.Equals("id"))
                .Value;
    }
}