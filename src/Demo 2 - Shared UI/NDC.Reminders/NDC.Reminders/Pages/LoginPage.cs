using Xamarin.Forms;

namespace NDC.Reminders
{
    public class LoginPage : ContentPage
    {
        public static string CurrentUsername { get; set; }

        public LoginPage()  
        {
            Title = "LOGIN";
            
            var usernameEntry = new Entry {Text = "Username"};
            var passwordEntry = new Entry { Text = "Password", IsPassword = true };

            var loginButton = new Button
            {
                Text = "LOGIN"
            };

            loginButton.Clicked += async (sender, args) =>
            {
#if __ANDROID__
                new AccountManager().Save(Forms.Context, usernameEntry.Text, passwordEntry.Text);
#endif
#if __IOS__
                new AccountManager().Save(usernameEntry.Text, passwordEntry.Text);
#endif

                CurrentUsername = usernameEntry.Text;

                await Navigation.PushAsync(new TodoPage());
            };

            Content = new StackLayout
            {
                Padding = 24,
                Children = {
                    new Label { Text = "Username" },
                    usernameEntry,
                    new Label { Text = "Password" },
                    passwordEntry,
                    loginButton
                }
            };
        }
    }
}