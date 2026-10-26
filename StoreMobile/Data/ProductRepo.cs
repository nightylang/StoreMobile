using Microsoft.Data.SqlClient;
using StoreMobileV2.Models;

namespace StoreMobileV2.Data
{
    public class ProductRepo
    {
        private readonly string _conString;
        public string sqlAll = @"SELECT p.ID_product, p.id_name, p.product_name, p.product_price, p.product_description, p.qty, c.category, m.models, i.image 
                                FROM Products p
                                INNER JOIN Models m ON p.id_model = m.id_model
                                INNER JOIN Images_product i ON p.id_image = i.id_image
                                INNER JOIN Categories c ON p.id_category = c.id_category
                                ORDER BY p.ID_product DESC
                                ";
        public ProductRepo(IConfiguration configuration)
        {
            _conString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("Connection string not found.");
        }

        public List<ProductModel> GetAll()
        {
            List<ProductModel> prods = new List<ProductModel>();
            using SqlConnection connection = new SqlConnection(_conString);
            string sql = @"
                SELECT ID_product, id_name, product_name, product_price, product_description, qty, id_category, id_model, id_image 
                FROM Products 
                ORDER BY ID_product DESC";
            using SqlCommand command = new SqlCommand(sqlAll, connection);
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ProductModel prod = new ProductModel();
                prod.ID_product = Convert.ToInt32(reader["ID_product"]);
                prod.id_name = reader["id_name"].ToString() ?? "";
                prod.product_name = reader["product_name"].ToString() ?? "";
                prod.product_description = reader["product_description"].ToString() ?? "";
                prod.product_price = Convert.ToInt32(reader["product_price"]);
                prod.qty = Convert.ToInt32(reader["qty"]);
                prod.id_category = reader["category"].ToString() ?? "";
                prod.id_model = reader["models"].ToString() ?? "";
                prod.id_image = reader["image"].ToString() ?? "";
                prods.Add(prod);
            }
            return prods;
        }

        public ProductModel? GetById(int id)
        {
            ProductModel? prod = null;
            using SqlConnection connection = new SqlConnection(_conString);
            string sql = @"
                SELECT ID_product, id_name, product_name, product_price, product_description, qty, id_category, id_model, id_image  
                FROM Products 
                WHERE ID_product = @ID_product";
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ID_product", id);
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                prod = new ProductModel();
                prod.ID_product = Convert.ToInt32(reader["ID_product"]);
                prod.id_name = reader["id_name"].ToString() ?? "";
                prod.product_name = reader["product_name"].ToString() ?? "";
                prod.product_description = reader["product_description"].ToString() ?? "";
                prod.product_price = Convert.ToInt32(reader["product_price"]);
                prod.qty = Convert.ToInt32(reader["qty"]);
                prod.id_category = reader["id_category"].ToString() ?? "";
                prod.id_model = reader["id_model"].ToString() ?? "";
                prod.id_image = reader["id_image"].ToString() ?? "";
            }
            return prod;
        }

        public void Create(ProductModel prod)
        {
            using SqlConnection connection = new SqlConnection(_conString);
            string sql = @"
                INSERT INTO Products (id_name, product_name, product_price, product_description, qty, id_category, id_model, id_image)
                VALUES (@id_name, @product_name, @product_price, @product_description, @qty, @id_category, @id_model, @id_image)";
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id_name", prod.id_name ?? "");
            command.Parameters.AddWithValue("@product_name", prod.product_name ?? "");
            command.Parameters.AddWithValue("@product_price", prod.product_price);
            command.Parameters.AddWithValue("@product_description", prod.product_description ?? "");
            command.Parameters.AddWithValue("@qty", prod.qty);
            command.Parameters.AddWithValue("@id_category", prod.id_category);
            command.Parameters.AddWithValue("@id_model", prod.id_model);
            command.Parameters.AddWithValue("@id_image", prod.id_image);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Update(ProductModel? prod) 
        {
            using SqlConnection connection = new SqlConnection(_conString);
            string sql = @"
                UPDATE Products
                SET id_name = @id_name,
                    product_name = @product_name,
                    product_price = @product_price,
                    product_description = @product_description,
                    qty = @qty, 
                    id_category = @id_category, 
                    id_model = @id_model 
                WHERE ID_product = @ID_product;
                UPDATE Images_product SET image = @id_image WHERE ID_product = @ID_product";
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ID_product", prod.ID_product);
            command.Parameters.AddWithValue("@id_name", prod.id_name ?? "");
            command.Parameters.AddWithValue("@product_name", prod.product_name ?? "");
            command.Parameters.AddWithValue("@product_price", prod.product_price);
            command.Parameters.AddWithValue("@product_description", prod.product_description ?? "");
            command.Parameters.AddWithValue("@qty", prod.qty);
            command.Parameters.AddWithValue("@id_category", prod.id_category);
            command.Parameters.AddWithValue("@id_model", prod.id_model);
            command.Parameters.AddWithValue("@id_image", prod.id_image ?? "");

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Delete(int id) 
        {
            using SqlConnection connection = new SqlConnection(_conString);
            string sql = "DELETE FROM Products WHERE ID_product = @ID_product";
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ID_product", id);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public int GetProductCount()
        {
            int count = 0;
            using SqlConnection connection = new SqlConnection(_conString);
            string sql = "SELECT COUNT(qty) FROM Products";

            using SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();

            count = (int)command.ExecuteScalar();

            return count;
        }
    }
}
