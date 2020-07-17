using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Microsoft.Extensions.DependencyInjection;
using MijnSauna.Frontend.Phone.Services.Interfaces;
using MijnSauna.Frontend.Phone.Droid.Services;
using MijnSauna.Frontend.Phone.DependencyInjection;

namespace MijnSauna.Frontend.Phone.Droid
{
    [Activity(Label = "Mijn Sauna", Icon = "@mipmap/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            
            IServiceCollection container = new ServiceCollection();
            container.AddXamarin();
            container.AddSingleton<App>();
            container.AddSingleton<IStatusBarService>(new StatusBarService(Window));
            container.AddSingleton<ISoundService>(new SoundService(this));
            var mediaService = new MediaService();
            container.AddSingleton<IMediaService>(mediaService);
            var provider = container.BuildServiceProvider();

            var intentFilter = new IntentFilter();
            intentFilter.AddAction("com.android.music.metachanged");
            intentFilter.AddAction("com.android.music.playstatechanged");
            intentFilter.AddAction("com.android.music.playbackcomplete");
            intentFilter.AddAction("com.android.music.queuechanged");
            RegisterReceiver(mediaService, intentFilter);

            var app = provider.GetService<App>();
            LoadApplication(app);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}