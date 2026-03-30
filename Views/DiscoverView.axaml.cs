using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia;
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

    private async Task AddSubmissionPanelThumbnail(SubmissionPanel submissionPanel, Uri thumbnailUrl)
    {
        var thumbnailBitmap = await GameBanana.DownloadImage(thumbnailUrl);

        submissionPanel.SubmissionThumbnail.Background = new ImageBrush
        {
            Source = thumbnailBitmap,
            Stretch = Stretch.UniformToFill,
            AlignmentX = AlignmentX.Center,
            AlignmentY = AlignmentY.Top
        };

        submissionPanel.PanelBackgroundImage.Source = thumbnailBitmap;
    }

    private async Task AddRemainingSubmissionInfo(SubmissionPanel submissionPanel, Record record)
    {
        Record remainingData = await GameBanana.GetRecordByModelNameAndRow(record.ModelName, record.Row);
        submissionPanel.DataContext = remainingData;

        if (remainingData.Category != null)
        {
            submissionPanel.Category.IsVisible = true;
        }
        if (remainingData.SuperCategory != null)
        {
            submissionPanel.SuperCategory.IsVisible = true;
        }
        if (submissionPanel.Category.IsVisible || submissionPanel.SuperCategory.IsVisible)
        {
            submissionPanel.Categories.IsVisible = true;
        }
        
        if (remainingData.Description != null)
        {
            submissionPanel.Description.IsVisible = true;
        }

        if (remainingData.Credits?.Count > 0)
        {
            submissionPanel.SubmissionCredits.IsVisible = true;
        }
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

        if (record.HasFiles)
        {
            SubmissionsContainer.Children.Add(submissionPanel);
        }

        // This is so that image-downloading can be done asynchronously
        // (this makes all the images get downloaded at the same time)
        Dispatcher.UIThread.Post(async () => await AddSubmissionPanelThumbnail(submissionPanel, thumbnailUrl));

        // Haven't found a way to get all the info we need from one API call so uhm we have to do this
        Dispatcher.UIThread.Post(async () => await AddRemainingSubmissionInfo(submissionPanel, record));

        /* Dispatcher.UIThread.Post(async () => await AddSubmissionPanelCarouselImages(submissionPanel, record.PreviewMedia)); */
        submissionPanel.carouselImages = record.PreviewMedia;
    }

    private async Task LoadFeaturedAsync()
    {
        SubmissionItem jsonResponse = await GameBanana.GetFeaturedSubmissions();
        foreach (Record record in jsonResponse.Records)
        {
            // Dispatcher.UIThread.Post(async () => await AddSubmissionPanel(record));
            await AddSubmissionPanel(record);
        }
    }
}
