﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_project.Columns
{
    public class Book
    {
        private float price;

        public int ID { get; set; }
        public string Title { get; set; }
        public int AuthorID { get; set; }
        public int CategoryID { get; set; }
        public float Price
        {
            get => price;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price cannot be negative");
                }
                price = value;
            }
        }
        public bool IsAvailable { get; set; }

        public Book(int id, string title, int authorID, int categoryID, float price, bool isAvailable)
        {
            ID = id;
            Title = title;
            AuthorID = authorID;
            CategoryID = categoryID;
            Price = price;
            IsAvailable = isAvailable;
        }
    }
}
