namespace PartyRaidR.Mobile.Controls;

public partial class InputTextField : ContentView
{
    public static BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(InputTextField));
    public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }

    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(InputTextField));
    public string Placeholder { get => (string)GetValue(PlaceholderProperty); set => SetValue(PlaceholderProperty, value); }

    public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(InputTextField));
    public bool IsPassword { get => (bool)GetValue(IsPasswordProperty); set => SetValue(IsPasswordProperty, value); }

    public InputTextField()
	{
		InitializeComponent();
	}
}