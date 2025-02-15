﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_project
{
    interface Methods<T>
    {
        void InsertData(T element);
        void UpdateData(T element);
        void DeleteData(T element);
    }
}
