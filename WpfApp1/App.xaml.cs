using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Services;
using WpfApp1.ViewModels;
using WpfApp1.Views;

namespace WpfApp1
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            Services = serviceCollection.BuildServiceProvider();  // This allows you to access services

            var mainWindow = Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICryptoService, CryptoService>();
            services.AddSingleton<MainWindow>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<CryptoDetailsPage>();  // Register the CryptoDetailsPage
            services.AddTransient<CryptoDetailsViewModel>();  // Register the CryptoDetailsViewModel
        }
    }
}