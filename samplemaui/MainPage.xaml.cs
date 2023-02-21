
using CommunityToolkit.Maui.Storage;
using MetroLog.Maui;
using Microsoft.AppCenter.Analytics;
using Microsoft.Extensions.Logging;

namespace samplemaui;

public partial class MainPage : ContentPage
{
	int count = 0;
    ILogger<MainPage> _logger;

    public MainPage(ILogger<MainPage> logger)
	{
		InitializeComponent();
        orientationLabel.Text = new DeviceOrientationService().GetOrientation().ToString();
        BindingContext = new LogController();

        _logger = logger;
    }

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;
		string message;

        if (count == 1)
            message = $"Clicked {count} time";
		else
            message = $"Clicked {count} times";

        CounterBtn.Text = message;

        Analytics.TrackEvent("Counter", new Dictionary<string, string> { { "Counter", count.ToString() } });

        _logger.LogInformation($"Button clicked. Count: {message}");

        SemanticScreenReader.Announce(CounterBtn.Text);

        var snackbar = CommunityToolkit.Maui.Alerts.Snackbar.Make(message, () => DisplayAlert("Test", "this is testing", "OK"), "YES!",
            TimeSpan.FromSeconds(10), new CommunityToolkit.Maui.Core.SnackbarOptions
            {
                BackgroundColor = Colors.Red,
                TextColor = Colors.White,
            }, img);

        snackbar.Show();
    }

    private void BtnNavVideoPage(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new MediaElementPage());
        }
        catch (Exception ex)
        {
            var err = ex.Message;
        }
    }

    private void BtnFileSaver_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new FileSaverDemo());
        }
        catch (Exception ex)
        {
            var err = ex.Message;
        }
    }

    private void BtnExistingDB_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new ExistingDBDemo());
        }
        catch (Exception ex)
        {
            var err = ex.Message;
        }
    }
}

