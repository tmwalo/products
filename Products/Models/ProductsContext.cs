using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Products.Models
{
    public class ProductsContext
    {

        public string ConnectionString { get; set; }

        public ProductsContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return (new MySqlConnection(ConnectionString));
        }

        public bool WasRowAffected(int rowsAffected)
        {
            if (rowsAffected == 1)
                return (true);
            else
                return (false);
        }

        public List<Product> ReadAllProducts()
        {

            List<Product> products;

            products = new List<Product>();
            using (MySqlConnection conn = GetConnection())
            {

                conn.Open();
                string sql = "SELECT * FROM products";
                var cmd = new MySqlCommand(sql, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product()
                            {
                                Id = reader.GetInt32("id"),
                                Type = reader.GetString("type"),
                                Cost = reader.GetDouble("cost"),
                                Price = reader.GetDouble("price"),
                                Quantity = reader.GetUInt32("quantity")
                            }
                        );
                    }
                }
                return (products);

            }

        }

        public Product ReadProduct(int id)
        {
            Product product;

            product = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM products WHERE id = @Id";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        product = new Product()
                        {
                            Id = reader.GetInt32("id"),
                            Type = reader.GetString("type"),
                            Cost = reader.GetDouble("cost"),
                            Price = reader.GetDouble("price"),
                            Quantity = reader.GetUInt32("quantity")
                        };
                    }
                }
            }
            return (product);
        }

        public bool Create(Product product)
        {
            if (product == null)
                return (false);
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string sql = "INSERT INTO products (type, cost, price, quantity) VALUES (@type, @cost, @price, @quantity)";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@type", product.Type);
                cmd.Parameters.AddWithValue("@cost", product.Cost);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@quantity", product.Quantity);
                int rowsAffected = cmd.ExecuteNonQuery();
                return (WasRowAffected(rowsAffected));
            }
        }

        public bool Update(Product product)
        {
            if (product == null)
                return (false);
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string sql =    "UPDATE products " +
                                "SET " +
                                "type = @type, " +
                                "cost = @cost, " +
                                "price = @price, " +
                                "quantity = @quantity " +
                                "WHERE id = @id";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", product.Id);
                cmd.Parameters.AddWithValue("@type", product.Type);
                cmd.Parameters.AddWithValue("@cost", product.Cost);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@quantity", product.Quantity);
                int rowsAffected = cmd.ExecuteNonQuery();
                return (WasRowAffected(rowsAffected));
            }
        }

        public bool Delete(int id)
        {
            if (id < 0)
                return (false);
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM products WHERE id = @id";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                int affectedRows = cmd.ExecuteNonQuery();
                return (WasRowAffected(affectedRows));
            }
        }

    }
}
