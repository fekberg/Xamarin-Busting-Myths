using Android.App;
using Android.Content.PM;
using Android.OS;

namespace NDC.Reminders.Droid.Activities
{
    [Activity(Label = "Reminders", Icon = "@drawable/ic_fekberg_ab_192", Theme = "@style/NdcOsloSplash", NoHistory = true,
        MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            StartActivity(typeof(MainActivity));
        }
    }
}