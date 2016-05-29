using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Widget;

namespace NDC.Reminders.Droid.Activities
{
    [Activity(Theme = "@style/NdcOslo")]
    public class MainActivity : AppCompatActivity
    {
        private TodoRepository repository;
		protected override async void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            repository = new TodoRepository();
            await repository.InitializeAsync();

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

		    var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);

		    fab.Click += (sender, args) =>
		    {
                StartActivityForResult(typeof(AddTodoActivity), 0);
		    };

            await LoadAsync();

            var listView = FindViewById<ListView>(Resource.Id.items);

		    listView.ItemClick += async (sender, args) =>
		    {
		        var item = listView.GetItemAtPosition(args.Position);
                var todoItem = (TodoItem) item.GetType().GetProperty("Instance").GetValue(item);

		        todoItem.Done = true;

                await repository.UpdateAsync(todoItem);

                Snackbar
                    .Make(fab, $"{todoItem.Text} marked as done!", Snackbar.LengthLong)
                    .SetAction("Undo", async (view) =>
                    {
                        todoItem.Done = false;

                        await repository.UpdateAsync(todoItem);

                        await LoadAsync();
                    })
                    .Show();



                await LoadAsync();
            };
		}

	    protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
	    {
	        base.OnActivityResult(requestCode, resultCode, data);

	        await LoadAsync();
	    }

	    public async Task LoadAsync()
        {
            var adapter = new ArrayAdapter<TodoItem>(this, Android.Resource.Layout.SimpleListItem1, (await repository.AllAsync(LoginActivity.CurrentUsername)).ToList());

            var listView = FindViewById<ListView>(Resource.Id.items);

	        listView.Adapter = adapter;
        }
    }
}