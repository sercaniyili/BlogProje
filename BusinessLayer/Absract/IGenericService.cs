﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Absract
{
    public interface IGenericService<T> 
    {
        void TAdd(T t);
        void TDelete(T t);
        void TUpdate(T t);
        T TGetById(int id);
        IEnumerable<T> GetList();
    }
}
