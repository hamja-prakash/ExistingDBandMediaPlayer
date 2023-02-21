using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;

namespace samplemaui;

public partial class MediaElementPage : ContentPage
{
	public MediaElementPage()
	{
		InitializeComponent();
	}

    private void btnPlay_Clicked(object sender, EventArgs e)
    {
		try
		{
			if (mediaelement.CurrentState == CommunityToolkit.Maui.Core.Primitives.MediaElementState.Playing)
			{
				mediaelement.Pause();
				btnPlay.Text = "Pause";
			}
			else if (mediaelement.CurrentState == CommunityToolkit.Maui.Core.Primitives.MediaElementState.Paused)
			{
				mediaelement.Play();
				btnPlay.Text = "Play";
			}
            else if (mediaelement.CurrentState == CommunityToolkit.Maui.Core.Primitives.MediaElementState.Stopped)
            {
                mediaelement.Play();
                btnPlay.Text = "Play";
            }
        }
		catch (Exception ex)
		{
			throw;
		}
	}

    private void btnStop_Clicked(object sender, EventArgs e)
    {
		try
		{
			mediaelement.Stop();
		}
		catch (Exception ex)
		{
			throw;
		}
    }
}