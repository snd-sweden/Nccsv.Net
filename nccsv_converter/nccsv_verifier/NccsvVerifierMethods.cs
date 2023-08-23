using UtfUnknown;

namespace nccsv_verifier
{
    public class NccsvVerifierMethods
    {
        public bool Utf8Checker(string filePath)
        {
            DetectionResult result = CharsetDetector.DetectFromFile(filePath);
            DetectionDetail resultDetected = result.Detected;
            string encodingName = resultDetected.EncodingName;

            //return encodingName;
            if (encodingName == "utf-8")
            {
                return true;
            }

            return false;

        }
    }
}