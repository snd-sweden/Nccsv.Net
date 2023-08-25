
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

        public bool VerifyNccsv(List<string[]> potentialNccsv)
        {
            if (potentialNccsv[0][5].Contains("NCCSV-1.2") || potentialNccsv[0][5].Contains("NCCSV-1.1"))
            {
                return true;
            }

            return false;
        }
    }
}