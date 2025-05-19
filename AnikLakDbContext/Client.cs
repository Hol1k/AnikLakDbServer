using System.ComponentModel.DataAnnotations.Schema;

namespace AnikLakDbContext
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Note { get; set; } = "";
        [NotMapped]
        public int AppointmentsCount
        {
            get
            {
                if (Appointments != null)
                    return Appointments.Count;
                else
                    return 0;
            }
        }
        public List<Appointment>? Appointments { get; set; }
    }
}
