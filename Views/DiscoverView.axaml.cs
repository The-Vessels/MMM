using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Threading;

namespace mmm;

public partial class DiscoverView : UserControl
{
    public void Next(object source, RoutedEventArgs args)
    {
        TopSubmissionsCarousel.Next();
    }

    public void Previous(object source, RoutedEventArgs args)
    {
        TopSubmissionsCarousel.Previous();
    }
    
    public DiscoverView()
    {
        InitializeComponent();

        _ = LoadFeaturedAsync();
        _ = LoadTopAsync();
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

        if (remainingData.DevelopmentStateAbbr == "indev" && remainingData.CompletionPercentage < 100)
        {
            submissionPanel.ProgressStats.IsVisible = true;
        }
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

        if (remainingData.PreviewMedia != null)
        {
            var thumbnailUrl = new Uri(GameBanana.GetSubmissionImageUrlByImageSize(remainingData.PreviewMedia.Images[0], GameBanana.ImageSizes.Size220));

            // This is so that image-downloading can be done asynchronously
            // (this makes all the images get downloaded at the same time)
            Dispatcher.UIThread.Post(async () => await AddSubmissionPanelThumbnail(submissionPanel, thumbnailUrl));

            submissionPanel.carouselImages = remainingData.PreviewMedia;
        }
    }

    private async Task<SubmissionPanel> GetSubmissionPanel(Record record)
    {
        var submissionPanel = new SubmissionPanel();
        submissionPanel.DataContext = record;

        // Haven't found a way to get all the info we need from one API call so uhm we have to do this
        Dispatcher.UIThread.Post(async () => await AddRemainingSubmissionInfo(submissionPanel, record));

        return submissionPanel;
    }

    private async Task LoadFeaturedAsync()
    {
        SubmissionsResult jsonResponse = await GameBanana.GetFeaturedSubmissions();
        foreach (Record record in jsonResponse.Records)
        {
            var subPanel = await GetSubmissionPanel(record);
            if (record.HasFiles)
            {
                FeaturedSubmissionsContainer.Children.Add(subPanel);
            }
        }
    }

    private async Task LoadTopAsync()
    {
        List<Record> jsonResponse = await GameBanana.GetTopSubmissions();
        foreach (Record record in jsonResponse)
        {
            var subPanel = await GetSubmissionPanel(record);

            //TODO: someone please help me figure out how to make topsubs without files not get added
            if (record.ModelName == "Mod" || record.ModelName == "Wip")
            {
                TopSubmissionsCarousel.Items.Add(subPanel);
            }
        }
    }
}
