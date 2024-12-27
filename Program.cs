// Doğru AutoMapper uzantısını içe aktarın
using Odev.Core.Interfaces;
using Odev.Altyapi.Repositories;
using Microsoft.EntityFrameworkCore;
using Odev.Altyapi.Data;
using Odev.Altyapi.Logging;


namespace VeriNesneOdev2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            

            // Add services to the container.
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(Program));

            // FileLogger eklenmesi
            builder.Services.AddScoped<Odev.Core.Interfaces.ILogger>(provider => new FileLogger("log.txt"));

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
             sqlOptions => sqlOptions.MigrationsAssembly("VeriNesneOdev2")));

            // Repository'lerin eklenmesi
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            

            // ApplicationDbContext eklenmesi
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
}
