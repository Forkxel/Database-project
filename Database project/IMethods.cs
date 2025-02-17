using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_project
{
    /// <summary>
    /// Interface for methods that are used in table classes
    /// </summary>
    /// <typeparam name="T">table class</typeparam>
    interface IMethods<T>
    {
        void InsertData(T element);
        void UpdateData(T element, List<int> column);
        void DeleteData(T element);
        void WriteAll();
    }
}
