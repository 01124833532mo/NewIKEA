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

		T? GetById(int id);
		int Add(T entity);
		int Update(T entity);
		int Delete(T entity);
	}
}
