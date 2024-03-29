using GuardianEyeAPI.Configuration;
using GuardianEyeAPI.Services;

namespace GuardianEyeAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<DataBaseSettings>(builder.Configuration.GetSection("MongoDatabase"));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<UsuarioServices>();
            builder.Services.AddScoped<CamaraServices>();
            builder.Services.AddSingleton<NotificacionServices>();
            builder.Services.AddSingleton<SensorServices>();
            builder.Services.AddSingleton<RegistroUsuariosServices>();
            builder.Services.AddSingleton<ImagenServices>();

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