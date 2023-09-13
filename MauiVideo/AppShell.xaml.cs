using MauiVideo.Views;
using Microsoft.Maui.Dispatching;

namespace MauiVideo;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Navigated += Current_Navigated;
    }

    private void Current_Navigated(object sender, ShellNavigatedEventArgs e)
    {
        BackButton.IsVisible = Shell.Current.Navigation.NavigationStack.Count > 1;
    }

    private async void BackButton_Pressed(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    private async void LogoButton_Pressed(object sender, EventArgs e)
    {
        while (Navigation.NavigationStack.Count > 1)
        {
            Navigation.RemovePage(Navigation.NavigationStack[1]);
        }

        CurrentItem = MainPageContent;

        await Shell.Current.Navigation.PopToRootAsync();
    }
}
