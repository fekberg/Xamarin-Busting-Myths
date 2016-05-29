#if !WINDOWS_UWP
using Xamarin.Auth;
#endif

namespace NDC.Reminders
{
    public class AccountManager
    {
#if __ANDROID__
        public void Save(Android.Content.Context context, string username, string password)
#else
        public void Save(string username, string password)
#endif
        {
#if WINDOWS_UWP
            return;
#else
            var account = new Account(username);
            account.Properties.Add("Password", password);
#endif
#if __ANDROID__
            AccountStore.Create(context).Save(account, "ndc_oslo_forms");
#endif
#if __IOS__
            AccountStore.Create().Save(account, "ndc_oslo_forms");
#endif
        }
    }
}
