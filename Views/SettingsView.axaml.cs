using Avalonia.Controls;

namespace mmm;

public partial class SettingsView : UserControl
{
    private readonly SettingsService _settings;
    public SettingsView()
    {
        InitializeComponent();
        _settings = new SettingsService();
        var s = _settings.Load();

        SystemDecoToggle.IsChecked = s.UseSystemDeco;
        SystemDecoToggle.IsCheckedChanged += (_, _) =>
        {
            var s = _settings.Load();
            s.UseSystemDeco = (bool)SystemDecoToggle.IsChecked;
            _settings.Save(s);
        };
    }
}
