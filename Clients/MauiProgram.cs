using Clients.Repository;
using Clients.ViewModel;
using Clients.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
using WinRT.Interop;

namespace Clients;

public static class MauiProgram
{
    private static SizeInt32 _mainWindowSize;

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

#if WINDOWS
        builder.ConfigureLifecycleEvents(events =>
        {
            events.AddWindows(wndLifeCycleBuilder =>
            {
                wndLifeCycleBuilder.OnWindowCreated(window =>
                {
                    var hWnd = WindowNative.GetWindowHandle(window);
                    var myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);
                    var appWindow = AppWindow.GetFromWindowId(myWndId);
                    var presenter = (appWindow.Presenter as OverlappedPresenter);

                    if (window.Title == "Clients")
                    {
                        presenter?.Maximize();
                        _mainWindowSize = appWindow.Size;
                    }
                    else
                    {
                        var halfWidth = _mainWindowSize.Width / 2;
                        var halfHeight = _mainWindowSize.Height / 2;
                        var centerX = (_mainWindowSize.Width - halfWidth) / 2;
                        var centerY = (_mainWindowSize.Height - halfHeight) / 2;

                        appWindow.Resize(new SizeInt32(halfWidth, halfHeight));
                        appWindow.Move(new PointInt32(centerX, centerY));
                    }
                });
            });
        });
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
        builder.Services.AddSingleton<ClientsViewModel>();
        builder.Services.AddSingleton<EditClientViewModel>();
        builder.Services.AddSingleton<AddClientViewModel>();

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
