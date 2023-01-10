using AngularAuthAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// I am allowing any header , Origin , method will not Throw any ERROR if youre trying to reach this API
builder.Services.AddCors(option =>
{
    option.AddPolicy("MyPolicy", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });


});
builder.Services.AddDbContext<AppDbContext>(option =>
{
    // helps to create use of configuration files 
    option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnStr"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// To register into our pipeline
app.UseCors("MyPolicy");


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
