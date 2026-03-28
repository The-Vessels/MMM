using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace mmm;

public partial class SubmissionPanel : UserControl
{
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
    public void ToggleMoreInfo(object source, RoutedEventArgs args)
    {
        if (!moreInfoOpen)
        {
            MoreInfoDropdown.IsVisible = true;
            moreInfoOpen = true;

            PanelContent.CornerRadius = Avalonia.CornerRadius.Parse("2, 2, 0, 0");

            MoreInfoText.Text = "Less info";
            MoreInfoArrow.RenderTransform = new RotateTransform
            {
                Angle = 180.0
            };
        } 
        else
        {
            MoreInfoDropdown.IsVisible = false;
            moreInfoOpen = false;

            PanelContent.CornerRadius = Avalonia.CornerRadius.Parse("2");

            MoreInfoText.Text = "More info";
            MoreInfoArrow.RenderTransform = new RotateTransform
            {
                Angle = 0.0
            };
        }
    }
}
