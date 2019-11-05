using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngryBird.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AngryBird.Controllers
{
    public class QuestionController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            QuestionRepository repo = new QuestionRepository();
            List<AbQuestion> questions = repo.GetAllQuestions();

            return View(questions);
        }

        //public IActionResult ViewQuestion()
        //{
        //    QuestionRepository repo = new QuestionRepository();
        //    AbQuestion question = repo.GetAllQuestions(id)
        //}
    }
}
