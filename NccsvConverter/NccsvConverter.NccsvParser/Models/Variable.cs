using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NccsvConverter.NccsvParser.Models
{
    public class Variable
    {
        public string VariableName { get; set; }
        public string DataType { get; set; }
        public bool Scalar { get; set; } = false;
        public string? ScalarValue { get; set; }
        public Dictionary<string, List<string>> Attributes { get; set; } = new();
    }
}
