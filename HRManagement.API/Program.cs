using HRManagement.API.Filters;
using HRManagement.API.Handlers;
using HRManagement.API.Services;
using HRManagement.Application;
using HRManagement.Application.Contracts;
using HRManagement.Application.Contracts.Services;
using HRManagement.Infrastructure.Persistence.Data;
using HRManagement.Persistence;
using Microsoft.AspNetCore.Authorization;

var CorsPolicy = "CorsPolicy";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionHandler>();

builder.Services.AddCors(o =>
{
    o.AddPolicy(CorsPolicy,
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

var scope = builder.Services.BuildServiceProvider().CreateScope();
var permissionService = scope.ServiceProvider.GetService<IPermissionService>();

builder.Services.AddAuthorization(async options =>
{
    foreach (var permission in await permissionService.GetPermissionsAsync())
    {
        options.AddPolicy(permission.Name, policy => policy.Requirements.Add(new PermissionRequirement(permission.Name)));
    }
});

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
