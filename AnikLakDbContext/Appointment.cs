namespace AnikLakDbContext
{
    public class Appointment
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client? Client { get; set; }
        public string? ClientName { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string Status { get; set; } = "";
        public string ServiceListString { get; set; } = "";
        public string DiscountListString { get; set; } = "";
        public string ToolListString { get; set; } = "";
        public List<Tool>? ToolList { get; set; }
        public string MaterialListString { get; set; } = "";
        public List<Material>? MaterialList { get; set; }
        public decimal BasePrice { get; set; }
        public decimal FinalPrice { get; set; }
    }
}
