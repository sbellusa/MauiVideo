using MauiVideo.ViewModels;

namespace MauiVideo.Views;

public partial class CategoriesPage : ContentPage
{
    CategoriesViewModel ViewModel;

    public CategoriesPage(CategoriesViewModel viewModel)
    {
        InitializeComponent();

        this.BindingContext = ViewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Task.Run(ViewModel.InitialiseAsync);
    }
}