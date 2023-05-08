﻿using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace TaskManager.Web.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }

    }
}
