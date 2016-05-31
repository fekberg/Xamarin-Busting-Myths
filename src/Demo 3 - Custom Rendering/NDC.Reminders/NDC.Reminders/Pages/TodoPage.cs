using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NDC.Reminders
{
    public class TodoListView : ListView
    {
        private Func<Task> onReloadCallbackAsync;
        public void OnReload(Func<Task> asyncCallback)
        {
            onReloadCallbackAsync = asyncCallback;
        }
        public async Task ReloadAsync()
        {
            await onReloadCallbackAsync();
        }
    }

    public class TodoPage : ContentPage
    {
        private TodoListView todoListView = new TodoListView();
        private TodoRepository repository;
        public TodoPage()
        {
            Title = "Reminders";

            todoListView.OnReload(LoadAsync);

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

                await todoListView.ReloadAsync();
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