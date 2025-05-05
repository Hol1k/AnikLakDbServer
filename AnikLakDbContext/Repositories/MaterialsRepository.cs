namespace AnikLakDbContext.Repositories
{
    public class MaterialsRepository
    {
        private readonly AnikLakContext _context;

        public MaterialsRepository(AnikLakContext context)
        {
            _context = context;
        }


    }
}
