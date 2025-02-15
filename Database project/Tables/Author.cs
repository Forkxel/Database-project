using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_project.Tables
{
    public class Author : Methods<Author>
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Author(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public Author()
        {
        }

        public void InsertData(Author element)
        {
            throw new NotImplementedException();
        }

        public void UpdateData(Author element)
        {
            throw new NotImplementedException();
        }

        public void DeleteData(Author element)
        {
            throw new NotImplementedException();
        }
    }
}
