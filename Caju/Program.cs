using Caju.Domain.Ports;
using Caju.Domain.UseCases;
using Caju.Driven.Database;
using Caju.Driven.Database.Configurations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Injeção de dependência
builder.Services.AddScoped<ICategoryPorts, CategoryRepository>();
builder.Services.AddScoped<IBalancePorts, BalanceRepository>();
builder.Services.AddScoped<ITransactionPorts, TransactionRepository>();
builder.Services.AddScoped<TransactionsUseCase>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() 
              .AllowAnyMethod() 
              .AllowAnyHeader();
    });
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    c.RoutePrefix = string.Empty;  // Para acessar diretamente em http://localhost:8081/swagger
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
