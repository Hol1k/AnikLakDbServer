using AnikLakDbContext;
using AnikLakDbContext.Repositories;

namespace AnikLakDBServer.MapMethods
{
    public static class ClientsMapMethods
    {
        public static async Task GetClientsList(HttpContext context, WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var clientsRepo = services.GetRequiredService<ClientsRepository>();

                var clientsList = await clientsRepo.GetAll();

                context.Response.StatusCode = StatusCodes.Status200OK;

                await context.Response.WriteAsJsonAsync(clientsList);
            }
        }

        internal static async Task AddNewClient(HttpContext context, WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var clientsRepo = services.GetRequiredService<ClientsRepository>();

                var client = await context.Request.ReadFromJsonAsync<Client>();

                if (client == null)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Некорректные данные. Ожидаются поля объекта 'name', 'phoneNumber' и 'note'.");
                    return;
                }

                await clientsRepo.AddNew(client);

                context.Response.StatusCode = StatusCodes.Status201Created;
                await context.Response.WriteAsync("Клиент успешно добавлен.");
            }
        }

        internal static async Task UpdateClientValues(HttpContext context, WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var clientsRepo = services.GetRequiredService<ClientsRepository>();

                var client = await context.Request.ReadFromJsonAsync<Client>();

                if (client == null)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Некорректные данные. Ожидается id объекта и поля 'name', 'phoneNumber' и 'note'.");
                    return;
                }

                await clientsRepo.UpdateValues(client);

                context.Response.StatusCode = StatusCodes.Status201Created;
                await context.Response.WriteAsync("Данные о клиенте успешно обновлены.");
            }
        }
    }
}
