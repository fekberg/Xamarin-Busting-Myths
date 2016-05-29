using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace NDC.Reminders.Droid.Activities
{
    [Activity(Theme = "@style/NdcOslo")]
    public class AddTodoActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.AddTodo);

            var button = FindViewById<Button>(Resource.Id.createTodo);

            button.Click += async (sender, args) =>
            {
                var text = FindViewById<EditText>(Resource.Id.text);

                var repository = new TodoRepository();
                await repository.InitializeAsync();
                await repository.InsertAsync(new TodoItem { Text = text.Text, Username = LoginActivity.CurrentUsername });

                SetResult(Result.Ok);

                Finish();
            };
        }
    }
}