using AnikLakDbContext;
using AnikLakDbContext.Repositories;

namespace AnikLakDBServer.MapMethods
{
    public static class MaterialsMapMethods
    {
        public static async Task GetMaterialsList(HttpContext context, WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var materialsRepo = services.GetRequiredService<MaterialsRepository>();

                var materialsList = await materialsRepo.GetAll();

                context.Response.StatusCode = StatusCodes.Status200OK;

                await context.Response.WriteAsJsonAsync(materialsList);
            }
        }

        internal static async Task AddMaterial(HttpContext context, WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var materialsRepo = services.GetRequiredService<MaterialsRepository>();

                var materials = await context.Request.ReadFromJsonAsync<List<Material>>();

                if (materials == null || !materials.Any())
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Некорректные данные. Ожидается массив объектов с полями 'name' и 'count'.");
                    return;
                }

                await materialsRepo.AddRange(materials);

                context.Response.StatusCode = StatusCodes.Status201Created;
                await context.Response.WriteAsync("Материалы успешно добавлены.");
            }
        }
    }
}
