using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
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
        //TODO: MAKE THIS FLIPPING PARALLEL FLIIIIIIIIIIIPPPPPPPP
        foreach (Record record in jsonResponse.Records)
        {
            var SubmissionPanel = new SubmissionPanel();
            SubmissionPanel.DataContext = record;

            foreach (string tag in record.Tags)
            {   
                var tagBlock = new TextBlock();
                tagBlock.Text = tag;
                SubmissionPanel.SubmissionTags.Children.Add(tagBlock);
            }

            var ThumbnailUrl = new Uri(GameBanana.GetSmallestSubmissionImageUrl(record.PreviewMedia.Images[0]));
            var ThumbnailBitmap = await ImageHelper.LoadFromWeb(ThumbnailUrl);

            SubmissionPanel.SubmissionThumbnail.Source = ThumbnailBitmap;

            if (record.HasFiles)
            {
                SubmissionsContainer.Children.Add(SubmissionPanel);
            }
        };
    }
}
