using System.Windows.Input;

namespace MauiVideo.Controls;

public partial class IconButton : ContentView
{
	public IconButton()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty IconProperty = 
        BindableProperty.Create(nameof(Icon), typeof(ImageSource), typeof(IconButton), null);
    public ImageSource Icon
    {
        get => (ImageSource)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }



    public static readonly BindableProperty StrokeProperty = 
        BindableProperty.Create(nameof(Stroke), typeof(Color), typeof(IconButton), null);
    public Color Stroke
    {
        get => (Color)GetValue(StrokeProperty);
        set => SetValue(StrokeProperty, value);
    }



    public static readonly BindableProperty LabelProperty = 
        BindableProperty.Create(nameof(Label), typeof(string), typeof(IconButton), null);
    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }


    public enum ButtonOrientations { Vertical, Horizontal };
    public static readonly BindableProperty OrientationProperty = 
        BindableProperty.Create(nameof(Orientation), typeof(ButtonOrientations), typeof(IconButton), ButtonOrientations.Vertical);
    public ButtonOrientations Orientation
    {
        get => (ButtonOrientations)GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }




    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(IconButton), null);
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }


}