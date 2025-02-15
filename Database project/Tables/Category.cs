using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_project.Tables
{
    public class Category : Methods<Category>
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Category(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public Category()
        {
        }

        public void InsertData(Category element)
        {
            throw new NotImplementedException();
        }

        public void UpdateData(Category element)
        {
            throw new NotImplementedException();
        }

        public void DeleteData(Category element)
        {
            throw new NotImplementedException();
        }
    }
}
