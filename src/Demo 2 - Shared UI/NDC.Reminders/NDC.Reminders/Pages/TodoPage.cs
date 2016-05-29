using System.Threading.Tasks;
using Xamarin.Forms;

namespace NDC.Reminders
{
    public class TodoPage : ContentPage
    {
        private ListView todoListView = new ListView();
        private TodoRepository repository;
        public TodoPage()
        {
            Title = "Reminders";

            ToolbarItems.Add(new ToolbarItem("Add Reminder", "add.png", async () =>
            {
                await Navigation.PushModalAsync(new AddTodoPage(), true);
            }));

            Content = new StackLayout
            {
                Padding = 24,
                Children = {
                    todoListView
                }
            };
            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await LoadAsync();

            todoListView.ItemSelected += async (sender, args) =>
            {
                var item = (TodoItem)args.SelectedItem;
                item.Done = true;

                await repository.UpdateAsync(item);

                await LoadAsync();
            };
        }

        public async Task LoadAsync()
        {
            repository = new TodoRepository();

            await repository.InitializeAsync();

            todoListView.ItemsSource = await repository.AllAsync(LoginPage.CurrentUsername);
        }
    }
}