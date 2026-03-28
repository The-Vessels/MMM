using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Transformation;

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
        }
    }
}
