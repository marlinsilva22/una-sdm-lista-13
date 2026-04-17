namespace AmericanAirlinesApi.Models
{
    public class Aeronave
    {
        public int Id { get; set; }
        public string? Modelo { get; set; }
        public string? CodigoCauda { get; set; }
        public int CapacidadePassageiros { get; set; }
    }
}