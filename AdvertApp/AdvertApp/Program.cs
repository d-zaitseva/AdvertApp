using AdvertApp.ApplicationServices.Contracts;
using AdvertApp.ApplicationServices;
using AdvertApp.EF;
using AdvertApp.Repositories.Contracts;
using AdvertApp.Repositories;
using AdvertApp.AutoMapping;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Adverts API",
        Description = "An ASP.NET Core Web API for managing Adverts",
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddScoped<IAdvertApplicationService, AdvertApplicationService>();
builder.Services.AddScoped<IImageApplicationService, ImageApplicationService>();
builder.Services.AddScoped<IUserApplicationService,  UserApplicationService>();

builder.Services.AddScoped<IAdvertContext, AdvertContext>();

builder.Services.AddScoped<IAdvertReadRepository, AdvertRepository>();
builder.Services.AddScoped<IAdvertWriteRepository, AdvertRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddAutoMapper(typeof(AppMappingProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();