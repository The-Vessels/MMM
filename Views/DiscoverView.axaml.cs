using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using HarfBuzzSharp;

namespace mmm;

public partial class DiscoverView : UserControl
{
    public DiscoverView()
    {
        InitializeComponent();
        _ = LoadFeaturedAsync();
    }

    private async Task AddSubmissionPanel(Record record)
    {
        var SubmissionPanel = new SubmissionPanel();
        SubmissionPanel.DataContext = record;

        foreach (string tag in record.Tags)
        {   
            var tagBlock = new TextBlock();
            tagBlock.Text = tag;
            SubmissionPanel.SubmissionTags.Children.Add(tagBlock);
        }

        var ThumbnailUrl = new Uri(GameBanana.GetSubmissionImageUrlByImageSize(record.PreviewMedia.Images[0], GameBanana.ImageSizes.Size220));
        var ThumbnailBitmap = await ImageHelper.LoadFromWeb(ThumbnailUrl);

        SubmissionPanel.SubmissionThumbnail.Background = new ImageBrush
        {
            Source = ThumbnailBitmap,
            Stretch = Stretch.UniformToFill,
            AlignmentX = AlignmentX.Center,
            AlignmentY = AlignmentY.Top
        };

        //TODO: why no blur?
        var ThumbnailBackgroundImage = new Image
        {
            Source = ThumbnailBitmap,
            Effect = new BlurEffect
            {
                Radius = 1000
            }
        };

        SubmissionPanel.PanelBackgroundImage.Background = new VisualBrush
        {
            Visual = ThumbnailBackgroundImage,
            Stretch = Stretch.UniformToFill,
            AlignmentX = AlignmentX.Center,
            AlignmentY = AlignmentY.Center
        };

        if (record.HasFiles)
        {
            SubmissionsContainer.Children.Add(SubmissionPanel);
        }
    }

    private async Task LoadFeaturedAsync()
    {
        SubmissionItem jsonResponse = await GameBanana.GetFeaturedSubmissions(GameBanana.client);
        foreach (Record record in jsonResponse.Records)
        {
            Dispatcher.UIThread.Post(async () => await AddSubmissionPanel(record));
        }
    }
}
