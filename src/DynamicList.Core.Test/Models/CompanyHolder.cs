using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicList.Core.Test.Models
{
	public class CompanyHolder
	{
		public CompanyHolder(ViewModels.CompanyModel _seed_company)
		{
			Company = _seed_company;
		}

		public readonly ViewModels.CompanyModel				Company;
	}
}
