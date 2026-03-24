using System;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace mmm;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();

        UsefulButton.Click += async (_, _) =>
        {
            var data = await Task.Run(() => UTMTTest.Test("TestData/deltarune-ch4-mac.win"));
            if (data == null)
            {
                Console.WriteLine("data is null. useless.");
                return;
            }
            Console.WriteLine($"Data Exists!!! ({data})");
        };
    }
}
