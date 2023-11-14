namespace Event
{
    public class RabbitMQSettings
    {
        public string Url { get; set; } = null!;
        public ushort Port { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}