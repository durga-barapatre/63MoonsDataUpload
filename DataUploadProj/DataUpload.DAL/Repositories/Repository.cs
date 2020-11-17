using DataUpload.DAL.Contexts;
using DataUpload.Domain.Domain;
using DataUpload.Domain.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUpload.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext _applicationContext;
        public Repository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public IQueryable<T> Entities
        {
            get
            {
                return this.Table;
            }
        }
        public DbSet<T> Table
        {
            get
            {
                return _applicationContext.Set<T>();
            }
        }
        public virtual void Add(T entity)
        {
            Table.Add(entity);
            _applicationContext.SaveChanges();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Table.ToList();
        }


        public void Update(T entity)
        {
            this.Table.AddOrUpdate(entity);
            _applicationContext.SaveChanges();
        }
    }
}
