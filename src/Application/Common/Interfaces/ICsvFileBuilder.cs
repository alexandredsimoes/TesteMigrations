using TesteMigrations.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace TesteMigrations.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
