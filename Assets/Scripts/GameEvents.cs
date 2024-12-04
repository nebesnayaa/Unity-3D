
namespace GameEvents
{
    public interface INotifiyer { string message { get; } }

    public class MessageEvent : INotifiyer
    {
        public string message { get; set; }
        public object data { get; set; }
    }
}

