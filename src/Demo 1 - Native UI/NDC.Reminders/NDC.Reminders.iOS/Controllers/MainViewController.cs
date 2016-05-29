using CoreGraphics;
using UIKit;

namespace NDC.Reminders.iOS.Controllers
{
    public class MainViewController : UIViewController
    {
        public static string CurrentUsername { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;
            Title = "Login";

            CreateControls();
        }

        private void CreateControls()
        {
            var width = UIScreen.MainScreen.Bounds.Width - 2 * Constants.DefaultMargin;

            var usernameLabel = new UILabel(new CGRect(Constants.DefaultMargin, 92, width, 32)) { Text = "Username" };
            var usernameInput = new UITextField(new CGRect(Constants.DefaultMargin, usernameLabel.Frame.Bottom + Constants.DefaultMargin, width, 32)) { BorderStyle = UITextBorderStyle.RoundedRect };

            var passwordLabel = new UILabel(new CGRect(Constants.DefaultMargin, usernameInput.Frame.Bottom + Constants.DefaultMargin, width, 32)) { Text = "Password" };
            var passwordInput = new UITextField(new CGRect(Constants.DefaultMargin, passwordLabel.Frame.Bottom + Constants.DefaultMargin, width, 32)) { SecureTextEntry = true, BorderStyle = UITextBorderStyle.RoundedRect };

            var loginButton = UIButton.FromType(UIButtonType.System);
            loginButton.Frame = new CGRect(Constants.DefaultMargin, passwordInput.Frame.Bottom + Constants.DefaultMargin, width, 32);
            loginButton.SetTitle("LOGIN", UIControlState.Normal);
            loginButton.TouchUpInside += (sender, args) =>
            {
                new AccountManager().Save(usernameInput.Text, passwordInput.Text);

                CurrentUsername = usernameInput.Text;

                NavigationController.PushViewController(new TodoViewController(), true);
            };

            View.AddSubview(usernameLabel);
            View.AddSubview(usernameInput);
            View.AddSubview(passwordLabel);
            View.AddSubview(passwordInput);
            View.AddSubview(loginButton);
        }
    }
}