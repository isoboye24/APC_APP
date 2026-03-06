using APC.AllForms;
using APC.Applications.Services;
using APC.DAL;
using APC.Domain.Entities;
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
            services.AddScoped<IMaritalStatusRepository, MaritalStatusRepository>();
            services.AddScoped<INationalityRepository, NationalityRepository>();
            services.AddScoped<IProfessionRepository, ProfessionRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IMonthRepository, MonthRepository>();
            services.AddScoped<IGenderRepository, GenderRepository>();
            services.AddScoped<IAttendanceStatusRepository, AttendanceStatusRepository>();
            services.AddScoped<IMembershipStatusRepository, MembershipStatusRepository>();
            services.AddScoped<IPaymentStatusRepository, PaymentStatusRepository>();
            services.AddScoped<INextOfKinRepository, NextOfKinRepository>();

            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IConstitutionRepository, ConstitutionRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IEventExpenditureRepository, EventExpenditureRepository>();
            services.AddScoped<IEventImagesRepository, EventImagesRepository>();
            services.AddScoped<IEventReceiptRepository, EventReceiptRepository>();
            services.AddScoped<IEventSalesRepository, EventSalesRepository>();
            services.AddScoped<IEventsRepository, EventsRepository>();
            services.AddScoped<IExpenditureRepository, ExpenditureRepository>();
            services.AddScoped<IFinancialReportRepository, FinancialReportRepository>();

            // Services
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IEmploymentStatusService, EmploymentStatusService>();
            services.AddScoped<IMaritalStatusService, MaritalStatusService>();
            services.AddScoped<INationalityService, NationalityService>();
            services.AddScoped<IProfessionService, ProfessionService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IMonthService, MonthService>();
            services.AddScoped<IGenderService, GenderService>();
            services.AddScoped<IAttendanceStatusService, AttendanceStatusService>();
            services.AddScoped<IMembershipStatusService, MembershipStatusService>();
            services.AddScoped<INextOfKinService, NextOfKinService>();

            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IConstitutionService, ConstitutionService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IEventExpenditureService, EventExpenditureService>();
            services.AddScoped<IEventImagesService, EventImagesService>();
            services.AddScoped<IEventReceiptService, EventReceiptService>();
            services.AddScoped<IEventSalesService, EventSalesService>();
            services.AddScoped<IEventsService, EventsService>();
            services.AddScoped<IExpenditureService, ExpenditureService>();
            services.AddScoped<IFinancialReportService, FinancialReportService>();

            // Forms
            services.AddTransient<FormLogin>();
            services.AddTransient<FormCountry>();
            services.AddTransient<FormPosition>();
            services.AddTransient<FormEmploymentStatus>();
            services.AddTransient<FormMaritalStatus>();
            services.AddTransient<FormNationality>();
            services.AddTransient<FormProfession>();
            services.AddTransient<FormPermission>();
            services.AddTransient<FormPaymentStatus>();
            services.AddTransient<FormSettings>();

            services.AddTransient<FormComments>();
            services.AddTransient<FormSingleCommentList>();
            services.AddTransient<FormViewComment>();
            services.AddTransient<FormConstitution>();
            services.AddTransient<FormViewConstitution>();
            services.AddTransient<FormMeetingBoard>();

            services.AddTransient<FormDocument>();
            services.AddTransient<FormDocumentList>();
            services.AddTransient<FormViewDocument>();
            services.AddTransient<FormEventExpenditure>();
            services.AddTransient<FormViewEventExpenditure>();
            services.AddTransient<FormEventSingleImage>();
            services.AddTransient<FormViewSingleImage>();
            services.AddTransient<FormEventReceipt>();
            services.AddTransient<FormViewEventReceipt>();
            services.AddTransient<FormEventSales>();
            services.AddTransient<FormViewEventSales>();
            services.AddTransient<FormEvent>();
            services.AddTransient<FormEventDetailsBoard>();
            services.AddTransient<FormEventsList>();

            services.AddTransient<FormExpenditure>();
            services.AddTransient<FormViewExpenditure>();
            services.AddTransient<FormFinancialReport>();
            services.AddTransient<FormViewFinancialReport>();
            services.AddTransient<FormReportsBoard>();


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
