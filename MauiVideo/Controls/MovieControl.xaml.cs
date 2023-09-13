using MauiVideo.Models;
using System.Windows.Input;

namespace MauiVideo.Controls;

public partial class MovieControl : ContentView
{
	public MovieControl()
	{
		InitializeComponent();
	}


    public static readonly BindableProperty MovieProperty = BindableProperty.Create(nameof(Movie), typeof(Movie), typeof(MovieInfoBox), null);
    public Movie Movie
    {
        get => (Movie)GetValue(MovieProperty);
        set => SetValue(MovieProperty, value);
    }


    public static readonly BindableProperty GoToDetailsCommandProperty =
    BindableProperty.Create(nameof(GoToDetailsCommand), typeof(ICommand), typeof(MovieInfoBox), null);
    public ICommand GoToDetailsCommand
    {
        get => (ICommand)GetValue(GoToDetailsCommandProperty);
        set => SetValue(GoToDetailsCommandProperty, value);
    }
}