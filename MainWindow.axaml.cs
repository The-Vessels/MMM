using System;
using System.Data;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using Avalonia.Media;

namespace mmm;

public partial class MainWindow : Window
{
    // DateTime? lastTitleBarClick = null;
    // const int MAX_DOUBLECLICK_MS = 20;

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

        Width = Screens.All[0].Bounds.Width * 0.65;
        Height = Screens.All[0].Bounds.Height * 0.75;

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
        discoverView = new();
        librarbyView = new();
        settingsView = new();

        ViewContainer.Child = homeView;
        HomeButton.Click += (_, _) => ViewContainer.Child = homeView;
        DiscoverButton.Click += (_, _) => ViewContainer.Child = discoverView;
        LibrarbyButton.Click += (_, _) => ViewContainer.Child = librarbyView;
        SettingsButton.Click += (_, _) => ViewContainer.Child = settingsView;
    }
}