using Android.Support.Design.Widget;
using Android.Widget;
using NDC.Reminders;
using NDC.Reminders.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ListView = Android.Widget.ListView;

[assembly: ExportRenderer(typeof(TodoListView), typeof(TodoListViewRenderer))]
namespace NDC.Reminders.Droid.Renderers
{
    public class TodoListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                Control.ItemClick -= OnItemClick;
            }

            if (e.NewElement != null)
            {
                Control.ItemClick += OnItemClick;
            }
        }

        private async void OnItemClick(object sender, AdapterView.ItemClickEventArgs args)
        {
            var todoListView = Element as TodoListView;

            if (todoListView == null) return;

            var listView = sender as ListView;

            var item = listView?.GetItemAtPosition(args.Position);

            var todoItem = (TodoItem)item?.GetType().GetProperty("Instance").GetValue(item);

            if (item == null) return;

            todoItem.Done = true;

            var repository = new TodoRepository();

            await repository.InitializeAsync();

            await repository.UpdateAsync(todoItem);

            Snackbar
                .Make(listView, $"{todoItem.Text} marked as done!", 
                Snackbar.LengthLong)
                .SetAction("Undo", async (view) =>
                {
                    todoItem.Done = false;

                    await repository.UpdateAsync(todoItem);

                    await todoListView.ReloadAsync();
                })
                .Show();

            await todoListView.ReloadAsync();
        }
    }
}
