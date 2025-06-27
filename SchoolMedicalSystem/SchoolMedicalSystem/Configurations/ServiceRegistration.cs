using Autofac;
using Microsoft.EntityFrameworkCore;
using SchoolMedicalSystem.Application.Interfaces.IReposervices;
using SchoolMedicalSystem.Application.Interfaces.IServices;
using SchoolMedicalSystem.Application.Services;
using SchoolMedicalSystem.Infrastructure.Data;
using SchoolMedicalSystem.Infrastructure.Repositories;

namespace SchoolMedicalSystem.Application.ExceptionHandler
{
    public class ServiceRegistration : Module
    {
        private readonly IConfiguration _configuration;

        public ServiceRegistration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            // Register DbContext
            builder.Register(c =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<SchoolMedicalDbContext>();
                optionsBuilder.UseMySql(
                    _configuration.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new Version(8, 0, 35))
                );
                return new SchoolMedicalDbContext(optionsBuilder.Options);
            }).InstancePerLifetimeScope();


            // Register service

            builder.RegisterType<JWTService>().As<IJWTService>().InstancePerLifetimeScope();
            builder.RegisterType<AuthService>().As<IAuthService>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();    
            builder.RegisterType<MedicalIncidentService>().As<IMedicalIncidentService>().InstancePerLifetimeScope();
            builder.RegisterType<MedicalSuppliesService>().As<IMedicalSuppliesService>().InstancePerLifetimeScope();
            builder.RegisterType<HealthProfileService>().As<IHealthProfileService>().InstancePerLifetimeScope();
            builder.RegisterType<VaccinCampaignService>().As<IVaccinCampaignService>().InstancePerLifetimeScope();
            builder.RegisterType<VaccinConfirmationService>().As<IVaccinConfirmationService>().InstancePerLifetimeScope();
            builder.RegisterType<VaccinDocumentService>().As<IVaccinDocumentService>().InstancePerLifetimeScope();
            builder.RegisterType<BlogService>().As<IBlogService>().InstancePerLifetimeScope();


        }
    }
}
