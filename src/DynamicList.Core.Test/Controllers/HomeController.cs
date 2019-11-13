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


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
