using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using WebAPI1990081.Filtros;
using WebAPI1990081.Services;

namespace WebAPI1990081
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(opciones => {
                opciones.Filters.Add(typeof(FiltroDeExcepcion));
                }).AddJsonOptions(x=> x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            //------------------_------------------_--------------------_---------------------_------------------------_|

            services.AddTransient<IService, ServiceA>();
            //services.AddTransient<ServiceA>();
            services.AddTransient<ServiceTransient>();
            
            //services.AddScoped<IService, ServiceA>();
            services.AddScoped<ServiceScoped>();

            //services.AddSingleton<IService, ServiceA>();
            services.AddSingleton<ServiceSingleton>();

            services.AddResponseCaching();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
            services.AddTransient<FiltroDeAccion>();
            services.AddHostedService<EscribirArchivo>();

            //------------------_------------------_--------------------_---------------------_------------------------_|
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI1990081", Version = "v1" });
            });
        }

        //aquí en el middleware se "instancian" para poder utilizarlos
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger) 
        {

            //metodo para usar el middleware sin exponer la clase
            //app.UseMiddleware<ResponseHttpMiddleware>();
            //app.UseResponseHttpMiddleware();

            app.Map("/ruta1", app =>
            {
                app.Run(async context =>
                {
                    await context.Response.WriteAsync("Interceptando peticiones");
                });
            });


            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseResponseCaching();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}
