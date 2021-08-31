using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using EmployeesContext;
namespace EmployeesTable
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
            services.AddMvc();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<EmployeeDbContext>(builder => builder.UseSqlServer(connectionString, c => c.MigrationsAssembly("EmployeesTable")));
            services.AddScoped<IRepository<Employee>,EmployeeRep>();
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Table}/{action=Show}");
            });
        }
    }
}
