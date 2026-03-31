using System;
using System.Data;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace mmm;

public partial class MainWindow : Window
{
    private readonly SettingsService _settings;

    // TODO should we make this more generic?
    HomeView homeView;
    DiscoverView discoverView;
    LibrarbyView librarbyView;
    SettingsView settingsView;

    void ToggleMaximize()
    {
        if (WindowState == WindowState.Normal)
        {
            WindowState = WindowState.Maximized;
        }
        else if (WindowState == WindowState.Maximized)
        {
            WindowState = WindowState.Normal;
        }
    }

    public MainWindow()
    {
        InitializeComponent();
        _settings = new SettingsService();
        var s = _settings.Load();
        if (s.UseSystemDeco)
        {
            SystemDecorations = SystemDecorations.Full;
            TitleBar.IsVisible = false;
            Grid.SetRowSpan(Sidebar, 2);
            Grid.SetRow(Sidebar, 0);
            Grid.SetRowSpan(ViewContainer, 2);
            Grid.SetRow(ViewContainer, 0);
            Grid.SetRowSpan(WindowBgColor, 2);
            Grid.SetRow(WindowBgColor, 0);
            WindowBorder.BorderThickness = Thickness.Parse("0");
        }

        Width = 640 * 2.0;
        Height = 480 * 1.6;

        TitleBar.PointerPressed += (sender, args) =>
        {
            if (args.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            {
                BeginMoveDrag(args);
            }
        };
        MinimizeBtn.Click += (_, _) => WindowState = WindowState.Minimized;
        MaximizeBtn.Click += (_, _) =>
            WindowState = (WindowState == WindowState.FullScreen) ? WindowState.Normal : WindowState.FullScreen;
        CloseWinBtn.Click += (_, _) => Close();

        Resized += (_, _) =>
        {
            Console.WriteLine(TitleBar.Bounds);
        };

        homeView = new();
        ViewContainer.Child = homeView;
        Title = "Home - MantleModManager";
        HomeButton.Classes.Add("Highlighted");

        HomeButton.Click += (_, _) =>
        {
            ViewContainer.Child = homeView;
            Title = "Home - MantleModManager";
            HomeButton.Classes.Add("Highlighted");
            DiscoverButton.Classes.Remove("Highlighted");
            LibrarbyButton.Classes.Remove("Highlighted");
            SettingsButton.Classes.Remove("Highlighted");
        };
        DiscoverButton.Click += (_, _) =>
        {
            if (discoverView == null)
            {
                discoverView = new();
            }
            ViewContainer.Child = discoverView;
            Title = "Discover - MantleModManager";
            HomeButton.Classes.Remove("Highlighted");
            DiscoverButton.Classes.Add("Highlighted");
            LibrarbyButton.Classes.Remove("Highlighted");
            SettingsButton.Classes.Remove("Highlighted");
        };
        LibrarbyButton.Click += (_, _) =>
        {
            if (librarbyView == null)
            {
                librarbyView = new();
            }
            ViewContainer.Child = librarbyView;
            Title = "Librarby - MantleModManager";
            HomeButton.Classes.Remove("Highlighted");
            DiscoverButton.Classes.Remove("Highlighted");
            LibrarbyButton.Classes.Add("Highlighted");
            SettingsButton.Classes.Remove("Highlighted");
        };
        SettingsButton.Click += (_, _) =>
        {
            if (settingsView == null)
            {
                settingsView = new();
            }
            ViewContainer.Child = settingsView;
            Title = "Settings - MantleModManager";
            HomeButton.Classes.Remove("Highlighted");
            DiscoverButton.Classes.Remove("Highlighted");
            LibrarbyButton.Classes.Remove("Highlighted");
            SettingsButton.Classes.Add("Highlighted");
        };
    }
}