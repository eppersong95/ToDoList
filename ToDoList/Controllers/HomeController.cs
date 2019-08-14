using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using Microsoft.EntityFrameworkCore;


namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        public IActionResult Index()
        { 
            var toDoItems = _context.ToDoItems.ToList();
            return View(toDoItems);
        }

        [HttpGet]
        public IActionResult AddEditToDoItem(int? itemID = 0)
        {
            var toDoItem = new ToDoItem();

            if (itemID != 0)
            {
               toDoItem =  _context.ToDoItems
                    .Where(m => m.ID == itemID)
                    .Single();
            }

            return View(toDoItem);
        }

        [HttpPost]
        public IActionResult AddEditToDoItem(ToDoItem item)
        {
            if (item.ID == 0)
            {
                AddNewToDoItem(item);
            }

            else
            {
                UpdateToDoItem(item);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public JsonResult ChangeItemStatus(long itemID, bool isComplete)
        {
            var toDoItem = _context.ToDoItems.Where(m => m.ID == itemID).Single();
            toDoItem.IsComplete = isComplete;

            _context.Update(toDoItem);
            _context.SaveChanges();

            return new JsonResult(new { isSuccess = "1" });
        }

        private void AddNewToDoItem(ToDoItem item)
        {
            item.DateCreated = DateTime.Now;
            _context.Add(item);
            _context.SaveChanges();
        }

        private void UpdateToDoItem(ToDoItem item)
        {
            var existingItem = _context.ToDoItems.Where(m => m.ID == item.ID).Single();
            existingItem.Description = item.Description;
            existingItem.IsComplete = item.IsComplete;

            _context.ToDoItems.Update(existingItem);
            _context.SaveChanges();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
