using Xamarin.Forms;

namespace NDC.Reminders
{
    public class AddTodoPage : ContentPage
    {
        public AddTodoPage()
        {
            Title = "ADD REMINDER";

            var descriptionEntry = new Entry { Text = "Description" };

            var createButton = new Button
            {
                Text = "CREATE"
            };

            createButton.Clicked += async (sender, args) =>
            {
                var repository = new TodoRepository();
                await repository.InitializeAsync();
                await repository.InsertAsync(new TodoItem { Text = descriptionEntry.Text, Username = LoginPage.CurrentUsername });

                await Navigation.PopModalAsync(true);
            };
            
            Content = new StackLayout
            {
                Padding = 24,
                Children = {
                    new Label { Text = "Description" },
                    descriptionEntry,
                    createButton
                }
            };
        }
    }
}