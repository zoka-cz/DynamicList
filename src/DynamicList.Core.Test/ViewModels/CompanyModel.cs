using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Zoka.AspNetCore.Components.DynamicList;

namespace DynamicList.Core.Test.ViewModels
{
	public class CompanyModel
	{
		[DisplayName("Company name")]
		public string										CompanyName { get; set; }

		[DynamicList]
		public List<EmployeeModel>							Employees { get; set; }
	}
}
