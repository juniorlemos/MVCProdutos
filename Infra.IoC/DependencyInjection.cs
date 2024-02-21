using Domain.Interfaces.Repository;
using Infra.Data.Context;
using Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
         IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"
              ), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient(typeof(IProductRepository), typeof(ProductRepository));
            services.AddTransient(typeof(ICategoryRepository), typeof(CategoryRepository));

            return services;
        }
    }
}
