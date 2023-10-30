using NccsvConverter.NccsvParser.Models;

namespace NccsvConverter.NccsvParser.Repositories;

public class MessageRepository
{
    public static List<Message> Messages { get; internal protected set; } = new List<Message>();
}