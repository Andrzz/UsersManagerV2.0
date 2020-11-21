namespace OperationsService.Operations
{
    using System;
    using System.Data;

    public interface IOperations
    {
        void AddOrEdit(string connectionString, string userName, DateTime bithDate, char gender, int userId);
        void Delete(string connectionString, int id);
        DataTable GetAllUsersRegistered(string connectionStrings);
        DataTable UpdateUser(string connectionString, int id);
    }
}
