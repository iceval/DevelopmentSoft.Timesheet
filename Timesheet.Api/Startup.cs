using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Api.Models;
using Timesheet.Api.ResourceModels;
using Timesheet.BussinessLogic.Services;
//using Timesheet.DataAccess.CSV;
using Timesheet.DataAccess.MSSQL;
using Timesheet.DataAccess.MSSQL.Repositories;
using Timesheet.Domain;
using Timesheet.Integrations.GitHub;

namespace Timesheet.Api
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
            services.AddLogging();

            services.AddAutoMapper(typeof(ApiMappingProfile), typeof(DataAccessMappingProfile));

            services.AddTransient<IValidator<CreateTimeLogRequest>, TimeLogFluentValidator>();
            services.AddTransient<IValidator<LoginRequest>, LoginRequestFluentValidator>();


            //services.AddTransient<ITimesheetRepository, DataAccess.CSV.TimesheetRepository>();
            services.AddTransient<ITimesheetRepository, TimesheetRepository>();
            //services.AddTransient<IEmployeeRepository, DataAccess.CSV.EmployeeRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITimesheetService, TimesheetService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<IIssuesService, IssuesService>();

            services.AddTransient<IIssuesClient>(x => new IssuesClient("jkdfg_FSShs4S543hdgbds4352fgdgfdg3423dsd"));

            //services.AddSingleton(x => new CsvSettings(';', "..\\Timesheet.DataAccess.CSV\\Data"));

            services.AddOptions<JwtConfig>()
                .Bind(Configuration.GetSection("JwtConfig"));

            services.AddDbContext<TimesheetContext>(x =>
                x.UseSqlServer(Configuration.GetConnectionString("TimesheetContext")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Timesheet API", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "Timesheet API", Version = "v2" });

                var filePath = Path.Combine(System.AppContext.BaseDirectory, "Timesheet.Api.xml");
                c.IncludeXmlComments(filePath);
            });

            services.AddOpenApiDocument();

            services.AddControllers().AddFluentValidation();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Timesheet V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "My Timesheet V2");
            });

            app.UseSerilogRequestLogging();

            app.UseOpenApi(); // serve documents (same as app.UseSwagger())
            app.UseReDoc(); // serve ReDoc UI

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<JwtAuthMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
