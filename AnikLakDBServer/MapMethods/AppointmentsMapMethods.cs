using AnikLakDbContext;
using AnikLakDbContext.Repositories;

namespace AnikLakDBServer.MapMethods
{
    public static class AppointmentsMapMethods
    {
        public static async Task GetAppointmentsList(HttpContext context, WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var appointmentsRepo = services.GetRequiredService<AppointmentsRepository>();

                var appointmentsList = await appointmentsRepo.GetAll();

                context.Response.StatusCode = StatusCodes.Status200OK;

                await context.Response.WriteAsJsonAsync(appointmentsList);
            }
        }

        internal static async Task AddNewAppointment(HttpContext context, WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var appointmentsRepo = services.GetRequiredService<AppointmentsRepository>();

                var appointmentDto = await context.Request.ReadFromJsonAsync<AppointmentDto>();

                if (appointmentDto == null)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Некорректные данные. Ожидаются поля объекта: 'clientId', 'date', 'time', 'status', 'serviceListString', 'discountListString', 'toolListString', 'materialListString', 'basePrice' и 'finalPrice'.");
                    return;
                }

                await appointmentsRepo.AddNew(appointmentDto);

                context.Response.StatusCode = StatusCodes.Status201Created;
                await context.Response.WriteAsync("Запись успешно добавлена.");
            }
        }

        internal static async Task UpdateAppointmentValues(HttpContext context, WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var appointmentsRepo = services.GetRequiredService<AppointmentsRepository>();

                var appointmentDto = await context.Request.ReadFromJsonAsync<AppointmentDto>();

                if (appointmentDto == null)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Некорректные данные. Ожидается id объекта и поля 'clientId', 'date', 'time', 'status', 'serviceListString', 'discountListString', 'toolListString', 'materialListString', 'basePrice' и 'finalPrice'.");
                    return;
                }

                await appointmentsRepo.UpdateValues(appointmentDto);

                context.Response.StatusCode = StatusCodes.Status201Created;
                await context.Response.WriteAsync("Данные о записи успешно обновлены.");
            }
        }
    }
}
