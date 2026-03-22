using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Controls;
using HarfBuzzSharp;

namespace mmm;

public partial class DiscoverView : UserControl
{
    public DiscoverView()
    {
        InitializeComponent();
        _ = LoadFeaturedAsync();
    }

    private async Task LoadFeaturedAsync()
    {
        SubmissionItem jsonResponse = await GameBanana.GetFeaturedSubmissions(GameBanana.client);
        foreach (Record record in jsonResponse.aRecords)
        {
            var SubmissionPanel = new SubmissionPanel();
            SubmissionPanel.DataContext = record;

            foreach (string tag in record.Tags)
            {   
                var tagBlock = new TextBlock();
                tagBlock.Text = tag;
                SubmissionPanel.SubmissionTags.Children.Add(tagBlock);
            }

            /* Console.WriteLine($"hi {record}\n"); */

            if (record.HasFiles)
            {
                SubmissionsContainer.Children.Add(SubmissionPanel);
            }
        };
    }
}
