namespace AnikLakDbContext
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Note { get; set; } = "";
        public int AppointmentsCount { get; set; }
        public List<Appointment>? Appointments { get; set; }
    }
}
