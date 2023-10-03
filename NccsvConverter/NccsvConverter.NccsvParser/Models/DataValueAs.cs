using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NccsvConverter.NccsvParser.Models
{
    public class DataValueAs<T> : DataValue
    {
        public T Value;
    }
}
