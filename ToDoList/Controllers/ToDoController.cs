using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ToDoController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddEditToDoItem(long id = 0)
        {
            var toDoItem = new ToDoItem();

            if (id != 0)
            {
                toDoItem = _context.ToDoItems
                     .Where(m => m.ID == id)
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
            existingItem.PriorityID = item.PriorityID;

            _context.ToDoItems.Update(existingItem);
            _context.SaveChanges();
        }

        [HttpDelete]
        public IActionResult DeleteToDoItem(long itemID)
        {
            var itemToDelete = _context.ToDoItems.Where(m => m.ID == itemID).Single();
            _context.Remove(itemToDelete);
            _context.SaveChanges();

            return new JsonResult(new { isSuccess = "true" });
        }
    }
}