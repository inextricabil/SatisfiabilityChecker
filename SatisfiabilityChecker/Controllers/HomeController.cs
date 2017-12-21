using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SatisfiabilityChecker.Models;

namespace SatisfiabilityChecker.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckSatisfiability(string Expression)
        {
            if (string.IsNullOrEmpty(Expression))
            {
                return RedirectToAction("Index");
            } else
            {
                TempData["Expression"] = Expression;

                //Overview
                TempData["Overview"] = null;

                //Result
                var checkSatisfiability = GetFormulaSatifsiability(Expression);
                TempData["Result"] = checkSatisfiability;

                return RedirectToAction("Index");
            }
        }

        private bool GetFormulaSatifsiability(string expression)
        {
            if(expression == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
