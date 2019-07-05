﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HomeworkDapper
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }
    }
}
