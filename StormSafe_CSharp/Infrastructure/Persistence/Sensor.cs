namespace StormSafe.Infrastructure.Persistence
{
    public class Sensor
    {
        public int SensorId { get; set; }
        public string Tipo { get; set; }
        public int RioId { get; set; }
        public Rio Rio { get; set; }
    }
}