using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NccsvConverter.MainProject.NccsvParser.Models
{
    public class Variable
    {
        public string VariableName { get; set; }
        public string DataType { get; set; }
        public Dictionary <string, List<string>> Properties { get;set; } = new();
    }
}
