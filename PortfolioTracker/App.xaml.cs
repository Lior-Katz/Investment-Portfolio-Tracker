using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PortfolioTracker.Services;
using PortfolioTracker.Stores;
using PortfolioTracker.ViewModels;

namespace PortfolioTracker;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
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

    public static IHost? AppHost { get; private set; }


    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        AppHost.Services.GetRequiredService<IFinancialDataService>()
               .CompleteHistory(AppHost.Services.GetRequiredService<PortfolioViewModel>());

        AppHost.Services.GetRequiredService<NavigationStore>().CurrentViewModel =
            AppHost.Services.GetRequiredService<DashboardViewModel>();

        MainWindow = new MainWindow
                     {
                         DataContext = AppHost.Services.GetRequiredService<MainViewModel>()
                     };

        MainWindow.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        saveHistoryOnExit(AppHost.Services.GetRequiredService<PortfolioViewModel>());
        await AppHost!.StopAsync();
        AppHost.Dispose();
        base.OnExit(e);
    }

    private void saveHistoryOnExit(PortfolioViewModel portfolio)
    {
    }

    private static void InitServices(IServiceCollection services)
    {
        // String implementationTypeName = AppConfigManager.GetConfig("FinancialDataServiceImpl");
        // TODO: use appconfigmanager to get the implementation type
        var implementationTypeName = "PortfolioTracker.Services.MockFinancialDataService";
        var implementationType = Type.GetType(implementationTypeName);
        if (implementationType == null)
            throw new InvalidOperationException($"Type {implementationTypeName} not found.");

        services.AddSingleton(typeof(IFinancialDataService), implementationType);
        services.AddSingleton<PortfolioViewModel>(provider => DataService.InitPortfolio(2));
        services.AddSingleton<NavigationStore>();

        services.AddSingleton<MainViewModel>();
        services.AddSingleton<DashboardViewModel>();
        services.AddSingleton<HoldingsListingViewModel>();
        services.AddSingleton<TransactionHistoryViewModel>();
        services.AddSingleton<AddTransactionViewModel>();
        services.AddSingleton<DistributionsViewModel>();
    }
}
