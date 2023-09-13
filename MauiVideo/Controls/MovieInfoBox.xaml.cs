namespace MauiVideo.Controls;
using Models;
using System.Windows.Input;

public partial class MovieInfoBox : ContentView
{
    public MovieInfoBox()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty MovieProperty = BindableProperty.Create(nameof(Movie), typeof(Movie), typeof(MovieInfoBox), null);

    public Movie Movie
    {
        get => (Movie)GetValue(MovieProperty);
        set => SetValue(MovieProperty, value);
    }



    public static readonly BindableProperty CloseCommandProperty =
    BindableProperty.Create(nameof(CloseCommand), typeof(ICommand), typeof(MovieInfoBox), null);
    public ICommand CloseCommand
    {
        get => (ICommand)GetValue(CloseCommandProperty);
        set => SetValue(CloseCommandProperty, value);
    }




    public static readonly BindableProperty GoToDetailsCommandProperty =
    BindableProperty.Create(nameof(GoToDetailsCommand), typeof(ICommand), typeof(MovieInfoBox), null);
    public ICommand GoToDetailsCommand
    {
        get => (ICommand)GetValue(GoToDetailsCommandProperty);
        set => SetValue(GoToDetailsCommandProperty, value);
    }
}