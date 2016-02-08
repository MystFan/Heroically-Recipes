using HeroicallyRecipes.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroicallyRecipes.Tests.TestObjects
{
    public class MemoryRepository<T> : IRepository<T> where T : class
    {
        private readonly IList<T> data;

        public MemoryRepository()
        {
            this.data = new List<T>();
            this.AttachedEntities = new List<T>();
            this.DetachedEntities = new List<T>();
            this.UpdateEntities = new List<T>();
        }

        public IList<T> AttachedEntities { get; private set; }

        public IList<T> DetachedEntities { get; private set; }

        public IList<T> UpdateEntities { get; private set; }

        public bool IsDispose { get; private set; }

        public int NumberOfSaves { get; set; }

        public void Add(T entity)
        {
            this.data.Add(entity);
        }

        public IQueryable<T> All()
        {
            return this.data.AsQueryable();
        }

        public T Attach(T entity)
        {
            this.AttachedEntities.Add(entity);
            return entity;
        }

        public void Delete(object id)
        {
            if(this.data.Count == 0)
            {
                throw new InvalidOperationException();
            }

            this.data.Remove(this.data.FirstOrDefault());
        }

        public void Delete(T entity)
        {
            if (!this.data.Contains(entity))
            {
                throw new InvalidOperationException();
            }

            this.data.Remove(entity);
        }

        public void Detach(T entity)
        {
            if (!this.data.Contains(entity))
            {
                throw new InvalidOperationException();
            }

            this.DetachedEntities.Remove(entity);
        }

        public void Dispose()
        {
            this.IsDispose = true;
        }

        public T GetById(object id)
        {
            return this.data[0];
        }

        public int SaveChanges()
        {
            this.NumberOfSaves++;
            return 1;
        }

        public void Update(T entity)
        {
            this.UpdateEntities.Add(entity);
        }
    }
}
