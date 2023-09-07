﻿
using System.Linq.Expressions;
using System.Text.Json.Serialization.Metadata;
using NccsvConverter.NccsvParser.Models;

namespace NccsvConverter.NccsvParser.Helpers
{
    public class NccsvParserMethods
    {
        // read file = new dataset

        public static string FindTitle(List<string[]> csv)
        {
            foreach (var line in csv)   
            {
                if (line[0] == "title")
                {
                    return line[1];
                }
            }

            return null;
        }

        public static string FindSummary(List<string[]> csv)
        {
            foreach (var line in csv)
            {
                if (line[0] == "summary")
                {
                    return line[1];
                }
            }

            return null;
        }

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

        //takes the list generated by FindGlobalProperties, and adds them to it's
        //GlobalProperties property.
        public static void AddGlobalProperties(DataSet dataSet, List<string[]> globalProperties)
        {
            foreach (var keyValuePair in globalProperties)
            {
                dataSet.GlobalProperties.Add(keyValuePair[0], keyValuePair[1]);
            }

        }


        /*Constructs a list of properties where each property is represented as a string array where [0]
        is the variable name, [1] is the attribute name and [2] to [n] is the values. Does not include
        global properties as they are collected in FindGlobalProperties.*/
        public static List<string[]> FindVariables(List<string[]> csv)
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

        //extracts a named property and all it's associated metadata from a FindVariables list
        public static List<string[]> IsolateProperty(List<string[]> globalProperties, string varName)
        {
            var isolatedProperty = new List<string[]>();
            foreach (var line in globalProperties)
            {
                if (line[0] == varName)
                {
                    isolatedProperty.Add(line);
                }
            }
            return isolatedProperty;
        }

        //when given a IsolateProperty list, creates a Variable out of that data.
        public static Variable CreateVariable(List<string[]> variableProperties)
        {
            var newVar = new Variable();

            //this loop finds the name of the variable, even if there would be
            //empty lines before the actual property data
            foreach (var line in variableProperties)
            {
                if (line[0].Length > 0)
                {
                    newVar.VariableName = line[0];
                    break;
                }
            }

            SetVariableDataType(newVar, variableProperties);
            AddProperties(newVar, variableProperties);

            return newVar;
        }

        // Takes a variable property list, and extracts the name of the data type.
        // Used by: CreateVariable
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


        // Adds string array of properties to variable as a dictionary where
        // [1] is the attribute name and [2] to [n] is the attribute values
        // Used by: CreateVariable
        public static void AddProperties(Variable variable, List<string[]> variableProperties )
        {
            foreach (var properties in variableProperties)
            {
                // disregard data type row as datatype is set in SetVariableDataType
                if (properties[1] == "*DATA_TYPE*")
                {
                    continue;
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

       //Extracts the data section of the nccsv-file.
        public static List<string[]> FindData(List<string[]> csv)
        {
            var data = new List<string[]>();
            var dataSectionReached = false;

            foreach (var line in csv)
            {
                if (line[0] == "*END_DATA*")
                {
                    break;
                }

                if (dataSectionReached)
                {
                    data.Add(line);
                }
                
                if (line[0] =="*END_METADATA*")
                {
                    dataSectionReached = true;
                }

            }

            return data;
        }

        public static void AddData(List<string[]> data, DataSet dataSet)
        {
            dataSet.Data = data;
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
