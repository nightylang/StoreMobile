using Microsoft.Data.SqlClient;
using StoreMobileV2.Models;

namespace StoreMobileV2.Data
{
    public class TableRepo
    {
        private readonly string _conString;
        public TableRepo(IConfiguration configuration)
        {
            _conString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("Connection string not found.");
        }

        public string sqlAllCategory = "";
        public string sqlAllModel = "";
        public string sqlAllRole = "";
        public string sqlAllStatus = "";

        public List<AllModel.Category> GetAllCate()
        {
            List<AllModel.Category> cates = new List<AllModel.Category>();
            using SqlConnection conn = new SqlConnection(_conString);
            string sql = "SELECT id_category, category FROM Categories";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                AllModel.Category cate = new AllModel.Category();
                cate.Id = Convert.ToInt32(reader["id_category"]);
                cate.Name = reader["category"].ToString() ?? "";
                cates.Add(cate);
            }
            return cates;
        }

        public AllModel.Category? GetById(int id)
        {
            AllModel.Category? cate = null;
            using SqlConnection connection = new SqlConnection(_conString);
            string sql = @"
                SELECT * FROM Categories 
                WHERE id_category = @id_category";
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ID_product", id);
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                cate = new ProductModel();
                cate.Id = Convert.ToInt32(reader["id_category"]);
                cate.Name = reader["category"].ToString() ?? "";
            }
            return cate;
        }

        public void CreateCategory(AllModel.Category cate)
        {
            using SqlConnection connection = new SqlConnection(_conString);
            string sql = @"
                INSERT INTO Categories (category)
                VALUES (@category)";
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@category", cate.Name ?? "");
            
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void UpdateCategory(AllModel.Category cate)
        {
            using SqlConnection connection = new SqlConnection(_conString);
            string sql = @" UPDATE Categories SET category = @category WHERE id_category = @id_category;";
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id_category", cate.Id);
            command.Parameters.AddWithValue("@category", cate.Name ?? "");
            
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void DeleteCategory(int id)
        {
            using SqlConnection connection = new SqlConnection(_conString);
            string sql = "DELETE FROM Categories WHERE id_category = @id_category";
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id_category", id);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public List<AllModel.Model> GetAllModel()
        {
            List<AllModel.Model> models = new List<AllModel.Model>();
            using SqlConnection conn = new SqlConnection(_conString);
            string sql = "SELECT id_model, models FROM Models";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                AllModel.Model mods = new AllModel.Model();
                mods.Id = Convert.ToInt32(reader["id_model"]);
                mods.Name = reader["models"].ToString() ?? "";
                models.Add(mods);
            }
            return models;
        }
        public List<AllModel.Role> GetAllRole()
        {
            List<AllModel.Role> roles = new List<AllModel.Role>();
            using SqlConnection conn = new SqlConnection(_conString);
            return roles;
        }
        public List<AllModel.Status> GetAllStatus()
        {
            List<AllModel.Status> statuses = new List<AllModel.Status>();
            using SqlConnection conn = new SqlConnection(_conString);
            return statuses;
        }

        public void Order(OrderModel order)
        {
            using SqlConnection conn = new SqlConnection(_conString);
            string sql = @"INSERT INTO Orders (ID_user, ID_product) 
                        VALUES (@ID_user, @ID_product)";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ID_user", order.ID_user);
            cmd.Parameters.AddWithValue("@ID_product", order.ID_product);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
