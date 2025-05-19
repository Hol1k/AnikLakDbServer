
using Microsoft.EntityFrameworkCore;

namespace AnikLakDbContext.Repositories
{
    public class ToolsRepository
    {
        private readonly AnikLakContext _context;

        public ToolsRepository(AnikLakContext context)
        {
            _context = context;
        }

        public async Task<List<Tool>> GetAll()
        {
            return await _context.Tools.OrderBy(t => t.FunctioningCount).ToListAsync();
        }

        public async Task AddNew(Tool tool)
        {
            await _context.Tools.AddAsync(tool);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateValues(Tool tool)
        {
            var oldTool = await _context.Tools.FirstOrDefaultAsync(t => t.Id == tool.Id);

            if (oldTool != null)
            {
                oldTool.Name = tool.Name;
                oldTool.Count = tool.Count;
                oldTool.FunctioningCount = tool.FunctioningCount;
            }

            await _context.SaveChangesAsync();
        }
    }
}
