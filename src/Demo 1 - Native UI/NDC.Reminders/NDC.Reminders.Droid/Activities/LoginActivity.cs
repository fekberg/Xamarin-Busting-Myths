using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace NDC.Reminders.Droid.Activities
{
    [Activity(Theme = "@style/NdcOslo")]
    public class LoginActivity : AppCompatActivity
    {
        public static string CurrentUsername { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            SetContentView(Resource.Layout.Login);
            
            var login = FindViewById<Button>(Resource.Id.login);

            login.Click += (sender, args) =>
            {
                var username = FindViewById<EditText>(Resource.Id.username);
                var password = FindViewById<EditText>(Resource.Id.password);
                new AccountManager().Save(this, username.Text, password.Text);

                CurrentUsername = username.Text;

                StartActivity(typeof(MainActivity));
            };
        }
    }
}