using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SQLite;

namespace NDC.Reminders
{
    public class TodoRepository
    {
        private SQLiteAsyncConnection connection;

        public Task InitializeAsync()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "todo-db.db3");

            connection = new SQLiteAsyncConnection(path);

            return connection.CreateTableAsync<TodoItem>();
        }

        public Task InsertAsync(TodoItem item)
        {
            return connection.InsertAsync(item);
        }

        public Task UpdateAsync(TodoItem item)
        {
            return connection.UpdateAsync(item);
        }   

        public async Task<IEnumerable<TodoItem>> AllAsync(string username)
        {
            try
            {
                return await connection.QueryAsync<TodoItem>("select * from TodoItem where Username=? and Done=0 order by AddedAt desc", username);
            }
            catch (Exception ex)
            {
                // Report error to Xamarin.Insights
            }

            return Enumerable.Empty<TodoItem>();
        }
    }
}
