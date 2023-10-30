using NccsvConverter.NccsvParser.Models;
using NccsvConverter.NccsvSerializer.SchemaDatasetJsonSerializer.Models;
using Schema.NET;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NccsvConverter.NccsvSerializer.SchemaDatasetJsonSerializer;

public class Serializer
{
    //What is output? string, file?
    public static string ToJson(DataSet dataSet)
    {
        Dataset schemaDataSet = ToSchemaDataSet(dataSet);
        var json = JsonSerializer.Serialize(schemaDataSet, new JsonSerializerOptions()
            {WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault});
        Console.WriteLine(json); // This is for testing! Feel free to delete anytime
        return json;
    }


    private static Dataset ToSchemaDataSet(DataSet dataSet)
    {
        var schemaDataSet = new Dataset
        {
            Name = dataSet.MetaData.Title,
            Description = dataSet.MetaData.Summary,
            License = GetLicense(dataSet),
            DateCreated = GetDateCreated(dataSet),
            Keywords = GetKeywords(dataSet),
            Creator = GetCreator(dataSet),
            VariableMeasured = GetVariables(dataSet),
            SpatialCoverage = GetSpatialCoverage(dataSet),
            Identifier = GetIdentifier(dataSet)
        };

        return schemaDataSet;
    }


    public static Values<ICreativeWork,Uri> GetLicense(DataSet dataSet)
    {
        var result = Uri.TryCreate(dataSet.MetaData
                .GlobalAttributes.FirstOrDefault(g => g
                .Key.Equals("license"))
                .Value, 
            UriKind.Absolute, 
            out Uri? uri);
        if (result)
            return uri;
        else if (dataSet.MetaData.GlobalAttributes.ContainsKey("license"))
            return new CreativeWork {Text = dataSet.MetaData
                .GlobalAttributes.FirstOrDefault(g => g
                .Key.Equals("license"))
                .Value};
        else
            return new Values<ICreativeWork, Uri>();
    }


    public static DateTime? GetDateCreated(DataSet dataSet)
    {
        var result = DateTime.TryParse(dataSet.MetaData
                .GlobalAttributes.FirstOrDefault(g => g
                .Key.Equals("date_created"))
                .Value, 
            out DateTime dateTime);
        if (result)
            return dateTime;
        else
            return null;
    }


    public static string GetKeywords(DataSet dataSet)
    {
        return dataSet.MetaData
            .GlobalAttributes.FirstOrDefault(g => g
            .Key.Equals("keywords"))
            .Value;
    }


    public static Values<IOrganization,IPerson> GetCreator(DataSet dataSet)
    {
        if (dataSet.MetaData
            .GlobalAttributes.Any(g => g
            .Key.StartsWith("creator")))
        {
            if (dataSet.MetaData
                .GlobalAttributes.FirstOrDefault(g => g
                .Key.Equals("creator_type"))
                .Value.Equals("institution"))
            {
                return new Organization
                {
                    Name = dataSet.MetaData
                        .GlobalAttributes.FirstOrDefault(g => g
                        .Key.Equals("creator_name"))
                        .Value,
                    Email = dataSet.MetaData
                        .GlobalAttributes.FirstOrDefault(g => g
                        .Key.Equals("creator_email"))
                        .Value,
                    Url = GetCreatorUrl(dataSet)
                };
            }

            return new Person
            {
                Name = dataSet.MetaData
                    .GlobalAttributes.FirstOrDefault(g => g
                    .Key.Equals("creator_name"))
                    .Value,
                Email = dataSet.MetaData
                    .GlobalAttributes.FirstOrDefault(g => g
                    .Key.Equals("creator_email"))
                    .Value,
                Url = GetCreatorUrl(dataSet),
                Affiliation = GetCreatorAffiliation(dataSet)
            };
        }
            
        return new Values<IOrganization, IPerson>();
    }


    public static Uri? GetCreatorUrl(DataSet dataSet)
    {
        var result = Uri.TryCreate(dataSet.MetaData
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


    public static Organization? GetCreatorAffiliation(DataSet dataSet)
    {
        if (dataSet.MetaData.GlobalAttributes.ContainsKey("creator_institution"))
        {
            return new Organization
            {
                Name = dataSet.MetaData
                    .GlobalAttributes.FirstOrDefault(g => g
                    .Key.Equals("creator_institution"))
                    .Value
            };
        }
        else if (dataSet.MetaData.GlobalAttributes.ContainsKey("institution"))
        {
            return new Organization
            {
                Name = dataSet.MetaData
                    .GlobalAttributes.FirstOrDefault(g => g
                    .Key.Equals("institution"))
                    .Value
            };
        }

        return null;
    }

    
    public static List<IPropertyValue> GetVariables(DataSet dataSet)
    {
        var values = new List<IPropertyValue>();

        foreach (var variable in dataSet.MetaData.Variables) 
        {
            values.Add(new ExtendedPropertyValue
            {
                Name = variable.VariableName,
                AlternateName = GetAlternateVariableName(variable),
                UnitText = GetVariableUnits(variable),
                AdditionalProperty = new PropertyValue
                {
                    Name = "dataType",
                    Value = variable.DataType
                }
            });
        }

        return values;
    }


    public static string GetAlternateVariableName(Variable variable)
    {
        if (variable.Attributes.Any(a => a.Key.Equals("standard_name")))
            return variable
                .Attributes.FirstOrDefault(a => a
                .Key.Equals("standard_name"))
                .Value[0];
        else
            return null;
    }


    public static string GetVariableUnits(Variable variable)
    {
        if (variable.Attributes.Any(a => a.Key.Equals("units")))
            return variable
                .Attributes.FirstOrDefault(a => a
                .Key.Equals("units"))
                .Value[0];
        else
            return null;
    }


    public static Place? GetSpatialCoverage(DataSet dataSet)
    {
        if (dataSet.MetaData.GlobalAttributes.ContainsKey("geospatial_lat_min")
            && dataSet.MetaData.GlobalAttributes.ContainsKey("geospatial_lon_min")
            && dataSet.MetaData.GlobalAttributes.ContainsKey("geospatial_lat_max")
            && dataSet.MetaData.GlobalAttributes.ContainsKey("geospatial_lon_max"))
        {
            return new Place
            {
                Geo = new GeoShape
                {
                    Box = dataSet.MetaData.GlobalAttributes.FirstOrDefault(g => g.Key.Equals("geospatial_lat_min")).Value + " " +
                    dataSet.MetaData.GlobalAttributes.FirstOrDefault(g => g.Key.Equals("geospatial_lon_min")).Value + " " +
                    dataSet.MetaData.GlobalAttributes.FirstOrDefault(g => g.Key.Equals("geospatial_lat_max")).Value + " " +
                    dataSet.MetaData.GlobalAttributes.FirstOrDefault(g => g.Key.Equals("geospatial_lon_max")).Value
                }
            };
        }

        return null;
    }


    private static string GetIdentifier(DataSet dataSet)
    {
        return dataSet.MetaData
                .GlobalAttributes.FirstOrDefault(g => g
                .Key.Equals("id"))
                .Value;
    }
}