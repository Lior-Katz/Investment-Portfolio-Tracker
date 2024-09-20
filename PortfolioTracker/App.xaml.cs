using System;
using PortfolioTracker.Services;
using PortfolioTracker.Stores;
using PortfolioTracker.ViewModels;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace PortfolioTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.AddDebug();
                })
                .ConfigureServices((hostContext, services) => { InitServices(services); }).Build();
        }


        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            AppHost.Services.GetRequiredService<NavigationStore>().CurrentViewModel =
                AppHost.Services.GetRequiredService<DashboardViewModel>();

            MainWindow = new MainWindow()
            {
                DataContext = AppHost.Services.GetRequiredService<MainViewModel>()
            };

            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            AppHost.Dispose();
            base.OnExit(e);
        }

        private static void InitServices(IServiceCollection services)
        {
            // String implementationTypeName = AppConfigManager.GetConfig("FinancialDataServiceImpl");
            // TODO: use appconfigmanager to get the implementation type
            String implementationTypeName = "PortfolioTracker.Services.MockFinancialDataService";
            Type? implementationType = Type.GetType(implementationTypeName);
            if (implementationType == null)
            {
                throw new InvalidOperationException($"Type {implementationTypeName} not found.");
            }
            services.AddSingleton(typeof(IFinancialDataService), implementationType);
            services.AddSingleton<PortfolioViewModel>(provider => DataService.RetrievePortfolio(2));
            services.AddSingleton<NavigationStore>();

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<DashboardViewModel>();
            services.AddSingleton<HoldingsListingViewModel>();
            services.AddSingleton<TransactionHistoryViewModel>();
            services.AddSingleton<AddTransactionViewModel>();
            services.AddSingleton<DistributionsViewModel>();
        }
    }
}
