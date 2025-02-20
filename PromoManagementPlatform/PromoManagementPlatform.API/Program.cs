using FluentValidation.AspNetCore;
using PromoManagementPlatform.Infrastructure;
using PromoManagementPlatform.Application;
using PromoManagementPlatform.API.Filters;
using Microsoft.OpenApi.Models;
using PromoManagementPlatform.Infrastructure.BackgroundJobs;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddBackgroundJobs(builder.Configuration);

builder.Services.AddControllers(options => { options.Filters.Add<ValidationFilter>(); });

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please, enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{ }
        }
    });
});

builder.Services
    .AddApplicationLayerServices()
    .AddInfrastructureLayerServices(builder.Configuration);



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard("/hangfire");

app.MapControllers();

app.Run();
