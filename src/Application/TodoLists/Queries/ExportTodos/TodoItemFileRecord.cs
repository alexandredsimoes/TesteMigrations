using TesteMigrations.Application.Common.Mappings;
using TesteMigrations.Domain.Entities;

namespace TesteMigrations.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
