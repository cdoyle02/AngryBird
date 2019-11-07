using System;
using System.Collections.Generic;
using AngryBird.Models;
using MySql.Data.MySqlClient;

namespace AngryBird
{
    public class QuestionRepository
    {
        private static string connectionString = System.IO.File.ReadAllText("ConnectionString.txt");

        public List<AbQuestion> GetAllQuestions()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM question;";

            using (conn)
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                List<AbQuestion> allQuestions = new List<AbQuestion>();

                var catRepo = new CategoryRepository();

                while(reader.Read() == true)
                {
                    var currentQuestion = new AbQuestion();
                    currentQuestion.QuestionID = reader.GetInt32("questionid");
                    currentQuestion.AngryBirdQuestion = reader.GetString("angrybirdquestion");

                    if (reader.IsDBNull(reader.GetOrdinal("categoryid")))
                    {
                        currentQuestion.CategoryID = null;
                    }
                    else
                    {
                        currentQuestion.CategoryID = reader.GetInt32("categoryid");
                    }

                    catRepo.GetCategoryName(currentQuestion);

                    allQuestions.Add(currentQuestion);
                }

                return allQuestions;
            }
        }

        public AbQuestion GetQuestion(int id)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM question WHERE questionid = @id;";
            cmd.Parameters.AddWithValue("id", id);

            using (conn)
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                var question = new AbQuestion();
                var catRepo = new CategoryRepository();

                while (reader.Read() == true)
                {
                    question.QuestionID = reader.GetInt32("questionid");
                    question.AngryBirdQuestion = reader.GetString("angrybirdquestion");
                    question.CategoryID = reader.GetInt32("categoryid");
                    catRepo.GetCategoryName(question);
                    //question.QuestionRating = reader.GetInt32();
                }

                return question;

            }

        }
        public void UpdateQuestion(AbQuestion questionToUpdate)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE question SET AngryBirdQuestion = @angrybirdquestion, CategoryID = @categoryid WHERE QuestionID = @id;";
            
            cmd.Parameters.AddWithValue("angrybirdquestion", questionToUpdate.AngryBirdQuestion);
            cmd.Parameters.AddWithValue("categoryid", questionToUpdate.CategoryID);
            cmd.Parameters.AddWithValue("id", questionToUpdate.QuestionID);


            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }

        }

        public void InsertQuestion(AbQuestion questionToInsert)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO question (angrybirdquestion, categoryid, ratingid) VALUES (@angrybirdquestion, @categoryid, @ratingid);";

            cmd.Parameters.AddWithValue("angrybirdquestion", questionToInsert.AngryBirdQuestion);
            cmd.Parameters.AddWithValue("categoryid", questionToInsert.CategoryID);
            cmd.Parameters.AddWithValue("ratingid", questionToInsert.RatingID);

            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }

        } 

        public AbQuestion AssignCategories()
        {
            var catRepo = new CategoryRepository();

            var catList = catRepo.GetCategories();

            AbQuestion question = new AbQuestion();

            question.Categories = catList;

            return question;
        }

        public void AssignCat(AbQuestion question)
        {
            var catRepo = new CategoryRepository();
            var catList = catRepo.GetCategories();

            question.Categories = catList;
        }

        public void DeleteQuestion(int id)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM question WHERE questionid = @id;";
            cmd.Parameters.AddWithValue("id", id);

            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteQuestionFromRating(int id)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM rating WHERE questionid = @id;";
            cmd.Parameters.AddWithValue("id", id);

            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteFromAllTables(int id)
        {
            DeleteQuestionFromRating(id);
            DeleteQuestion(id);
        }

    }
}
