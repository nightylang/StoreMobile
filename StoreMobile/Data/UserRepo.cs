using Microsoft.Data.SqlClient;
using StoreMobileV2.Models;

namespace StoreMobileV2.Data
{
    public class UserRepo
    {
        private readonly string _conString;

        public UserRepo(IConfiguration configuration)
        {
            _conString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("Connection string not found.");
        }

        // GET ALL STUDENTS
        public List<UserModel> GetAll()
        {
            List<UserModel> users = new List<UserModel>();
            using SqlConnection connection = new SqlConnection(_conString);
            string sql = @"
                SELECT u.ID_user, u.username, u.password, u.Image, u.email, s.ID_role 
                FROM Users u 
                INNER JOIN Roles s ON u.ID_role = s.ID
                ORDER BY u.ID_user DESC";

            using SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                UserModel user = new UserModel();
                user.ID_user = Convert.ToInt32(reader["ID_user"]);
                user.username = reader["username"].ToString() ?? "";
                user.password = reader["password"].ToString() ?? "";
                user.Image = reader["Image"].ToString() ?? "";
                user.email = reader["email"].ToString() ?? "";
                user.ID_role = reader["ID_role"].ToString() ?? "";
                users.Add(user);
            }
            return users;
        }

        // GET STUDENT BY ID
        public UserModel? GetById(int id)
        {
            UserModel? user = null;
            using SqlConnection connection = new SqlConnection(_conString);
            string sql = @"
                SELECT ID_user, username, password, Image, email
                FROM Users 
                WHERE ID_user = @ID_user";

            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ID_user", id);
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                user = new UserModel();
                user.ID_user = Convert.ToInt32(reader["ID_user"]);
                user.username = reader["username"].ToString() ?? "";
                user.password = reader["password"].ToString() ?? "";
                user.email = reader["email"].ToString() ?? "";
            }
            return user;
        }

        // INSERT
        public void Create(UserModel user)
        {
            using SqlConnection connection = new SqlConnection(_conString);
            string sql = @"
                INSERT INTO Users (username, password, Image, email)
                VALUES (@username, @password, @Image, @email)";

            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@username", user.username);
            command.Parameters.AddWithValue("@password", user.password ?? "");
            command.Parameters.AddWithValue("@Image", user.Image ?? "");
            command.Parameters.AddWithValue("@email", user.email ?? "");

            connection.Open();
            command.ExecuteNonQuery();
        }

        // UPDATE
        public void Update(UserModel user)
        {
            using SqlConnection connection = new SqlConnection(_conString);
            string sql = @"
                UPDATE Users
                SET username = @username,
                    password = @password,
                    Image = @Image,
                    email = @email,
                    ID_role = @ID_role 
                WHERE ID_user = @ID_user";

            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ID_user", user.ID_user);
            command.Parameters.AddWithValue("@username", user.username);
            command.Parameters.AddWithValue("@password", user.password ?? "");
            command.Parameters.AddWithValue("@Image", user.Image ?? "");
            command.Parameters.AddWithValue("@email", user.email ?? "");
            command.Parameters.AddWithValue("@ID_role", user.ID_role ?? "");

            connection.Open();
            command.ExecuteNonQuery();
        }

        // DELETE
        public void Delete(int id)
        {
            using SqlConnection connection = new SqlConnection(_conString);
            string sql = "DELETE FROM Users WHERE ID_user = @ID_user";

            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ID_user", id);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public int GetUserCount()
        {
            int count = 0;
            using SqlConnection connection = new SqlConnection(_conString);
            string sql = "SELECT COUNT(*) FROM Users";

            using SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();

            count = (int)command.ExecuteScalar();

            return count;
        }
    }
}
