namespace Veox.Attendance.Record.Infraestructure.MongoDb
{
    public interface IMongoDbOptions
    {
        string Hostname { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        int Port { get; }
        string Database { get; set; }
    }
}