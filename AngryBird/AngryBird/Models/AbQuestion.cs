using System;
using System.Collections.Generic;
namespace AngryBird.Models
{
    public class AbQuestion
    {
        public int QuestionID { get; set; }
        public string AngryBirdQuestion { get; set; }
        public int? QuestionRating { get; set; }

        //public List<category> questionCategory { get; set; }
    }
}
