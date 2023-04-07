using HRManagement.API;
using HRManagement.API.Filters;
using HRManagement.Application;
using HRManagement.Infrastructure.Persistence.Data;
using HRManagement.Persistence;

var CorsPolicy = "CorsPolicy";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddApiServices();

builder.Services.AddCors(o =>
{
    o.AddPolicy(CorsPolicy,
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

//var scope = builder.Services.BuildServiceProvider().CreateScope();
//var permissionService = scope.ServiceProvider.GetService<IPermissionService>();

//builder.Services.AddAuthorization(async options =>
//{
//    foreach (var permission in await permissionService.GetPermissionsAsync())
//    {
//        options.AddPolicy(permission.Name, policy => policy.Requirements.Add(new PermissionRequirement(permission.Name)));
//    }
//});

builder.Services.AddControllers(config =>
{
    config.Filters.Add<ApiExceptionFilterAttribute>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

var app = builder.Build();

SeedData.PopulateDb(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(CorsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
