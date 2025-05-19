using AnikLakDbContext;
using AnikLakDbContext.Repositories;
using AnikLakDBServer.MapMethods;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AnikLakContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(AnikLakDbContext)));
});

builder.Services.AddScoped<AppointmentsRepository>();
builder.Services.AddScoped<ClientsRepository>();
builder.Services.AddScoped<MaterialsRepository>();
builder.Services.AddScoped<ToolsRepository>();

var app = builder.Build();

#region AppointmentsSubsystem

app.MapGet("/appointments/get-appointments-list", async (context) => 
await AppointmentsMapMethods.GetAppointmentsList(context, app));

#endregion

#region ClientsSubsystem

app.MapGet("/clients/get-clients-list", async (context) =>
await ClientsMapMethods.GetClientsList(context, app));

#endregion

#region MaterialsSubsystem

app.MapGet("/materials/get-materials-list", async (context) =>
await MaterialsMapMethods.GetMaterialsList(context, app));

app.MapPut("/materials/add-material", async (context) =>
await MaterialsMapMethods.AddMaterial(context, app));

#endregion

#region ToolsSubsystem

app.MapGet("/tools/get-tools-list", async (context) =>
await ToolsMapMethods.GetToolsList(context, app));

app.MapPost("/tools/add-new-tool", async (context) =>
await ToolsMapMethods.AddNewTool(context, app));

app.MapPut("/tools/update-tool-values", async (context) =>
await ToolsMapMethods.UpdateToolValues(context, app));

#endregion

app.Run();
