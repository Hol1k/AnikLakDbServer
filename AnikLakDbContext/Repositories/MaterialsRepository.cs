using Microsoft.EntityFrameworkCore;

namespace AnikLakDbContext.Repositories
{
    public class MaterialsRepository
    {
        private readonly AnikLakContext _context;

        public MaterialsRepository(AnikLakContext context)
        {
            _context = context;
        }

        public async Task<List<Material>> GetAll()
        {
            return await _context.Materials.OrderByDescending(m => m.Count).ToListAsync();
        }

        public async Task AddRange(List<Material> materials)
        {
            foreach (var material in materials)
            {
                var existingMaterial = await _context.Materials.FirstOrDefaultAsync(m => m.Name == material.Name);

                if (existingMaterial != null)
                {
                    existingMaterial.Count += material.Count;
                }
                else
                {
                    _context.Materials.Add(material);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
