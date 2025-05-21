
using Microsoft.EntityFrameworkCore;

namespace AnikLakDbContext.Repositories
{
    public class AppointmentsRepository
    {
        private readonly AnikLakContext _context;

        public AppointmentsRepository(AnikLakContext context)
        {
            _context = context;
        }

        public async Task<List<Appointment>> GetAll()
        {
            return await _context.Appointments
                .Include(a => a.Client)
                .Include(a => a.ToolList)
                .Include(a => a.MaterialList)
                .OrderBy(a => a.Date)
                .ThenBy(a => a.Time)
                .ToListAsync();
        }

        public async Task AddNew(AppointmentDto appointmentDto)
        {
            var appointment = new Appointment()
            {
                ClientId = appointmentDto.ClientId,
                Date = DateOnly.Parse(appointmentDto.Date),
                Time = TimeOnly.Parse(appointmentDto.Time),
                Status = appointmentDto.Status,
                ServiceListString = appointmentDto.ServiceListString,
                DiscountListString = appointmentDto.DiscountListString,
                BasePrice = appointmentDto.BasePrice,
                FinalPrice = appointmentDto.FinalPrice
            };

            if (appointmentDto.ToolListString != null && appointmentDto.ToolListString != "")
            {
                List<string> tools = appointmentDto.ToolListString.Split(';').ToList();
                List<Tool> toolList = new List<Tool>();
                foreach (string toolValues in tools)
                {
                    var toolName = toolValues.Split('&')[1];
                    var tool = await _context.Tools.FirstOrDefaultAsync(t => t.Name == toolName);
                    if (tool != null)
                        toolList.Add(tool);
                }
                appointment.ToolList = toolList;
            }

            if (appointmentDto.MaterialListString != null && appointmentDto.MaterialListString != "")
            {
                List<string> materials = appointmentDto.MaterialListString.Split(';').ToList();
                List<Material> materialList = new List<Material>();
                foreach (string materialValues in materials)
                {
                    var materialName = materialValues.Split('&')[1];
                    var material = await _context.Materials.FirstOrDefaultAsync(m => m.Name == materialName);
                    if (material != null)
                        materialList.Add(material);
                }
                appointment.MaterialList = materialList;
            }

            await _context.Appointments.AddAsync(appointment);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateValues(AppointmentDto appointmentDto)
        {
            var oldAppointment = await _context.Appointments
                .Include(a => a.ToolList)
                .Include(a => a.MaterialList)
                .FirstOrDefaultAsync(a => a.Id == appointmentDto.Id);

            if (oldAppointment != null)
            {
                oldAppointment.Client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == appointmentDto.ClientId);
                oldAppointment.Date = DateOnly.Parse(appointmentDto.Date);
                oldAppointment.Time = TimeOnly.Parse(appointmentDto.Time);
                oldAppointment.Status = appointmentDto.Status;
                oldAppointment.ServiceListString = appointmentDto.ServiceListString;
                oldAppointment.DiscountListString = appointmentDto.DiscountListString;
                oldAppointment.ToolList?.Clear();
                oldAppointment.MaterialList?.Clear();
                oldAppointment.BasePrice = appointmentDto.BasePrice;
                oldAppointment.FinalPrice = appointmentDto.FinalPrice;

                await _context.SaveChangesAsync();

                if (appointmentDto.ToolListString != null && appointmentDto.ToolListString != "")
                {
                    List<string> tools = appointmentDto.ToolListString.Split(';').ToList();
                    List<Tool> toolList = new List<Tool>();
                    foreach (string toolValues in tools)
                    {
                        var toolName = toolValues.Split('&')[1];
                        var tool = await _context.Tools.FirstOrDefaultAsync(t => t.Name == toolName);
                        if (tool != null)
                            toolList.Add(tool);
                    }
                    oldAppointment.ToolList = toolList;
                }
                if (appointmentDto.MaterialListString != null && appointmentDto.MaterialListString != "")
                {
                    List<string> materials = appointmentDto.MaterialListString.Split(';').ToList();
                    List<Material> materialList = new List<Material>();
                    foreach (string materialValues in materials)
                    {
                        var materialName = materialValues.Split('&')[1];
                        var material = await _context.Materials.FirstOrDefaultAsync(m => m.Name == materialName);
                        if (material != null)
                            materialList.Add(material);
                    }
                    oldAppointment.MaterialList = materialList;
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}

