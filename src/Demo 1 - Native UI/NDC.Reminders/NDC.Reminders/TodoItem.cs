using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace NDC.Reminders
{
    public class TodoItem
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public DateTime AddedAt { get; set; }
        public string Text { get; set; }
        public string Username { get; set; }
        public bool Done { get; set; }

        public TodoItem()
        {
            Id = Guid.NewGuid();
            AddedAt = DateTime.Now;
        }

        public override string ToString() => string.Format($"{Text} ({Username})");
    }
}
