﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class ToDoItem
    {
        public int ID { get; set; }
        [Required]
        public bool IsComplete { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
    }
}
    