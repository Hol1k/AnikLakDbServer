namespace AnikLakDbContext.Repositories
{
    public class AppointmentsRepository
    {
        private readonly AnikLakContext _context;

        public AppointmentsRepository(AnikLakContext context)
        {
            _context = context;
        }


    }
}
