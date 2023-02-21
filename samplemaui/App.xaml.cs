using MetroLog.Maui;
using samplemaui.Helpers;
using System.Reflection;

namespace samplemaui;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        SetupExistingDB();

        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(BorderlessEntry), (handler, view) =>
        {
#if __ANDROID__
            //handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
            handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#elif __IOS__
            handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;

#endif
        });
        MainPage = new AppShell();

        LogController.InitializeNavigation(
            page => MainPage!.Navigation.PushModalAsync(page),
            () => MainPage!.Navigation.PopModalAsync());
    }

    public void SetupExistingDB()
    {
        try
        {
            // TODO Only do this when app first runs
            bool isDBExist = File.Exists(EmployeeRepository.DbPath);
            if(!isDBExist)
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
                using (Stream stream = assembly.GetManifestResourceStream("samplemaui.Resources.Raw.chinook.db"))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        stream.CopyTo(memoryStream);
                        File.WriteAllBytes(EmployeeRepository.DbPath, memoryStream.ToArray());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            var err = ex.Message;
        }
    }
}
