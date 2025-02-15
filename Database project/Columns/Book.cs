using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_project.Columns
{
    public class Book
    {
        private int id;
        private string title;
        private int authorID;
        private int categoryID;
        private float price;
        private bool isAvailable;

        public int ID { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public int AuthorID { get => authorID; set => authorID = value; }
        public int CategoryID { get => categoryID; set => categoryID = value; }
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

        public bool IsAvailable { get => isAvailable; set => isAvailable = value; }

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
