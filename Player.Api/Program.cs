using Microsoft.EntityFrameworkCore;
using SignageLivePlayer.Api.Data.Db;
using SignageLivePlayer.Api.Data.Repository;
using SignageLivePlayer.Api.Data.Repository.Interfaces;
using SignageLivePlayer.Api.Configuration;

namespace SignageLivePlayer.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("testdb"));
        builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
        builder.Services.AddScoped<ISiteRepository, SiteRepository>();
        builder.Services.AddAutoMapper(opt => opt.AddProfile<MapperConfig>());

        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
