using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Threading;

namespace mmm;

public partial class DiscoverView : UserControl
{
    public DiscoverView()
    {
        InitializeComponent();
        _ = LoadFeaturedAsync();
    }

    private async Task AddSubmissionPanelImage(SubmissionPanel submissionPanel, Uri thumbnailUrl)
    {
        var thumbnailBitmap = await ImageHelper.LoadFromWeb(thumbnailUrl);

        submissionPanel.SubmissionThumbnail.Background = new ImageBrush
        {
            Source = thumbnailBitmap,
            Stretch = Stretch.UniformToFill,
            AlignmentX = AlignmentX.Center,
            AlignmentY = AlignmentY.Top
        };

        submissionPanel.PanelBackgroundImage.Source = thumbnailBitmap;
    }

    private async Task AddSubmissionPanel(Record record)
    {
        var submissionPanel = new SubmissionPanel();
        submissionPanel.DataContext = record;

        /* foreach (string tag in record.Tags)
        {   
            var tagBlock = new TextBlock();
            tagBlock.Text = tag;
            submissionPanel.SubmissionTags.Children.Add(tagBlock);
        } */

        var thumbnailUrl = new Uri(GameBanana.GetSubmissionImageUrlByImageSize(record.PreviewMedia.Images[0], GameBanana.ImageSizes.Size220));

        if (record.DevelopmentStateAbbr == "indev" && record.CompletionPercentage < 100)
        {
            submissionPanel.ProgressStats.IsVisible = true;
        }
        if (record.Tags.Count > 0)
        {
            submissionPanel.SubmissionTags.IsVisible = true;
        }

        if (record.HasFiles)
        {
            SubmissionsContainer.Children.Add(submissionPanel);
        }

        // This is so that image-downloading can be done asynchronously
        // (this makes all the images get downloaded at the same time)
        Dispatcher.UIThread.Post(async () => await AddSubmissionPanelImage(submissionPanel, thumbnailUrl));
    }

    private async Task LoadFeaturedAsync()
    {
        SubmissionItem jsonResponse = await GameBanana.GetFeaturedSubmissions(GameBanana.client);
        foreach (Record record in jsonResponse.Records)
        {
            // Dispatcher.UIThread.Post(async () => await AddSubmissionPanel(record));
            await AddSubmissionPanel(record);
        }
    }
}
