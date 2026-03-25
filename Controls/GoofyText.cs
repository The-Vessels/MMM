using System;
using System.Diagnostics;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace mmm;

public class GoofyText : ProgressBar
{
    public static readonly StyledProperty<IPen?> BorderPenProperty =
        AvaloniaProperty.Register<GoofyText, IPen?>(nameof(BorderPen), null);
    public IPen? BorderPen
    {
        get => GetValue(BorderPenProperty);
        set => SetValue(BorderPenProperty, value);
    }
    
    public override void Render(DrawingContext context)
    {
        base.Render(context);

        var typeface = new Typeface(FontFamily);

        var text = new FormattedText(
            "the text.",
            CultureInfo.CurrentCulture,
            FlowDirection.LeftToRight,
            typeface,
            32,
            Brushes.White
        );

        var geometry = text.BuildGeometry(new (10.0, 0.0));
        if (geometry == null)
        {
            Console.WriteLine("NOOO");
            return;
        }


        context.DrawGeometry(null, BorderPen, geometry);
        context.DrawGeometry(Brushes.Black, null, geometry);
    }
}
