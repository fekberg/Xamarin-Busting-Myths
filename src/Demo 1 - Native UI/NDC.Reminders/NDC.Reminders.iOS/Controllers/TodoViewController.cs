using System.Linq;
using System.Threading.Tasks;
using NDC.Reminders.iOS.Sources;
using UIKit;

namespace NDC.Reminders.iOS.Controllers
{
    public class TodoViewController : UITableViewController
    {
        private TodoRepository repository;
        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;
            Title = "Reminders";

            repository = new TodoRepository();

            await repository.InitializeAsync();

            NavigationItem.SetRightBarButtonItem(
                new UIBarButtonItem(UIBarButtonSystemItem.Add, (sender, args) => {
                    NavigationController.PresentModalViewController(new AddTodoController(), true);
                })
            , true);
        }

        public override async void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            await ReloadDataAsync();
        }

        private async Task ReloadDataAsync()
        {
            var items = await repository.AllAsync(MainViewController.CurrentUsername);

            TableView.Source = new TodoItemsSource(items.ToArray(), repository, () => TableView.ReloadData());

            TableView.ReloadData();
        }
    }
}

