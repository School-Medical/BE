using Autofac;
using Microsoft.EntityFrameworkCore;
using SchoolMedicalSystem.Infrastructure.Data;

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

            // Register repository and service

            //builder.RegisterType<JwtService>().As<IJwtService>().InstancePerLifetimeScope();
            //builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();


        }
    }
}
