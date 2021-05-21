using BookStore.Services;
using BookStore.Data;
using BookStore.Services.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using BookStore.Data.Implementations;

namespace BookStore.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private string ConnectionString =>
            Configuration.GetConnectionString("BookStoreDb");

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            services.AddDbContext<BookStoreDbContext>(options =>
                options.UseSqlServer(ConnectionString));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<IEmployeesService, EmployeesService>();
            services.AddScoped<ICustomerService, CustomersService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<IAuthorsService, AuthorsService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}