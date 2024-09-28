using Link.Dev.IKEA.DAL.Entites;
using LinkDev.IKEA.DAL.Entites.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.Repositories._Generic
{
	public interface IGenericRepository<T> where T : ModelBase
	{
		IEnumerable<T> GetAll(bool AsNoTraking = true);
		IQueryable<T> GetAllAsIQueryable();
        //IEnumerable<T> GetAllAsIEnumrable();

        T? GetById(int id);
		void Add(T entity);
		void Update(T entity);
		void Delete(T entity);
	}
}
