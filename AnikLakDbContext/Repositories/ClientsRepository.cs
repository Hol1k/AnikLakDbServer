namespace AnikLakDbContext.Repositories
{
    public class ClientsRepository
    {
        private readonly AnikLakContext _context;

        public ClientsRepository(AnikLakContext context)
        {
            _context = context;
        }


    }
}
