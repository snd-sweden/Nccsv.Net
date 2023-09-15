using NccsvConverter.NccsvParser.Models;

namespace NccsvConverter.NccsvParser.Helpers
{
    public class NccsvVerifierMethods
    {

        // Returns true if file has correct .nccsv extension.
        public static bool CheckNccsvExtension(string filePath)
        {
            if (filePath.EndsWith(".nccsv"))
                return true;
            else
                return false;
        }


        // Returns true if file is not empty.
        public static bool CheckFileForContent(string[] lines)
        {
            if (lines.Length > 0) 
                return true;
            else
                return false;
        }


        // Returns true if *GLOBAL* "Conventions" attribute exist.
        // Note: if Conventions have to be first row, maybe check for that as well.
        public static bool CheckGlobalConventions(List<string[]> globalProperties)
        {
            //if (globalProperties
            //    .Exists(r => r[1].Equals("Conventions")))
            //{
            //    return true;
            //}

            foreach (var globalProperty in globalProperties)
            {
                if (globalProperty[1] == "Conventions")
                    return true;
            }

            return false;
        }


        // Returns true if Global Conventions include a reference to NCCSV.
        // Note: Will work as long as Conventions is first row -- maybe change to check in same row as
        // conventions to be sure?
        public static bool CheckNccsvVerification(List<string[]> potentialNccsv)
        {
            //if (potentialNccsv[0]
            //    .ToList()
            //    .Exists(s => s.Contains("NCCSV")))
            //{
            //    return true;
            //}

            foreach (var s in potentialNccsv[0])
            {
                if (s.Contains("NCCSV"))
                    return true;
            }

            return false;
        }

        
        // Checks for *END_METADATA* that must exist at end of metadata section.
        // Returns true if found.
        // Note: This only checks that *END_METADATA* exists *somewhere* in the file.
        public static bool CheckForMetaDataEndTag(List<string[]> csv)
        {
            //if (csv
            //    .Exists(r => r[0].Equals("*END_METADATA*")))
            //{
            //    return true;
            //}

            foreach (var row in csv)
            {
                if (row[0] == "*END_METADATA*")
                    return true;
            }

            return false;
        }


        // Checks for *END_DATA* that must exist at end of data section.
        // Returns true if found.
        // Note: This only checks that *END_DATA* exists *somewhere* in the file.
        public static bool CheckForDataEndTag(List<string[]> csv)
        {
            //if (csv
            //    .Exists(r => r[0].Equals("*END_DATA*")))
            //{
            //    return true;
            //}

            foreach (var row in csv)
            {
                if (row[0] == "*END_DATA*")
                    return true;
            }

            return false;
        }


        // Attributes must have value. Returns true if the property list
        // is longer than 2 columns.
        public static bool CheckAttributeForValue(List<string[]> properties)
        {
            //if (properties
            //    .All(r => r.Length > 2))
            //{
            //    return true;
            //}

            // We check that rows are more than 2 columns because values resides on column [2] to [n]
            foreach (var row in properties)
            {
                if (row.Length > 2)
                    return true;
            }

            return false;
        }


        // Returns true if each non-scalar variable have a data type.
        // TODO: also check if the data type is acceptable?
        public static bool CheckVariableForDataType(Variable variable)
        {
            if (variable.DataType != null 
                && variable.DataType != string.Empty)
                return true;
            else
                return false;
        }


        // Check that *SCALAR* variable doesn't have data for the variable in the data section
        public static bool CheckScalarVariable()
        {
            // TODO: Check scalar stuff here!

            throw new NotImplementedException();
        }


        // Data must be of the data type specified by the metadata.
        // Note: this is only necessary if we store data values as their datatypes.
        public static bool CheckDataForDataType(DataSet dataSet)
        {
            // TODO: compare data values to variable data type

            throw new NotImplementedException();
        }


        // Data values, if not string, should not have suffixes.
        public static bool CheckDataValuesForSuffix(DataSet dataSet)
        {
            // TODO: if dataType is not string, value should not have suffix
            // Note: will be noticed in TryParse?
            throw new NotImplementedException();
        }


        // Returns true if there is whitespace before or after values.
        public static bool CheckDataValuesForSpace(List<string[]> data)
        {
            //if(dataSet.Data
            //    .Any(r => r
            //    .Any(v => v.EndsWith(" ") 
            //        || v.StartsWith(" "))))
            //{
            //    return true;
            //}

            foreach (var dataRow in data)
            {
                foreach (var value in dataRow)
                {
                    if (value.EndsWith(" ")
                        || value.StartsWith(" "))
                        return true;
                }
            }

            return false;
        }


        // should this be each row or just check headers (as it does now)?
        // Returns true if the row of data have the same number of values as
        // the list of variable names.
        public static bool CheckNumberOfValuesToVariables(DataSet dataSet)
        {
            // Numbers of headers should be same as number of variables
            if (dataSet.Data[0].Length == dataSet.Variables.Count)
                return true;
            else
                return false;
        }


        // Returns true if suffix is found for numeric data type other than L or uL
        public static bool CheckDataForIllegalSuffix(string dataType, string value)
        {
            if(dataType != "ulong" && dataType != "long" 
                && dataType != "String" && dataType != "char")
            {
                if (value.All(char.IsDigit))
                    return true;
                else
                    return false;
            }

            return false;
        }
    }
}