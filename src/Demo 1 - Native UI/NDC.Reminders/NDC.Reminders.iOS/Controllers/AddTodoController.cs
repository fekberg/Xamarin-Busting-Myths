using CoreGraphics;
using UIKit;

namespace NDC.Reminders.iOS.Controllers
{
    public class AddTodoController : UIViewController
    {
        public static string CurrentUsername { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;
            Title = "Create Reminder";

            CreateControls();
        }

        private void CreateControls()
        {
            var width = UIScreen.MainScreen.Bounds.Width - 2 * Constants.DefaultMargin;

            var reminderDescriptionLabel = new UILabel(new CGRect(Constants.DefaultMargin, 92, width, 32)) { Text = "Description" };
            var descriptionInput = new UITextField(new CGRect(Constants.DefaultMargin, reminderDescriptionLabel.Frame.Bottom + Constants.DefaultMargin, width, 32)) { BorderStyle = UITextBorderStyle.RoundedRect };

            var addReminderButton = UIButton.FromType(UIButtonType.System);
            addReminderButton.Frame = new CGRect(Constants.DefaultMargin, descriptionInput.Frame.Bottom + Constants.DefaultMargin, width, 32);
            addReminderButton.SetTitle("CREATE", UIControlState.Normal);
            addReminderButton.TouchUpInside += async (sender, args) =>
            {
                var repository = new TodoRepository();
                await repository.InitializeAsync();
                await repository.InsertAsync(new TodoItem { Text = descriptionInput.Text, Username = MainViewController.CurrentUsername });

                DismissModalViewController(true);
            };

            View.AddSubview(reminderDescriptionLabel);
            View.AddSubview(descriptionInput);
            View.AddSubview(addReminderButton);
        }
    }
}