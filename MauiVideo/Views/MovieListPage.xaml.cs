using MauiVideo.ViewModels;

namespace MauiVideo.Views;

public partial class MovieListPage : ContentPage
{
    public MovieListViewModel ViewModel { get; }

    public MovieListPage(MovieListViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = ViewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await ViewModel.InitialiseAsync();
    }

    // TODO : Workaround for RemainingItemsThresholdReachedCommand not being triggered on Windows 
    // This also not working on iOS
    private void CollectionView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
    {        
        if (DeviceInfo.Current.Platform != DevicePlatform.WinUI)
        {
            return;
        }

        // This is temporarily disabled as it's spamming requests way too often, and before scrolling to the end
        //if (sender is CollectionView cv && cv is IElementController element)
        //{
        //    var count = element.LogicalChildren.Count;
        //    if (e.LastVisibleItemIndex + 1 - count + cv.RemainingItemsThreshold >= 0)
        //    {
        //        if (cv.RemainingItemsThresholdReachedCommand.CanExecute(null))
        //        {
        //            cv.RemainingItemsThresholdReachedCommand.Execute(null);
        //        }
        //    }
        //}
    }
}