using UtfUnknown;

namespace NccsvConverter.MainProject.NccsvParser.Helpers
{
    public class NccsvVerifierMethods
    {
        public bool NccsvExtensionChecker(string filePath)
        {
            if (filePath.EndsWith(".nccsv"))
            {
                return true;
            }
            return false;
        }
    }
}