using Microsoft.Maui.Controls.Handlers.Items;

namespace MauiVideo;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();


        // Workaround to fix Headers and Footers not being displayed on CollectionViews
        CollectionViewHandler.Mapper.AppendToMapping("HeaderAndFooterFix", (_, collectionView) =>
        {
            collectionView.AddLogicalChild(collectionView.Header as Element);
            collectionView.AddLogicalChild(collectionView.Footer as Element);
        });
    }


    /// <summary>
    /// Set window size on Windows
    /// </summary>
    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);

        const int newWidth = 500;
        const int newHeight = 1000;

        window.Width = newWidth;
        window.Height = newHeight;

        return window;
    }
}
