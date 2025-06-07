namespace StormSafe.Infrastructure.Persistence
{
    public class Rio
    {
        public int RioId { get; set; }
        public string Nome { get; set; }
        public List<Sensor> Sensores { get; set; }
    }
}