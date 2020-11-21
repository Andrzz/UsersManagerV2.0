namespace OperationsService.Operations.Impl
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    public class Operations : IOperations
    {
        public void AddOrEdit(string connectionString, string userName, DateTime bithDate, char gender, int userId)
        {
            using(SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("UserAddOrEdit", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("USERID", userId);
                cmd.Parameters.AddWithValue("USERNAME", userName);
                cmd.Parameters.AddWithValue("BIRTHDATE", bithDate);
                cmd.Parameters.AddWithValue("GENDER", gender);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(string connectionString, int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("UserDeleteByID", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("USERID", id);
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetAllUsersRegistered(string connectionString)
        {
            DataTable dtt = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("UsersViewAll", sqlConnection);
                adapter.Fill(dtt);
            }
            return dtt;
        }

        public DataTable UpdateUser(string connectionString, int id)
        {
            DataTable dtt = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("UserViewByID", sqlConnection);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("USERID", id);
                adapter.Fill(dtt);
            }
            return dtt;
        }
    }
}
