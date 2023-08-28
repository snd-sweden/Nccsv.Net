
namespace NccsvConverter.MainProject.NccsvParser.Helpers
{
    public class NccsvParserMethods
    {
        // read file = new dataset

        public List<string[]> FindGlobalProperties (string file) 
        {
            // check for *global*, disregard others
            return new List<string[]>();

        }

        public void AddGlobalProperties(string file)
        {
            // add to globalprops for dataset, in dictionary<[1],[2]>

        }

        public List<string[]> FindProperties (string file) 
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

        public void AddProperties (string file)
        {
            // if variableName without *DATA_TYPE* tag
            // add to Properties as <[1], List<[2]-[n]>>
        }

        public void FindData (string file) 
        {
            // until *end_data* tag
            // csvhelper?
        }
    }
}
