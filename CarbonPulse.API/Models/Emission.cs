namespace CarbonPulse.API.Models
{
    public class Emission
    {
        public int Id { get; set; }
        public string Category { get; set; } = string.Empty;  // e.g. "Transport", "Electricity"
        public double CO2Amount { get; set; }                 // Measured in kilograms
        public DateTime Date { get; set; } = DateTime.UtcNow;

        // Foreign key relationship
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
