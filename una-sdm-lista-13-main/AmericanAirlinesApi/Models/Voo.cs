namespace AmericanAirlinesApi.Models
{
    public class Voo
    {
        public int Id { get; set; }
        public string? CodigoVoo { get; set; }
        public string? Origem { get; set; }
        public string? Destino { get; set; }

        public int AeronaveId { get; set; }
        public Aeronave? Aeronave { get; set; }

        public string? Status { get; set; }
    }
}