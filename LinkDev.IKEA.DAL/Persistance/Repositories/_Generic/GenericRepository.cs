using Link.Dev.IKEA.DAL.Data;
using Link.Dev.IKEA.DAL.Entites;
using LinkDev.IKEA.DAL.Entites.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.Repositories._Generic
{
	public class GenericRepository<T> :IGenericRepository<T> where T : ModelBase
	{
		private protected readonly ApplicationDbContext _dbContext;
		public GenericRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task <IEnumerable<T>> GetAllAsynce(bool AsNoTraking = true)
		{
			if (AsNoTraking)
			{
                return await _dbContext.Set<T>().Where(d=>!d.IsDeleted).AsNoTracking().ToListAsync();
			}
			return await _dbContext.Set<T>().Where(d => !d.IsDeleted).ToListAsync();
		}
		public async Task< T?> GetByIdAsynce(int id)
		{
			///var T = _dbContext.Ts.Local.FirstOrDefault(d => d.Id == id);
			///if(T is null)
			///	T = _dbContext.Ts.FirstOrDefault(d => d.Id == id);
			///return T;
			return await  _dbContext.Set<T>().FindAsync(id);
		}
		public IQueryable<T> GetAllAsIQueryable()
		{
			return _dbContext.Set<T>();
		}

        //public IEnumerable<T> GetAllAsIEnumrable()
        //{
        //    return _dbContext.Set<T>();
        //}
        public void Add(T entity)
		{
			 _dbContext.Set<T>().Add(entity);
		}
		public void Update(T entity)
		{
			_dbContext.Set<T>().Update(entity);
		}
		public void Delete(T entity)
		{
			entity.IsDeleted = true;
			_dbContext.Set<T>().Update(entity);
		}

       
    }
}
