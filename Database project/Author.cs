﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_project
{
    public class Author
    {
        private int id;
        private string name;

        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }

        public Author(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
    }
}
