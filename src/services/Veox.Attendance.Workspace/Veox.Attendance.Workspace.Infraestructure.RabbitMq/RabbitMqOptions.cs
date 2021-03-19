// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace Veox.Attendance.Workspace.Infraestructure.RabbitMq
{
    public class RabbitMqOptions
    {
        public string Hostname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string VHost { get; set; }
    }
}