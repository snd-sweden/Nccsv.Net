
using System.Text.Json.Serialization.Metadata;

namespace NccsvConverter.NccsvParser.Helpers
{
    public class NccsvParserMethods
    {
        // read file = new dataset

        //finds global properties and stores them without *GLOBAL*-tag in a string array
        public List<string[]> FindGlobalProperties(List<string[]> csv)
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

        public void AddGlobalProperties(string file)
        {
            // add to globalprops for dataset, in dictionary<[1],[2]>

        }

        public List<string[]> FindProperties(string file)
        {
            // exclude *global*
            // until *end_metadata* tag

            // set datatype
            return new List<string[]>();
        }

        public bool CheckIfVariableExists(string file)
        {
            // take variable name
            // check if variable exists
            return false;
        }

        // if not->create new variable

        public void CreateVariable(string file)
        {
            // create new variable object
            // add to dataset
        }

        public void SetVariableDataType(string file)
        {
            // find *DATA_TYPE* tag
            // set DataType prop to *data_type* value
        }

        public void AddProperties(string file)
        {
            // if variableName without *DATA_TYPE* tag
            // add to Properties as <[1], List<[2]-[n]>>
        }

        public void FindData(string file)
        {
            // until *end_data* tag
            // csvhelper?
        }
    }
}
