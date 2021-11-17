using Employee.Web.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Employee.Web.Middlewares
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<EmployeeContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("EmployeeConnection"), b =>
        b.MigrationsAssembly("Employee.Web")));

    }
}
