
namespace NccsvConverter.MainProject.NccsvParser.Helpers
{
    public class NccsvParserMethods
    {
        // read file = new dataset

        public void FindGlobalProperties (string file) 
        { 
            // check for *global*
            // add to globalprops for dataset
        }

        public void FindProperties (string file) 
        {
            // until *end_metadata* tag
            // check if variable exists --> create new variable/add properties + values
        }

        public void FindData (string file) 
        {
            // until *end_data* tag
            // csvhelper?
        }
    }
}
