namespace NccsvConverter.NccsvParser.Helpers
{
    public class NccsvVerifierMethods
    {
        public bool CheckNccsvExtension(string filePath)
        {
            if (filePath.EndsWith(".nccsv"))
            {
                return true;
            }

            return false;
        }

        public bool VerifyNccsv(List<string[]> potentialNccsv)
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