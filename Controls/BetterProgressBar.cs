using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace mmm;

/// <summary>
/// This is like ProgressBar, but allows for text with a border.
/// </summary>
public class BetterProgressBar : ProgressBar
{
    static BetterProgressBar()
    {
        // DefaultStyleKeyProper
    }

    // public static readonly StyledProperty<IPen?> BorderPenProperty =
    //     AvaloniaProperty.Register<BetterProgressBar, IPen?>(nameof(BorderPen), null);
    // public IPen? BorderPen
    // {
    //     get => GetValue(BorderPenProperty);
    //     set => SetValue(BorderPenProperty, value);
    // }

    // public double ProgressWidth
    // {
    //     get => (Value / 100.0) * Width;
    // }

    // public override void Render(DrawingContext context)
    // {
    //     base.Render(context);

    //     context.DrawRectangle(Background, null, new Rect(0.0, 0.0, Width, Height));

    //     // var typeface = new Typeface(FontFamily);

    //     // var text = new FormattedText(
    //     //     "the text.",
    //     //     CultureInfo.CurrentCulture,
    //     //     FlowDirection.LeftToRight,
    //     //     typeface,
    //     //     32,
    //     //     Brushes.White
    //     // );

    //     // var geometry = text.BuildGeometry(new (10.0, 0.0));
    //     // if (geometry == null)
    //     // {
    //     //     Console.WriteLine("NOOO");
    //     //     return;
    //     // }


    //     // context.DrawGeometry(null, BorderPen, geometry);
    //     // context.DrawGeometry(Brushes.Black, null, geometry);
    // }
}
