
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AlertToCareAPI.Repositories;
using DataAccessLayer;
using DataAccessLayer.AlertManagement;
using DataAccessLayer.BedManagement;
using DataAccessLayer.IcuManagement;
using DataAccessLayer.LayoutManagement;
using DataAccessLayer.PatientManagement;
using DataAccessLayer.VitalManagement;

namespace AlertToCareAPI
{
    public class Startup
    {


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddSingleton<IMonitoringRepository, MonitoringRepository>();
            services.AddSingleton<IIcuConfigurationRepository, IcuConfigurationRepository>();
            services.AddSingleton<IPatientDbRepository, PatientDbRepository>();

            services.AddSingleton<IAlertManagement, AlertManagementSqLite>();
            services.AddSingleton<IBedManagement, BedManagementSqLite>();
            services.AddSingleton<IIcuManagement, IcuManagementSqLite>();
            services.AddSingleton<ILayoutManagement,LayoutManagementSqLite>();
            services.AddSingleton<IPatientManagement, PatientManagementSqLite>();
            services.AddSingleton<IVitalManagement, VitalManagementSqLite>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseCors();
        }
    }
}
