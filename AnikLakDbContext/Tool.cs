namespace AnikLakDbContext
{
    public class Tool
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Count { get; set; }
        public int FunctioningCount { get; set; }
        public List<Appointment>? Appointments { get; set; }
    }
}
