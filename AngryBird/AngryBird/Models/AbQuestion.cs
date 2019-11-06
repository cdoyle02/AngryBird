using System;
using System.Collections.Generic;
namespace AngryBird.Models
{
    public class AbQuestion
    {
        public int QuestionID { get; set; }
        public string AngryBirdQuestion { get; set; }
        public int? RatingID { get; set; }
        public int? CategoryID { get; set; }

        public List<Category> Categories { get; set; }
    }
}
