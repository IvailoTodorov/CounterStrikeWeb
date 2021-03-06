namespace CounterStrikeWeb
{
    using CounterStrikeWeb.Data;
    using CounterStrikeWeb.Data.Models;
    using CounterStrikeWeb.Infrastrucure;
    using CounterStrikeWeb.Services.Events;
    using CounterStrikeWeb.Services.Matches;
    using CounterStrikeWeb.Services.Players.Models;
    using CounterStrikeWeb.Services.Teams;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CounterStrikeDbContext>(options => options
            .UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddDatabaseDeveloperPageExceptionFilter();

            services
                .AddDefaultIdentity<User>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<CounterStrikeDbContext>();

            services.AddAutoMapper(typeof(Startup));

            services.AddMemoryCache();

            services.AddControllersWithViews(options => 
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

            services.AddTransient<IPlayerService, PlayerService>();
            services.AddTransient<ITeamService, TeamService>();
            services.AddTransient<IMatchService, MatchService>();
            services.AddTransient<IEventService, EventService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultAreaRoute();

                endpoints.MapControllerRoute(
                    name: "Player Details",
                    pattern: "/Players/Details/{id}/{information}",
                    defaults: new { controller = "Players", action = "Details"});

                endpoints.MapControllerRoute(
                    name: "Team Details",
                    pattern: "/Teams/Details/{id}/{information}",
                    defaults: new { controller = "Teams", action = "Details" });

                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}
