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

                while(reader.Read() == true)
                {
                    var currentQuestion = new AbQuestion();
                    currentQuestion.QuestionID = reader.GetInt32("questionid");
                    currentQuestion.AngryBirdQuestion = reader.GetString("angrybirdquestion");

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

                while (reader.Read() == true)
                {
                    question.QuestionID = reader.GetInt32("questionid");
                    question.AngryBirdQuestion = reader.GetString("angrybirdquestion");
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
    }
}
