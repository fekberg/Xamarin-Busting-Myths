using Xamarin.Auth;

namespace NDC.Reminders
{
    public class AccountManager
    {
#if __ANDROID__
        public void Save(Android.Content.Context context, string username, string password)
#endif
#if __IOS__
        public void Save(string username, string password)
#endif
        {
            var account = new Account(username);
            account.Properties.Add("Password", password);

#if __ANDROID__
            AccountStore.Create(context).Save(account, "ndc_oslo");
#endif
#if __IOS__
            AccountStore.Create().Save(account, "ndc_oslo");
#endif
        }
    }
}
