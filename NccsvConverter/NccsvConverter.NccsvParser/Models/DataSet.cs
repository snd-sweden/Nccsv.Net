using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NccsvConverter.NccsvParser.Models
{
    public class DataSet
    {
        public string? Title;
        public string? Summary;
        public Dictionary<string, string> GlobalAttributes = new();
        public List<Variable> Variables = new();
        public List<string[]> Data = new();
    }
}
