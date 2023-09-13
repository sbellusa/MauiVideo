using MauiVideo.ViewModels;

namespace MauiVideo.Views;

public partial class MainPage : ContentPage
{
	MainPageViewModel ViewModel;

	public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = ViewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

		await ViewModel.InitialiseAsync();
    }
}

