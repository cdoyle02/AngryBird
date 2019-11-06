using System;
using System.Collections.Generic;
using AngryBird.Models;
using MySql.Data.MySqlClient;

namespace AngryBird
{
    public class CategoryRepository
    {
        private static string connectionString = System.IO.File.ReadAllText("ConnectionString.txt");

        public List<Category> GetCategories()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM category;";

            using (conn)
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                var allCategories = new List<Category>();

                while (reader.Read() == true)
                {
                    var currentCategory = new Category();
                    currentCategory.CategoryID = reader.GetInt32("categoryID");
                    currentCategory.Name = reader.GetString("category");
                    allCategories.Add(currentCategory);
                }

                return allCategories;
            }
        }

        public void GetCategoryName(AbQuestion question)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT category FROM category WHERE categoryid = @id;";
            cmd.Parameters.AddWithValue("id", question.CategoryID );

            using (conn)
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                

                while (reader.Read() == true)
                {
                    
                    question.Category = reader.GetString("category");
                    
                }
               
            }
        }
    }
}
