using System;
using System.Collections.Generic;
using productservice.Models;
using Microsoft.Extensions.OptionsModel;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Data;

namespace productservice.Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        private readonly Settings settings;

        public ProductRepository(IOptions<Settings> settings)
        {
            this.settings = settings.Value;
        }

        public IEnumerable<Product> AllProducts()
        {
            using (var connection = this.Connect())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT id, productCode, productName, buyPrice FROM PRODUCT";

                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        var result = new List<Product>();

                        while (reader.Read())
                        {
                            result.Add(new Product
                            {
                                Id = reader.GetInt64(0),
                                Code = reader.GetString(1),
                                Name = reader.GetString(2),
                                Price = reader.GetDecimal(3)
                            });
                        }

                        return result;
                    }
                }
            }
        }

        public Product GetById(long id)
        {
            using (var connection = this.Connect())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = string.Format("SELECT id, productCode, productName, buyPrice FROM PRODUCT WHERE id = {0}", id );

                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        var result = new List<Product>();

                        while (reader.Read())
                        {
                            result.Add(new Product
                            {
                                Id = reader.GetInt64(0),
                                Code = reader.GetString(1),
                                Name = reader.GetString(2),
                                Price = reader.GetDecimal(3)
                            });
                        }

                        return result.FirstOrDefault();
                    }
                }
            }
        }

        private MySqlConnection Connect()
        {
            var connection = new MySqlConnection(this.settings.DatabaseConnection);

            connection.Open();

            return connection;
        }

    }
}
