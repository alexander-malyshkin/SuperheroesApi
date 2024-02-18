using System.Text.Json;
using System.Text.Json.Serialization;
using FastEndpoints;
using FluentValidation;
using SuperHeroes.Repositories;

namespace SuperHeroes.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddFastEndpoints();
        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(typeof(SuperHeroes.Application.Shared.ResponseBase).Assembly);
        });
        builder.Services.AddValidatorsFromAssembly(typeof(SuperHeroes.Application.Shared.ResponseBase).Assembly);
        
        builder.Services.AddAuthorization();
        builder.Services.RegisterExternalProviders(builder.Configuration);
        builder.Services.AddRedisCaching(builder.Configuration);
        builder.Services.RegisterDalServices(builder.Configuration);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseFastEndpoints(config =>
        {
            config.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            config.Serializer.Options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

        app.Run();
    }
}