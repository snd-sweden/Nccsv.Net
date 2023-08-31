namespace NccsvConverter.NccsvParser.Helpers
{
    public class NccsvVerifierMethods
    {
        public static bool CheckNccsvExtension(string filePath)
        {
            if (filePath.EndsWith(".nccsv"))
            {
                return true;
            }

            return false;
        }

        // Verifies that the files Conventions include a reference to NCCSV
        public static bool VerifyNccsv(List<string[]> potentialNccsv)
        {
            foreach (var s in potentialNccsv[0])
            {
                if (s.Contains("NCCSV"))
                {
                    return true;
                }
            }

            return false;
        }
    }
}