using Avalonia;
using System;

namespace mmm;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        if (args.Length >= 1)
        {
            if (args[0] == "--test-utmt")
            {
                UTMTTest.Test("TestData/deltarune-ch4-mac.win");
            }
            else if (args[0] == "--test-xdelta")
            {
                XDeltaTest.Test();
            }
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
