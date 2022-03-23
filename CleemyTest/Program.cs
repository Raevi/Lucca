using Domain;
using Domain.Interface;
using Infrastructure;
using Infrastructure.SqlServer;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                .AddJsonOptions(options =>
                 {
                     options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                     options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                     options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                 });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserExpensesRepository, UserExpensesRepository>();
builder.Services.AddScoped<IUserExpensesDataStorage, UserExpensesDataStorage>();
builder.Services.AddDbContext<ExpenseContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("ExpenseContext"), 
                       b => b.MigrationsAssembly("Api")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ExpenseContext>();
    if (context.Database.IsRelational())
    {
        context.Database.Migrate();
    }
    Api.DbInitializer.Initialize(context);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
