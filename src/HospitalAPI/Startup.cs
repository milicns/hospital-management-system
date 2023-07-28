using System.Text;
using HospitalLibrary.Auth;
using HospitalLibrary.Auth.Interface;
using HospitalLibrary.Blog.Repository;
using HospitalLibrary.Blog.Service;
using HospitalLibrary.Doctor.Repository;
using HospitalLibrary.Doctor.Service;
using HospitalLibrary.DoctorReferral.Repository;
using HospitalLibrary.DoctorReferral.Service;
using HospitalLibrary.Email;
using HospitalLibrary.Examination.Repository;
using HospitalLibrary.Examination.Service;
using HospitalLibrary.ExaminationReport.Repository;
using HospitalLibrary.ExaminationReport.Service;
using HospitalLibrary.MedicalData.Repository;
using HospitalLibrary.MedicalData.Service;
using HospitalLibrary.MenstrualPeriod.Repository;
using HospitalLibrary.MenstrualPeriod.Service;
using HospitalLibrary.News.Repository;
using HospitalLibrary.News.Service;
using HospitalLibrary.Patient.Repository;
using HospitalLibrary.Patient.Service;
using HospitalLibrary.Settings;
using HospitalLibrary.User.Repository;
using HospitalLibrary.ValidationService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace HospitalAPI
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
            var emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            
            var jwtSettings = Configuration.GetSection("JwtSettings");
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value))
                };
            });
            
            
            services.AddDbContext<HospitalDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("HospitalDb")));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GraphicalEditor", Version = "v1" });
                
            });
            
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IPatientService,PatientService>();
            services.AddScoped<IPatientRepository,PatientRepository>();
            services.AddScoped<IDoctorService,DoctorService>();
            services.AddScoped<IDoctorRepository,DoctorRepository>();
            services.AddScoped<IAuthService,AuthService>();
            services.AddScoped<IValidationService,ValidationService>();
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IJwtHandler, JwtHandler>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IExaminationService, ExaminationService>();
            services.AddScoped<IExaminationRepository, ExaminationRepository>();
            services.AddScoped<IExaminationReportService, ExaminationReportService>();
            services.AddScoped<IExaminationReportRepository, ExaminationReportRepository>();
            services.AddScoped<IDoctorReferralService, DoctorReferralService>();
            services.AddScoped<IDoctorReferralRepository, DoctorReferralRepository>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<IMedicalDataService,MedicalDataService>();
            services.AddScoped<IMedicalDataRepository,MedicalDataRepository>();
            services.AddScoped<IMenstrualPeriodService,MenstrualPeriodService>();
            services.AddScoped<IMenstrualPeriodRepository,MenstrualPeriodRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HospitalAPI v1"));
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
