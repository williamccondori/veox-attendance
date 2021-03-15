// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace Veox.Attendance.Identity.Infraestructure.MongoDb
{
    public class MongoDbOptions
    {
        public string Hostname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
    }
}