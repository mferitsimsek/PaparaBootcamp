using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PaparaBootcamp.Application.Mappings;
using PaparaBootcamp.Persistence.Context;
using PaparaBootcamp.Persistence.Repositories;
using PaparaBootcamp.Application.Services;
using PaparaBootcamp.Application.Interfaces;
using PaparaBootcamp.Application.Validators;
using PaparaBootcamp.RestfulAPI.Extensions.Middleware;
using System.Reflection;
using PaparaBootcamp.Application.CQRS.Handlers.Product;

namespace PaparaBootcamp.RestfulAPI.Extensions
{
    public static class ServiceRegistration
    {

        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {

            // Hafızada oluşturulan veritabanı
            services.AddDbContext<MyDbContext>(options =>
                options.UseInMemoryDatabase("MyDatabase"));



            // Patch methodu için gerekli olan newtonsoftJson
            services.AddControllers().AddNewtonsoftJson();


            services.AddMediatR(opt =>
            {
                opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(GetAllProductsQueryHandler).Assembly);
            });

            // Loglama
            services.AddLogging();
            //services.AddScoped<RequestLoggingMiddleware>();

            // Fluent validation için kullandığımız özellikler
            services.AddValidatorsFromAssemblyContaining<ProductValidator>();
            services.AddFluentValidationAutoValidation();

            // Repository ve Serviceler
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ProductService>();
            services.AddScoped<LoginService>();


            // Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Papara Bootcamp API", Version = "v1" });
            });

            services.AddAutoMapper(opt =>
            {
                opt.AddProfile(new ProductMappingProfile());
            });


        }
    }
}
