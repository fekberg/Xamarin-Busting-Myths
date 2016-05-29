using System;
using System.Linq;
using Foundation;
using NDC.Reminders.iOS.Controllers;
using UIKit;

namespace NDC.Reminders.iOS.Sources
{
    public class TodoItemsSource : UITableViewSource
    {
        private TodoItem[] items;
        private readonly TodoRepository repository;
        private readonly Action postDeleteAction;

        private string CellIdentifier = "TableCell";

        public TodoItemsSource(TodoItem[] items, TodoRepository repository, Action postDeleteAction)
        {
            this.items = items;
            this.repository = repository;
            this.postDeleteAction = postDeleteAction;
        }

        public override async void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var selectedItem = items[indexPath.Row];

            selectedItem.Done = true;

            await repository.UpdateAsync(selectedItem);

            items = (await repository.AllAsync(MainViewController.CurrentUsername)).ToArray();

            postDeleteAction();
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return items.Length;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CellIdentifier);
            var item = items[indexPath.Row];

            if (cell == null) { cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier); }

            cell.TextLabel.Text = item.ToString();

            return cell;
        }
    }
}