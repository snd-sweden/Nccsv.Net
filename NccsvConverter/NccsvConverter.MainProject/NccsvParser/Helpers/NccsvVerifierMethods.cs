using UtfUnknown;

namespace NccsvConverter.MainProject.NccsvParser.Helpers
{
    public class NccsvVerifierMethods
    {
        // Returns true if input file is in utf-8 encoding, false if not
        public bool Utf8Checker(string filePath)
        {
            DetectionResult result = CharsetDetector.DetectFromFile(filePath);
            DetectionDetail resultDetected = result.Detected;
            string encodingName = resultDetected.EncodingName;
            var encoding = resultDetected.Encoding;
            if (encodingName == "utf-8")
            {
                return true;
            }
            return false;
        }
    }
}