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

		Task <IEnumerable<T>> GetAllAsynce(bool AsNoTraking = true);
		IQueryable<T> GetAllAsIQueryable();
        //IEnumerable<T> GetAllAsIEnumrable();

       Task< T?> GetByIdAsynce(int id);
		void Add(T entity);

		void Update(T entity);
		void Delete(T entity);
	}
}
