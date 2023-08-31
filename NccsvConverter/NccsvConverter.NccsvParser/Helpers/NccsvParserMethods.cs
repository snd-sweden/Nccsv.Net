
using System.Text.Json.Serialization.Metadata;
using NccsvConverter.NccsvParser.Models;

namespace NccsvConverter.NccsvParser.Helpers
{
    public class NccsvParserMethods
    {
        // read file = new dataset

        //finds global properties and stores them without *GLOBAL*-tag in a string array
        public static List<string[]> FindGlobalProperties(List<string[]> csv)
        {
            var globalProps = new List<string[]>();
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


        public static void AddGlobalProperties(DataSet dataSet, List<string[]> globalProps)
        {
            

        }


        // Constructs a list of properties where each property is represented as a string array where [0]
        // is the variable name, [1] is the attribute name and [2] to [n] is the values. Does not include
        // global properties as they are collected in FindGlobalProperties.
        public static List<string[]> FindProperties (List<string[]> file) 
        {
            var properties = new List<string[]>();
            
            foreach (var line in file)
            {
                // disgregard global properties as they are collected in FindGlobalProperties
                if (line.Contains("*GLOBAL*"))
                {
                    continue;
                }

                // end collection of properties at metadata end tag (needs to exist)
                if (line.Contains("*END_METADATA*"))
                {
                    break;
                }

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

        public static void SetVariableDataType(string file)
        {
            // find *DATA_TYPE* tag
            // set DataType prop to *data_type* value
        }

        public static void AddProperties(string file)
        {
            // if variableName without *DATA_TYPE* tag
            // add to Properties as <[1], List<[2]-[n]>>
        }

        public static void FindData(string file)
        {
            // until *end_data* tag
            // csvhelper?
        }
    }
}
