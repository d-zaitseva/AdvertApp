using AdvertApp.ApplicationServices.Contracts;
using AdvertApp.ApplicationServices;
using AdvertApp.EF;
using AdvertApp.Repositories.Contracts;
using AdvertApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAdvertApplicationService, AdvertApplicationService>();

builder.Services.AddScoped<IAdvertContext, AdvertContext>();

builder.Services.AddScoped<IAdvertReadRepository, AdvertRepository>();
builder.Services.AddScoped<IAdvertWriteRepository, AdvertRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

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