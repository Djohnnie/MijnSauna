using Android.App;
using Android.Content.PM;
using Android.OS;

namespace MijnSauna.Frontend.Maui
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        private readonly Android.Graphics.Color _navigationColor = Android.Graphics.Color.Rgb(0, 0, 0);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Window.SetNavigationBarColor(_navigationColor);

            base.OnCreate(savedInstanceState);
        }
    }
}