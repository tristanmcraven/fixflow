
using fixflow_api.Model;
using fixflow_api.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace fixflow_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<DeviceBrandService>();
            builder.Services.AddScoped<DeviceModelService>();
            builder.Services.AddScoped<StatusService>();
            builder.Services.AddScoped<TicketKitService>();
            builder.Services.AddScoped<TicketMalfunctionsService>();
            builder.Services.AddScoped<TicketRepairService>();
            builder.Services.AddScoped<TicketService>();
            builder.Services.AddScoped<TicketStatusService>();
            builder.Services.AddScoped<RepairService>();

            builder.Services.AddDbContext<FixflowContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("MyDbConnection"),
    new MySqlServerVersion(new Version(8, 0, 21))));

            var app = builder.Build();

            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
                });
            }

            app.UseAuthorization();

            app.Urls.Add("http://localhost:5108");


            app.MapControllers();

            app.Run();
        }
    }
}
