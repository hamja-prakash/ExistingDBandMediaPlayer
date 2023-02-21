using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Storage;
using System.Text;
using System.Threading;

namespace samplemaui;

public partial class FileSaverDemo : ContentPage
{
	IFileSaver fileSaver;
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

	public FileSaverDemo()
	{
		InitializeComponent();
        this.fileSaver = FileSaver.Default;
    }

    private async void BtnFileSaver_Clicked(object sender, EventArgs e)
    {
		try
		{
            using var stream = new MemoryStream(Encoding.Default.GetBytes("Welcome to the MAUI"));
			var path = await fileSaver.SaveAsync("Test.txt", stream, cancellationTokenSource.Token);
			await App.Current.MainPage.DisplayAlert("Success", $"Path : {path}", "Ok", "Cancel");
        }
		catch (Exception ex)
		{
			var err = ex.Message;
		}
    }
}