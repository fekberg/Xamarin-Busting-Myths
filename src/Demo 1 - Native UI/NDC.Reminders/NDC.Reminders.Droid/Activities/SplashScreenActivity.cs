using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Support.V7.App;

namespace NDC.Reminders.Droid.Activities
{
    [Activity(Label = "Reminders", MainLauncher = true, Icon = "@drawable/ic_fekberg_ab_192", Theme = "@style/NdcOsloSplash", NoHistory = true)]
    public class SplashScreenActivity : AppCompatActivity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            await Task.Delay(2000);

            StartActivity(typeof(LoginActivity));
        }
    }
}