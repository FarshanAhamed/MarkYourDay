
using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.Permissions;
using ButtonCircle.FormsPlugin.Droid;


namespace MarkYourDay.Droid
{
    [Activity(Label = "MarkYourDay", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher =false, ScreenOrientation =ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            

            ButtonCircleRenderer.Init();
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

