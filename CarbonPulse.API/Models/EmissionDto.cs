namespace CarbonPulse.API.DTOs
{
    public class EmissionDto
    {
        public string Category { get; set; } = string.Empty;
        public double CO2Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
