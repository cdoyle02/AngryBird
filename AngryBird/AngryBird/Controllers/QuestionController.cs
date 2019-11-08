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

        public IActionResult ViewQuestion(int id)
        {
            QuestionRepository repo = new QuestionRepository();
            AbQuestion question = repo.GetQuestion(id);

            return View(question);
        }

        public IActionResult UpdateQuestion(int id)
        {
            QuestionRepository repo = new QuestionRepository();
            AbQuestion quest = repo.GetQuestion(id);
            repo.AssignCat(quest);

            //repo.UpdateQuestion(quest);

            if(quest == null)
            {
                return View("QuestionNotFound");
            }
            return View(quest);

        }

        public IActionResult UpdateQuestionToDatabase(AbQuestion question)
        {
            var repo = new QuestionRepository();

            var catRepo = new CategoryRepository();
            catRepo.GetCategoryName(question);

            repo.UpdateQuestion(question);

            return RedirectToAction("ViewQuestion", new { id = question.QuestionID });
        }

        public IActionResult InsertQuestion()
        {
            var repo = new QuestionRepository();
            var quest = repo.AssignCategories(); 

                return View(quest);
        }

        public IActionResult InsertQuestionIntoDatabase(AbQuestion questionToInsert)
        {
            var repo = new QuestionRepository();

            repo.InsertQuestion(questionToInsert);
            repo.InsertRating(questionToInsert);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteQuestion(AbQuestion question)
        {
            var repo = new QuestionRepository();

            repo.DeleteFromAllTables(question.QuestionID);

            return RedirectToAction("Index");
        }


    }
}
