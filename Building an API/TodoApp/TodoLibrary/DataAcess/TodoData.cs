using TodoLibrary.Models;

namespace TodoLibrary.DataAcess
{
    public class TodoData
    {
        private readonly ISqlDataAccess _sql;
        public TodoData(ISqlDataAccess sql)
        {
            _sql= sql;
        }

        public Task<List<TodoModel>> GetAllAssigned(int assignedTo)
        {
           return _sql.LoadData<TodoModel, dynamic>(
                "dbo.spTodos_GetAllAssigned",
                new { AssignedTo = assignedTo },
                "Default");
        }

        public async Task<TodoModel?>GetOneAssigned(int assignedTo, int todoId)
        {
            var result = await _sql.LoadData<TodoModel, dynamic>(
                 "dbo.spTodos_GetAllAssigned",
                 new { AssignedTo = assignedTo, TodoId = todoId },
                 "Default");

            return result.FirstOrDefault();
        }

    }
}
