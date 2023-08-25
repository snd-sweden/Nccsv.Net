using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NccsvConverter.MainProject.NccsvParser.Models
{
    public class DataSet
    {
        public string Title;
        public string Summary;
        public Dictionary<string, List<string>> GlobalProperties;
        public List<Variable> Variables;
    }
}
