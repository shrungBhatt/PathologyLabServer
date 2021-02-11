using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using OPSMBackend.DataEntities;
using OPSMBackend.Repositories;
using OPSMBackend.Services;
using OPSMBackend.Services.DhlRegistration;
using OPSMBackend.Services.Finance;
using OPSMBackend.Services.Logger;
using OPSMBackend.Services.Maintenance;
using OPSMBackend.Services.Other;
using OPSMBackend.Services.Patient;
using OPSMBackend.Services.Reagent;
using OPSMBackend.Services.Roletypes;
using OPSMBackend.Services.Tests;
using OPSMBackend.Services.User;
using OPSMBackend.Services.Util;

namespace OPSMBackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "\\Nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options => {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Program.Logger.Info(Configuration.GetConnectionString("OPSMDBContext").Replace("[DataDirectory]", path));
            services.AddDbContext<OPSMContext>(options => options.UseSqlServer(Configuration.GetConnectionString("OPSMDBContext").Replace("[DataDirectory]", path)));

            InjectServicesAndRepositories(services);
        }

        public void InjectServicesAndRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            services.AddSingleton<ILog, NLogger>();
            
            services.AddTransient<IUtil, Services.Util.Util>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleTypesService, RoleTypesService>();
            services.AddTransient<ITestsService, TestsService>();
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IOtherService, OtherService>();
            services.AddTransient<IReagentService, ReagentService>();
            services.AddTransient<IDhlRegistrationService, DhlRegistrationService>();
            services.AddTransient<IMaintenanceService, MaintenanceService>();
            services.AddTransient<IFinanceService, FinanceService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
