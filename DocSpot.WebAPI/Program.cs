using DocSpot.Models;
using DocSpot.Repository.DAL.Interfaces;
using DocSpot.Repository.DAL.Repositories;
using DocSpot.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<DocSpotDBContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IGenericRepository<Hospital>, GenericRepository<Hospital>>();
builder.Services.AddScoped<IGenericRepository<Department>, GenericRepository<Department>>();
builder.Services.AddScoped<IGenericRepository<Doctor>, GenericRepository<Doctor>>();
builder.Services.AddScoped<IGenericRepository<Patient>, GenericRepository<Patient>>();
builder.Services.AddScoped<IGenericRepository<Admin>, GenericRepository<Admin>>();
builder.Services.AddScoped<IGenericRepository<Appointment>, GenericRepository<Appointment>>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();


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
