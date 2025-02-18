using FluentValidation.AspNetCore;
using PromoManagementPlatform.Infrastructure;
using PromoManagementPlatform.Application;
using PromoManagementPlatform.API.Filters;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options => { options.Filters.Add<ValidationFilter>(); });

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.MapControllers();

app.Run();
