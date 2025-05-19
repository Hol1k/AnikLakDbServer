using AnikLakDbContext;
using AnikLakDbContext.Repositories;

namespace AnikLakDBServer.MapMethods
{
    public static class ToolsMapMethods
    {
        public static async Task GetToolsList(HttpContext context, WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var toolsRepo = services.GetRequiredService<ToolsRepository>();

                var toolsList = await toolsRepo.GetAll();

                context.Response.StatusCode = StatusCodes.Status200OK;

                await context.Response.WriteAsJsonAsync(toolsList);
            }
        }

        internal static async Task AddNewTool(HttpContext context, WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var toolsRepo = services.GetRequiredService<ToolsRepository>();

                var tool = await context.Request.ReadFromJsonAsync<Tool>();

                if (tool == null)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Некорректные данные. Ожидаются поля объекта 'name', 'count' и 'functioningCount'.");
                    return;
                }

                await toolsRepo.AddNew(tool);

                context.Response.StatusCode = StatusCodes.Status201Created;
                await context.Response.WriteAsync("Инструменты успешно добавлены.");
            }
        }

        internal static async Task UpdateToolValues(HttpContext context, WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var toolsRepo = services.GetRequiredService<ToolsRepository>();

                var tool = await context.Request.ReadFromJsonAsync<Tool>();

                if (tool == null)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Некорректные данные. Ожидается id объекта и поля 'name', 'count' и 'functioningCount'.");
                    return;
                }

                await toolsRepo.UpdateValues(tool);

                context.Response.StatusCode = StatusCodes.Status201Created;
                await context.Response.WriteAsync("Данные об инструменте успешно обновлены.");
            }
        }
    }
}
