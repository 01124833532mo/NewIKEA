﻿using Link.Dev.IKEA.DAL.Data;
using LinkDev.IKEA.DAL.Entites.Departments;
using LinkDev.IKEA.DAL.Entites.Employees;
using LinkDev.IKEA.DAL.Persistance.Repositories._Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.Repositories.Employees
{
	public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
	{
		public EmployeeRepository(ApplicationDbContext context) : base(context) { }

	}
}
