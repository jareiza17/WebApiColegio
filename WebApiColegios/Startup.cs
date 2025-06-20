using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using DataAccessLayer;
using DomainLayer.Services;
using DomainLayer.Repositories;
using DataAccessLayer.Repositories;

namespace WebApiColegios
{
    /// <summary>
    /// Clase de configuración principal para la aplicación.
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Startup"/>.
        /// </summary>
        /// <param name="configuration">La configuración de la aplicación.</param>
        public Startup(IConfiguration configuration)
        {
            JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();
            this.configuration = configuration;
        }

        /// <summary>
        /// Configura los servicios de la aplicación.
        /// </summary>
        /// <param name="services">La colección de servicios a configurar.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container
            services.AddControllers(options => {
                // Agregar filtros, convenciones, etc.
            })
            .AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
            .AddNewtonsoftJson();

            //services.AddDbContext<SchoolContext>(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("defaultConnection")));

            services.AddDbContext<SchoolContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("defaultConnection"),
                b => b.MigrationsAssembly("DataAccessLayer")));



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                    },
                    new string[]{ }
                }
            });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["jwtKey"])),
                    ClockSkew = TimeSpan.Zero
                });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<SchoolContext>()
                .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsAdmin", policy => policy.RequireClaim("isAdmin"));
            });

            // Otros servicios

            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }

        /// <summary>
        /// Configura el pipeline de solicitudes HTTP.
        /// </summary>
        /// <param name="app">El constructor de la aplicación.</param>
        /// <param name="env">El entorno de hosting.</param>
        /// <param name="logger">El logger de la aplicación.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            // Configure the HTTP request pipeline
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

}
