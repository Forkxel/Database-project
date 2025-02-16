using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_project
{
    interface IMethods<T>
    {
        void InsertData(T element);
        void UpdateData(T element, List<int> column);
        void DeleteData(T element);
        void WriteAll();
    }
}
