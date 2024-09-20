﻿using Link.Dev.IKEA.DAL.Data;
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
	public class GenericRepository<T> where T : ModelBase
	{
		private protected readonly ApplicationDbContext _dbContext;
		public GenericRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public IEnumerable<T> GetAll(bool AsNoTraking = true)
		{
			if (AsNoTraking)
			{
				return _dbContext.Set<T>().AsNoTracking().ToList();
			}
			return _dbContext.Set<T>().ToList();
		}
		public T? GetById(int id)
		{
			///var T = _dbContext.Ts.Local.FirstOrDefault(d => d.Id == id);
			///if(T is null)
			///	T = _dbContext.Ts.FirstOrDefault(d => d.Id == id);
			///return T;
			return _dbContext.Set<T>().Find(id);
		}
		public IQueryable<T> GetAllAsIQueryable()
		{
			return _dbContext.Set<T>();
		}
		public int Add(T entity)
		{
			_dbContext.Set<T>().Add(entity);
			return _dbContext.SaveChanges();
		}
		public int Update(T entity)
		{
			_dbContext.Set<T>().Update(entity);
			return _dbContext.SaveChanges();
		}
		public int Delete(T entity)
		{
			_dbContext.Set<T>().Remove(entity);
			return _dbContext.SaveChanges();
		}
	}
}