using Clients.Repository;
using Clients.ViewModel;
using Clients.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace Clients;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .RegisterViews()
            .RegisterDatabaseContext()
            .RegisterRepositories()
            .RegisterViewModels()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    private static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<ClientsPage>();
        builder.Services.AddTransient<EditClientPage>();
        builder.Services.AddTransient<AddClientPage>();

        return builder;
    }

    private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<ClientsViewModel>();
        builder.Services.AddTransient<EditClientViewModel>();
        builder.Services.AddTransient<AddClientViewModel>();

        return builder;
    }

    private static MauiAppBuilder RegisterRepositories(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<IClientRepository, ClientRepository>();

        return builder;
    }


    private static MauiAppBuilder RegisterDatabaseContext(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<ApplicationDbContext>();

        return builder;
    }
}
