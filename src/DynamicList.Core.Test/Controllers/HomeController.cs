using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DynamicList.Core.Test.Models;

namespace DynamicList.Core.Test.Controllers
{
	public class HomeController : Controller
	{
		private readonly Models.CompanyHolder				m_CompanyHolder;

		public HomeController(Models.CompanyHolder _company_holder)
		{
			m_CompanyHolder = _company_holder;
		}

		public IActionResult Index()
		{
			var model = m_CompanyHolder.Company;

			return View(model);
		}

		[HttpPost]
		public IActionResult Index(ViewModels.CompanyModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			// find deleted & updated
			var deleted = new List<ViewModels.EmployeeModel>();
			var updated = new List<ViewModels.EmployeeModel>();
			var added = new List<ViewModels.EmployeeModel>();
			foreach (var employee in m_CompanyHolder.Company.Employees)
			{
				// find the form item which corresponds to the id
				var employee_from_form = model.Employees.FirstOrDefault(e => e.Id == employee.Id);
				// is it deleted?
				if (employee_from_form == null)
					deleted.Add(employee);
				// is it updated?
				else if (!employee.Equals(employee_from_form))
					updated.Add(employee_from_form);
			}
			// add newly added
			added.AddRange(model.Employees.Where(e => e.Id == 0));

			// perform changes
			// change company name
			m_CompanyHolder.Company.CompanyName = model.CompanyName;
			// add newly added
			m_CompanyHolder.Company.Employees.AddRange(added);
			// delete deleted
			foreach (var emp_to_del in deleted)
			{
				m_CompanyHolder.Company.Employees.Remove(emp_to_del);
			}
			// update updated
			foreach (var emp_to_update in updated)
			{
				var idx = m_CompanyHolder.Company.Employees.FindIndex(e => e.Id == emp_to_update.Id);
				if (idx != -1)
					m_CompanyHolder.Company.Employees[idx] = emp_to_update;
			}

			return RedirectToAction("Index");

		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
