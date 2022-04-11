using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestApiDoc.Database;
using RestApiDoc.ViewModels;
using RestApiDoc.Views;
using System.Windows;

namespace RestApiDoc
{
    public partial class App : Application
    {
        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            IocService.Initialize(services);
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RestAppDb;Trusted_connection=true");
            }, ServiceLifetime.Transient);

            services.AddSingleton<MainWindow>();
            services.AddTransient<TestsWindow>();
            services.AddTransient<AdminWindow>();
            services.AddTransient<AuthWindow>();

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<AdminViewModel>();

            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<RegisterViewModel>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            IocService.Get<MainWindow>()?.Show();
        }
    }
}