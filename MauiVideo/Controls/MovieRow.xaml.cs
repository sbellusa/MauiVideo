using MauiVideo.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace MauiVideo.Controls;

public partial class MovieRow : ContentView
{
    public MovieRow()
    {
        InitializeComponent();
    }


    public static readonly BindableProperty HeadingProperty =
        BindableProperty.Create(nameof(Heading), typeof(string), typeof(MovieRow), string.Empty);

    public static readonly BindableProperty MoviesProperty =
        BindableProperty.Create(nameof(Movies), typeof(IEnumerable<Movie>), typeof(MovieRow), Enumerable.Empty<Movie>());

    public static readonly BindableProperty IsLargeProperty =
        BindableProperty.Create(nameof(IsLarge), typeof(bool), typeof(MovieRow), false);


    public string Heading
    {
        get { return (string)GetValue(MovieRow.HeadingProperty); }
        set { SetValue(MovieRow.HeadingProperty, value); }
    }

    public IEnumerable<Movie> Movies
    {
        get { return (IEnumerable<Movie>)GetValue(MovieRow.MoviesProperty); }
        set { SetValue(MovieRow.MoviesProperty, value); }
    }

    public bool IsLarge
    {
        get { return (bool)GetValue(MovieRow.IsLargeProperty); }
        set { SetValue(MovieRow.IsLargeProperty, value); }
    }

    public bool IsNotLarge => !IsLarge;



    public static readonly BindableProperty SelectedMovieCommandProperty =
        BindableProperty.Create(nameof(SelectedMovieCommand), typeof(ICommand), typeof(IconButton), null);
    public ICommand SelectedMovieCommand
    {
        get => (ICommand)GetValue(SelectedMovieCommandProperty);
        set => SetValue(SelectedMovieCommandProperty, value);
    }
}