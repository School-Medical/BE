using Autofac;
using CinemaManagement.API.Data;
using CinemaManagement.API.Repository.Interface;
using CinemaManagement.API.Repository;
using CinemaManagement.API.Service.Interface;
using CinemaManagement.API.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Hangfire;

namespace CinemaManagement.API.DependencyInjection
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
            builder.Register(context =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
                return new ApplicationDbContext(optionsBuilder.Options);
            }).AsSelf().InstancePerLifetimeScope();

            // Register repository and service
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<GenresRepository>().As<IGenresRepository>().InstancePerLifetimeScope();
            builder.RegisterType<GenresService>().As<IGenresService>().InstancePerLifetimeScope();

            builder.RegisterType<MovieGenreRepository>().As<IMovieGenreRepository>().InstancePerLifetimeScope();

            builder.RegisterType<MovieRepository>().As<IMovieRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MovieService>().As<IMovieService>().InstancePerLifetimeScope();

            builder.RegisterType<TheaterRepository>().As<ITheaterRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TheaterService>().As<ITheaterService>().InstancePerLifetimeScope();

            builder.RegisterType<ShowtimeRepository>().As<IShowtimeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ShowtimeService>().As<IShowtimeService>().InstancePerLifetimeScope();

            builder.RegisterType<RoomRepository>().As<IRoomRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RoomService>().As<IRoomService>().InstancePerLifetimeScope();

            builder.RegisterType<ChairRepository>().As<IChairRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ChairService>().As<IChairService>().InstancePerLifetimeScope();
            
            builder.RegisterType<ChairShowtimeRepository>().As<IChairShowtimeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ChairShowtimeService>().As<IChairShowtimeService>().InstancePerLifetimeScope();
            
            builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>().InstancePerLifetimeScope();
            
            builder.RegisterType<TicketRepository>().As<ITicketRepository>().InstancePerLifetimeScope();
            
            builder.RegisterType<PaymentService>().As<IPaymentService>().InstancePerLifetimeScope();

            builder.RegisterType<FnbRepository>().As<IFnbRepository>().InstancePerLifetimeScope();
            builder.RegisterType<FnbService>().As<IFnbService>().InstancePerLifetimeScope();

            builder.RegisterType<ComboRepository>().As<IComboRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ComboService>().As<IComboService>().InstancePerLifetimeScope();
            builder.RegisterType<ComboDetailRepository>().As<IComboDetailRepository>().InstancePerLifetimeScope();
            
            builder.RegisterType<VoucherRepository>().As<IVoucherRepository>().InstancePerLifetimeScope();
            builder.RegisterType<VoucherService>().As<IVoucherService>().InstancePerLifetimeScope();

            builder.RegisterType<OrderDetailTicketRepository>().As<IOrderDetailTicketRepository>().InstancePerLifetimeScope();
            builder.RegisterType<OrderDetailComboRepository>().As<IOrderDetailComboRepository>().InstancePerLifetimeScope();
            
            builder.RegisterType<OrderDetailComboService>().As<IOrderDetailComboService>().InstancePerLifetimeScope();
            
            builder.RegisterType<QRCodeService>().As<IQRCodeService>().InstancePerLifetimeScope();

            builder.RegisterType<ExportExcelFileService>().As<IExportExcelFileService>().InstancePerLifetimeScope();
            builder.RegisterType<DashboardReportService>().As<IDashboardReportService>().InstancePerLifetimeScope();


            builder.RegisterType<CloudinaryService>().As<ICloudinaryService>().InstancePerLifetimeScope();

            builder.RegisterType<JwtService>().As<IJwtService>().InstancePerLifetimeScope();
            builder.RegisterType<EmailService>().As<IEmailService>().InstancePerLifetimeScope();
            builder.RegisterType<AuthService>().As<IAuthService>().InstancePerLifetimeScope();

            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();

            builder.RegisterType<StaffRepository>().As<IStaffRepository>().InstancePerLifetimeScope();

            builder.RegisterType<ConfigurationRepository>().As<IConfigurationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ConfigurationService>().As<IConfigurationService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();

            builder.RegisterType<RankRepository>().As<IRankRepository>().InstancePerLifetimeScope();

            builder.RegisterType<HangfireService>().As<IHangfireService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<StaffService>().As<IStaffService>().InstancePerLifetimeScope();
        }
    }
}
