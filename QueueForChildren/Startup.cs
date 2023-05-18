using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QueueForChildren.Web.Extensions;
using Microsoft.EntityFrameworkCore;
using QueueForChildren.Web.DataContext;
using Microsoft.AspNetCore.Identity;
using QueueForChildren.Data.Identity;
using QueueForChildren.Services.Kindergarten;
using QueueForChildren.Services.School;

namespace QueueForChildren
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
            services.RegisterDependencies();
            services.AddScoped<ISchoolListGetService, SchoolListGetService>();
            services.AddScoped<IKindergartenListGetService, KindergartenListGetService>();
            services.AddScoped<IDataCheckService, DataCheckService>();
            services.AddScoped<Services.School.IAppendToQueueService, QueueForChildren.Services.School.AppendToQueueService>();
            services.AddScoped<Services.Kindergarten.IAppendToQueueService, QueueForChildren.Services.Kindergarten.AppendToQueueService>();
            services.AddScoped<ISchoolQueueGetService, SchoolQueueGetService>();
            services.AddScoped<IKindergartenQueueGetService, KindergartenQueueGetService>();

            services.AddDbContext<QueueDbContext>(options =>
            {
                options
                    .UseLazyLoadingProxies()
                    .UseNpgsql(
                    "Host=localhost;Port=5432;Database=queue_for_children;Username=postgres;Password=rebupu09", b => b.MigrationsAssembly("QueueForChildren"));
            });

            //services.AddDbContext<UserDataContext>(options =>
            //{
            //    options.UseNpgsql(
            //        "Host=localhost;Port=5432;Database=queue_for_children;Username=postgres;Password=294495", b => b.MigrationsAssembly("QueueForChildren"));
            //});

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<QueueDbContext>();

            // services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //     .AddCookie(options => //CookieAuthenticationOptions
            //     {
            //         options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Login");
            //     });
            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Login";
            });
            
            services.AddControllersWithViews(options =>
            {
                options.EnableEndpointRouting = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "",
                    template: "{action}/{id?}",
                    defaults: new { controller = "User" });
                
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Index}/{id?}");
            });
        }
    }
}
