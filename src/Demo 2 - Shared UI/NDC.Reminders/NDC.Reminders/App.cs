using Xamarin.Forms;

namespace NDC.Reminders
{
    public class App : Application
    {
        public App()
        {
            var navigationPage = new NavigationPage(new LoginPage());

            MainPage = navigationPage;
        }
    }
}
