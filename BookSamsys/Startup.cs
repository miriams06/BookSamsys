using Microsoft.EntityFrameworkCore;
using BookSamsys.Repository;
using BookSamsys.Services;
using BookSamsys.Models;
using System.Text.Json.Serialization;
using AutoMapper;

namespace BookSamsys
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped<ILivroService, LivroService>();
            services.AddScoped<IAutorService, AutorService>();
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<IAutorRepository, AutorRepository>();

            services.AddAutoMapper(typeof(Startup));

            services.AddControllers().AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });

            services.AddDbContext<AppDBContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));


            services.AddCors();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}