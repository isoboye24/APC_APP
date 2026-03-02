using APC.AllForms;
using APC.Applications.Services;
using APC.DAL;
using APC.Domain.Interfaces;
using APC.Infrastructure.Repositories;
using APC.Utility;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APC
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();

            // DbContext 
            services.AddScoped<APCEntities>();

            // Repositories
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddScoped<IEmploymentStatusRepository, EmploymentStatusRepository>();

            // Services
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IEmploymentStatusService, EmploymentStatusService>();

            // Forms
            services.AddTransient<FormLogin>();
            services.AddTransient<FormCountry>();
            services.AddTransient<FormPosition>();
            services.AddTransient<FormEmploymentStatus>();

            services.AddTransient<FormSettings>();

            var initializedForms = new HashSet<Form>();

            Application.Idle += (s, e) =>
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (!initializedForms.Contains(form))
                    {
                        initializedForms.Add(form);
                        form.Load += (sender, args) =>
                        {
                            AutoResizeInitializer.Initialize(form, ZoomManager.CurrentFontSize);
                        };
                    }
                }
            };

            var provider = services.BuildServiceProvider();

            Application.Run(provider.GetRequiredService<FormLogin>());
        }
    }
}
