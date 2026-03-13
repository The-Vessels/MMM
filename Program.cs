using Avalonia;
using System;

namespace avalonia_test;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        if (args.Length >= 1 && args[0] == "--test-steam")
        {
            Steam.Download();
        }
        else AvaloniaMain(args);
    }

    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void AvaloniaMain(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
