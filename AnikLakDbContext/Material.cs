namespace AnikLakDbContext
{
    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Count { get; set; }
        public List<Appointment>? Appointments { get; set; }
    }
}
