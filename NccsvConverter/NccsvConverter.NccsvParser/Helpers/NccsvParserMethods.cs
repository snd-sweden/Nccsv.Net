
using NccsvConverter.NccsvParser.Models;
using System.Text.Json.Serialization.Metadata;

namespace NccsvConverter.NccsvParser.Helpers
{
    public class NccsvParserMethods
    {
        // read file = new dataset

        //finds global properties and stores them without *GLOBAL*-tag in a string array
        public static List<string[]> FindGlobalProperties(List<string[]> csv)
        {
            var globalProps = new List<string[]>();
            // check for *global*, disregard others
            foreach (var stringArray in csv)
            {
                if (stringArray[0] == "*GLOBAL*")
                {
                    if (stringArray.Length < 3)
                    {
                        string propValue = "";
                        for (int i = 1; i < stringArray.Length; i++)
                        {
                            propValue += stringArray[i + 1];
                        }

                        globalProps.Add(new[] { stringArray[1], propValue });
                    }

                    else
                    {
                        globalProps.Add(new[] { stringArray[1], stringArray[2] });
                    }
                }
            }

            return globalProps;

        }


        public static void AddGlobalProperties(string file)
        {
            // add to globalprops for dataset, in dictionary<[1],[2]>

        }


        // Constructs a list of properties where each property is represented as a string array where [0]
        // is the variable name, [1] is the attribute name and [2] to [n] is the values. Does not include
        // global properties as they are collected in FindGlobalProperties.
        public static List<string[]> FindProperties (List<string[]> csv) 
        {
            var properties = new List<string[]>();
            
            foreach (var line in csv)
            {
                // disgregard global properties as they are collected in FindGlobalProperties
                if (line[0].Contains("*GLOBAL*"))
                {
                    continue;
                }

                // end collection of properties at metadata end tag (needs to exist)
                if (line[0].Contains("*END_METADATA*"))
                {
                    break;
                }

                 // add to properties
                 properties.Add(line);

            }

            return properties;
        }


        public static bool CheckIfVariableExists(string file)
        {
            // take variable name
            // check if variable exists
            return false;
        }

        // if not->create new variable

        public static void CreateVariable(string file)
        {
            // create new variable object
            // add to dataset
        }


        // 
        public static void SetVariableDataType(string[] properties, Variable variable)
        {
            // ta in string[] eller List<string[]> ?

            // find *DATA_TYPE* tag
            if (properties[1] == "*DATA_TYPE*")
            {
                // set DataType prop to *data_type* value
                variable.DataType = properties[2];
            }
        }


        // Adds string array of properties to variable as a dictionary where
        // [1] is the attribute name and [2] to [n] is the attribute values
        public static void AddProperties(List<string[]> variableProperties, Variable variable)
        {
            foreach (var properties in variableProperties)
            {
                // if *DATA_TYPE* tag
                if (properties[1] == "*DATA_TYPE*")
                {
                    return;
                }

                var attributeName = properties[1];
                List<string> values = new List<string>();

                for (int i = 2; i < properties.Length; i++)
                {
                    values.Add(properties[i]);
                }

                // add to Properties as <[1], List<[2]-[n]>>
                variable.Properties.Add(attributeName, values);

            }
        }



        public static void FindData(string file)
        {
            // until *end_data* tag
            // csvhelper?
        }


        // Splits line at "," but not if it's within a string
        public static List<string> Separate(string line)
        {
            List<string> separatedLine = new List<string>();
            string tempString = string.Empty;
            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '\"')
                {
                    inQuotes = !inQuotes;
                }

                else if (line[i] == ',')
                {
                    // Only regards commas outside of quotes
                    if (!inQuotes)
                    {
                        separatedLine.Add(tempString);
                        tempString = string.Empty;
                        continue;
                    }
                }

                tempString += line[i];
            }

            separatedLine.Add(tempString);

            return separatedLine;
        }



    }
}
