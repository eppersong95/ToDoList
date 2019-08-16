using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.ViewModels;
using ToDoList.Enums;

namespace ToDoList.Controllers
{
    public class ToDoController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        //used for building the list in the UI
        [HttpGet]
        public IActionResult GetToDoList(int sortID)
        {
            var itemList = new List<ToDoItem>();

            switch ((SortOptions)sortID)
            {
                case SortOptions.NewestFirst:
                    itemList = _context.ToDoItems.OrderByDescending(m => m.ID).ToList();
                    break;
                case SortOptions.OldestFirst:
                    itemList = _context.ToDoItems.OrderBy(m => m.ID).ToList();
                    break;
                case SortOptions.PriorityDesc:
                    itemList = _context.ToDoItems.OrderByDescending(m => m.PriorityID).ToList();
                    break;
                case SortOptions.PriorityAsc:
                    itemList = _context.ToDoItems.OrderBy(m => m.PriorityID).ToList();
                    break;
                default: //completed at bottom
                    itemList = _context.ToDoItems.OrderBy(m => m.IsComplete).ToList();
                    break;
            }

            return Json(itemList);
        }

        //returns a form for adding or editing an item
        [HttpGet]
        public IActionResult AddEditToDoItem(long id = 0)
        {
            var toDoItemVM = new ToDoItemFormVM();

            if (id != 0)
            {
                var existingItem = _context.ToDoItems
                     .Where(m => m.ID == id)
                     .Single();
                toDoItemVM = new ToDoItemFormVM(existingItem);
            }

            return View(toDoItemVM);
        }

        //gets input from form and processes it in the database
        [HttpPost]
        public IActionResult AddEditToDoItem(ToDoItem item)
        {
            //don't process invalid data
            if (!ModelState.IsValid)
            {
                var itemFormVM = new ToDoItemFormVM(item);
                return View("AddEditToDoItem", itemFormVM);
            }

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

        [HttpPost]
        public JsonResult ChangeItemStatus(long itemID, bool isComplete)
        {
            var toDoItem = _context.ToDoItems.Where(m => m.ID == itemID).Single();
            toDoItem.IsComplete = isComplete;

            _context.Update(toDoItem);
            _context.SaveChanges();

            return Json(new { isSuccess = true });
        }


        [HttpDelete]
        public IActionResult DeleteToDoItem(long itemID)
        {
            var itemToDelete = _context.ToDoItems.Where(m => m.ID == itemID).Single();
            _context.Remove(itemToDelete);
            _context.SaveChanges();

            return new JsonResult(new { isSuccess = true });
        }

        [HttpDelete]
        public IActionResult DeleteCompletedToDoItems()
        {
            var completedItems = _context.ToDoItems.Where(m => m.IsComplete).ToList();

            if (completedItems.Count == 0)
            {
                return Json(new { isSuccess = false });
            }
            _context.RemoveRange(completedItems);
            _context.SaveChanges();

            return Json(new { isSuccess = true });
        }
    }
}