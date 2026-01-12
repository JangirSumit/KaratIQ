using KaratIQ.Data;
using KaratIQ.Services;
using KaratIQ.ViewModels;
using KaratIQ.Views;
using Microsoft.Extensions.Logging;

namespace KaratIQ;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddDbContext<KaratIQContext>();

        // Register Services
        builder.Services.AddScoped<BillingService>();

        // Register ViewModels
        builder.Services.AddTransient<InventoryViewModel>();
        builder.Services.AddTransient<BillingViewModel>();

        // Register Views
        builder.Services.AddTransient<InventoryPage>();
        builder.Services.AddTransient<BillingPage>();
        builder.Services.AddTransient<CustomersPage>();
        builder.Services.AddTransient<OrdersPage>();
        builder.Services.AddTransient<ReportsPage>();

        return builder.Build();
    }
}