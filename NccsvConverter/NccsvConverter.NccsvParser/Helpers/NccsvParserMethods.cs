
using System.Linq.Expressions;
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
            var globalProperties = new List<string[]>();
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

                        globalProperties.Add(new[] { stringArray[1], propValue });
                    }

                    else
                    {
                        globalProperties.Add(new[] { stringArray[1], stringArray[2] });
                    }
                }
            }

            return globalProperties;

        }


        public static void AddGlobalProperties(DataSet dataSet, List<string[]> globalProperties)
        {
            foreach (var keyValuePair in globalProperties)
            {
                dataSet.GlobalProperties.Add(keyValuePair[0], keyValuePair[1]);
            }

        }


        // Constructs a list of properties where each property is represented as a string array where [0]
        // is the variable name, [1] is the attribute name and [2] to [n] is the values. Does not include
        // global properties as they are collected in FindGlobalProperties.
        public static List<string[]> FindProperties(List<string[]> csv)
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

        //returns true if variable name exists, otherwise false.
        public static bool CheckIfVariableExists(List<Variable> variablesFromDataSet, string varName)
        {
            foreach (var v in variablesFromDataSet)
            {
                if (v.VariableName == varName)
                {
                    return true;
                }
            }
            return false;
        }

        // if not->create new variable

        public static List<string[]> IsolateProperty(List<string[]> properties, string varName)
        {
            var isolatedProperty = new List<string[]>();
            foreach (var line in properties)
            {
                if (line[0] == varName)
                {
                    isolatedProperty.Add(line);
                }
            }
            return isolatedProperty;
        }


        public static Variable CreateVariable(List<string[]> variableProperties)
        {
            var newVar = new Variable();

            newVar.VariableName = variableProperties[0][0];
            SetVariableDataType(newVar, variableProperties);
            AddProperties("");

            return new Variable();
        }

        public static Variable SetVariableDataType(
            Variable newVar, List<string[]> variableProperties)
        {
            string varDataType = "";

            foreach (var line in variableProperties)
            {
                if (line[1] == "*DATA_TYPE*")
                {
                    varDataType = line[2];
                }
            }

            newVar.DataType = varDataType;

            return newVar;
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
