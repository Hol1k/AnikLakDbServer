
using Microsoft.EntityFrameworkCore;

namespace AnikLakDbContext.Repositories
{
    public class ClientsRepository
    {
        private readonly AnikLakContext _context;

        public ClientsRepository(AnikLakContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetAll()
        {
            return await _context.Clients.OrderByDescending(c => c.Id).ToListAsync();
        }

        public async Task AddNew(Client client)
        {
            await _context.Clients.AddAsync(client);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateValues(Client client)
        {
            var oldClient = await _context.Clients.FirstOrDefaultAsync(c => c.Id == client.Id);

            if (oldClient != null)
            {
                oldClient.Name = client.Name;
                oldClient.PhoneNumber = client.PhoneNumber;
                oldClient.Note = client.Note;
            }

            await _context.SaveChangesAsync();
        }
    }
}
