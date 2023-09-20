using NccsvConverter.NccsvParser.Models;

namespace NccsvConverter.NccsvParser.Helpers
{
    public class NccsvVerifierMethods
    {

        // Returns true if file has .nccsv extension.
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


        // Returns true if the *GLOBAL* "Conventions" attribute is first row.
        public static bool CheckGlobalConventions(List<string[]> globalAttributes)
        {
            if (globalAttributes[0][1] == "Conventions")
                return true;
            else
                return false;
        }


        // Returns true if Global Conventions include a reference to NCCSV.
        public static bool CheckNccsvVerification(List<string[]> potentialNccsv)
        {
            foreach (var s in potentialNccsv[0])
            {
                if (s.Contains("NCCSV"))
                    return true;
            }

            return false;
        }

        
        // Returns true if Metadata end tag comes before data end tag.
        // Note: This only checks the order. If *END_METADATA* does not exist, this will
        // still return true.
        public static bool CheckOrderOfEndTags(List<string[]> potentialNccsv)
        {   
            int indexMetaData = 0;
            int indexData = 0;

            foreach (var row in potentialNccsv)
            {
                if (row[0] == "*END_METADATA*")
                    indexMetaData = potentialNccsv.IndexOf(row);
                else if (row[0] == "*END_DATA*")
                    indexData = potentialNccsv.IndexOf(row);
            }

            if (indexData > indexMetaData)
                return true;
            else
                return false;
        }


        // Checks for *END_METADATA* that must exist at end of metadata section.
        // Returns true if found.
        // Note: This only checks that *END_METADATA* exists *somewhere* in the file.
        public static bool CheckForMetaDataEndTag(List<string[]> potentialNccsv)
        {
            foreach (var row in potentialNccsv)
            {
                if (row[0] == "*END_METADATA*")
                    return true;
            }

            return false;
        }


        // Checks for *END_DATA* that must exist at end of data section.
        // Returns true if found.
        // Note: This only checks that *END_DATA* exists *somewhere* in the file.
        public static bool CheckForDataEndTag(List<string[]> potentialNccsv)
        {
            foreach (var row in potentialNccsv)
            {
                if (row[0] == "*END_DATA*")
                    return true;
            }

            return false;
        }


        // Attributes must have value. Returns true if the variableMetaData list
        // rows has more than 2 columns.
        public static bool CheckAttributeForValue(List<string[]> variableMetaData)
        {
            // We check that rows are more than 2 columns because values resides on column [2] to [n]
            foreach (var row in variableMetaData)
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
        // Note: How we check for this depends on how/if we store data types
        public static bool CheckDataForDataType(DataSet dataSet)
        {
            // TODO: compare data values to variable data type
            // Something like: (Note: this solution only works if Variables
            // is in the same order as data headers)
            //for (int i = 0; i < dataSet.Data.Length; i++)
            //{
            //    for (int j = 0; j < dataSet.Data[i].Length; j++)
            //    {
            //        if (dataSet.Data[i][j].DataType == dataSet.Variables[j].DataType)
            //            return true;
            //        else
            //            return false;
            //    }
            //}

            throw new NotImplementedException();
        }


        // Returns true if there is whitespace before or after values.
        public static bool CheckDataValuesForSpace(List<string[]> data)
        {
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
            if(dataType != "ulong" 
                && dataType != "long" 
                && dataType != "String" 
                && dataType != "char")
            {
                if (value.All(char.IsDigit))
                    return false;
                else
                    return true;
            }

            return false;
        }
    }
}