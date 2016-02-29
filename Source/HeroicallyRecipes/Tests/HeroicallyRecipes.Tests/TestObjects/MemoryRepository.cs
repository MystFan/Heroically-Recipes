namespace HeroicallyRecipes.Tests.TestObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using HeroicallyRecipes.Data.Models;
    using HeroicallyRecipes.Data.Repositories;

    public class MemoryRepository<T> : IDbRepository<T, int>
        where T : BaseModel<int>
    {
        private readonly IList<T> data;

        public MemoryRepository()
        {
            this.data = new List<T>();
        }

        public int NumberOfSaves { get; set; }

        public IQueryable<T> All()
        {
            return this.data.Where(m => !m.IsDeleted).AsQueryable();
        }

        public IQueryable<T> AllWithDeleted()
        {
            return this.data.AsQueryable();
        }

        public T GetById(int id)
        {
            if (this.data.Count == 0)
            {
                throw new ArgumentNullException("No such entity - invalid id");
            }

            return this.data[id];
        }

        public void Add(T entity)
        {
            this.data.Add(entity);
        }

        public void Delete(T entity)
        {
            if (!this.data.Contains(entity))
            {
                throw new ArgumentNullException("No such entity");
            }

            var databaseEntity = this.data.FirstOrDefault(e => e.Id == entity.Id);
            databaseEntity.IsDeleted = true;
        }

        public void HardDelete(T entity)
        {
            if (!this.data.Contains(entity))
            {
                throw new ArgumentNullException("No such entity");
            }

            this.data.Remove(entity);
        }

        public void SaveChanges()
        {
            ++this.NumberOfSaves;
        }
    }
}
