using CommunityToolkit.Maui;
using MetroLog.MicrosoftExtensions;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter;
using Microsoft.Extensions.Logging;

namespace samplemaui;

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
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseMauiCommunityToolkitMediaElement()
            .UseMauiCommunityToolkit();

        AppCenter.Start("windowsdesktop={Your Windows App secret here};" +
                "android={Your Android App secret here};" +
                "ios=58b6c330-14ce-4f2f-b9bb-3fd6e5dc3ae4;" +
                "macos={Your macOS App secret here};",
                typeof(Analytics), typeof(Crashes));

        builder.Logging.AddTraceLogger(_ => { });
        builder.Logging.AddInMemoryLogger(_ => { });
        builder.Logging.AddStreamingFileLogger(_ => { });
        builder.Services.AddTransient<MainPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
