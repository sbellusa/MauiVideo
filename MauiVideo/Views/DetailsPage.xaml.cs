using MauiVideo.ViewModels;

namespace MauiVideo.Views;

public partial class DetailsPage : ContentPage
{
    public DetailsViewModel ViewModel { get; }

    public DetailsPage(DetailsViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = ViewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await ViewModel.InitialiseAsync();
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        if (width > 0)
        {
            int columns = (int)(width / 190) + 1;

            ViewModel.SimilarItemsWidth = (width / columns) -4;
        }
    }
}