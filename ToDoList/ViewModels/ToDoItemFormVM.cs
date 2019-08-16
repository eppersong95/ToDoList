using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ToDoList.Models;

namespace ToDoList.ViewModels
{
    public class ToDoItemFormVM
    {
        public int ID { get; set; }

        [Required]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Priority")]
        public int PriorityID { get; set; }

        public string Title
        {
            get
            {
                return ID == 0 ? "Add a New Item" : "Edit Item";
            }
        }

        public ToDoItemFormVM()
        {
            ID = 0;
        }

        public ToDoItemFormVM(ToDoItem item)
        {
            Description = item.Description;
            PriorityID = item.PriorityID;
            ID = item.ID;
        }
    }
}
