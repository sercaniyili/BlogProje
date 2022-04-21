﻿using DataAccessLayer.Abstract;
using DataAccessLayer.Contrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        //private readonly Context c;
        //private DbSet<T> c;
        //public GenericRepository(Context context)
        //{
        //    c = context;
        //    c = c.Set<T>();
        //}

        public void Delete(T entity)
        {
            using var c = new Context();
            c.Remove(entity);
            c.SaveChanges();
        }

        public T GetById(int id)
        {
            using var c = new Context();
            return c.Set<T>().Find(id);
        }

        public IEnumerable<T> GetListAll()
        {

            using var c = new Context();
            return c.Set<T>().ToList();           
        }

        public void Insert(T entity)
        {

            using var c = new Context();
            c.Add(entity);
            c.SaveChanges();
        }

        public List<T> GetListAll(Expression<Func<T, bool>> filter)
        {
            using var c = new Context();
            return c.Set<T>().Where(filter).ToList();
        }

        public void Update(T entity)
        {

            using var c = new Context();
            c.Update(entity);
            c.SaveChanges();
        }
    }
}