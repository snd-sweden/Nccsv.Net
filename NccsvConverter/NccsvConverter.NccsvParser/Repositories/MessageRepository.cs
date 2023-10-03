using NccsvConverter.NccsvParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NccsvConverter.NccsvParser.Repositories
{
    public class MessageRepository
    {
        public static List<Message> Messages { get; set; } = new List<Message>();

    }
}
