using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Transformation;
using Avalonia.Threading;

namespace mmm;

public partial class SubmissionPanel : UserControl
{
    public PreviewMedia? carouselImages;
    /* private bool carouselImagesLoaded = false; */

    public SubmissionPanel()
    {
        InitializeComponent();
    }
    public void Next(object source, RoutedEventArgs args)
        {
            ImagesCarousel.Next();
        }

    public void Previous(object source, RoutedEventArgs args)
    {
        ImagesCarousel.Previous();
    }

    bool moreInfoOpen = false;
    public async void ToggleMoreInfo(object source, RoutedEventArgs args)
    {
        if (!moreInfoOpen)
        {
            moreInfoOpen = true;

            MoreInfoDropdown.IsVisible = true;
            MoreInfoDropdown.Opacity = 1.0;

            PanelContent.CornerRadius = Avalonia.CornerRadius.Parse("2, 2, 0, 0");

            MoreInfoText.Text = "Less info";
            
            MoreInfoArrow.RenderTransform = TransformOperations.Parse("rotate(180deg)");
            MoreInfoDropdown.RenderTransform = TransformOperations.Parse("translateY(0px)");

            Dispatcher.UIThread.Post(async () => await LoadCarouselImages());
        } 
        else
        {
            moreInfoOpen = false;

            MoreInfoDropdown.Opacity = 0.0;

            PanelContent.CornerRadius = Avalonia.CornerRadius.Parse("2");

            MoreInfoText.Text = "More info";

            MoreInfoArrow.RenderTransform = TransformOperations.Parse("rotate(0deg)");
            MoreInfoDropdown.RenderTransform = TransformOperations.Parse("translateY(-20px)");

            //NOTE: Probably should use an Animation instead of Delay
            await Task.Delay(125);
            MoreInfoDropdown.IsVisible = false;

            ImagesCarousel.Items.Clear();
        }
    }

    public async Task LoadCarouselImages()
    {
        foreach (GBImage image in carouselImages.Images)
        {
            await AddCarouselImage(image);
        }
    }

    public async Task AddCarouselImage(GBImage image)
    {
        var imageUrl = new Uri(GameBanana.GetSubmissionImageUrlByImageSize(image, GameBanana.ImageSizes.Size530));
        var imageBitmap = await ImageHelper.LoadFromWeb(imageUrl);


        var carouselImage = new Image
        {
            Source = imageBitmap
        };

        var carouselCaption = new TextBlock
        {
            IsVisible = false
        };
        if (image.Caption != null)
        {
            carouselCaption = new TextBlock
            {
                IsVisible = true,
                Text = image.Caption,
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Bottom,
                Padding = Thickness.Parse("4"),
                Background = new SolidColorBrush
                {
                    Color = Color.FromArgb(175, 0, 0, 0)
                }
            };
        };

        ImagesCarousel.Items.Add(
            new Grid
            {
                Children =
                {
                    carouselImage,
                    carouselCaption
                }
            }
        );
    }
}
