using NccsvConverter.NccsvParser.Models;

namespace NccsvConverter.NccsvParser.Repositories;

public class MessageRepository
{
    public static List<Message> Messages { get; set; } = new List<Message>();

}