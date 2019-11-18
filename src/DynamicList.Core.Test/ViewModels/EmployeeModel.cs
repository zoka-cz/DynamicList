using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DynamicList.Core.Test.ViewModels
{
	public class EmployeeModel : IEquatable<EmployeeModel>
	{
		[HiddenInput(DisplayValue = false)]
		public int											Id { get; set; }
		public string										Name { get; set; }
		[Required]
		public string										Surname { get; set; }
		public int											Age { get; set; }

		/// <inheritdoc />
		public bool Equals(EmployeeModel other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			return Id == other.Id && string.Equals(Name, other.Name) && string.Equals(Surname, other.Surname) && Age == other.Age;
		}

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;
			if (ReferenceEquals(this, obj))
				return true;
			if (obj.GetType() != this.GetType())
				return false;
			return Equals((EmployeeModel) obj);
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Id;
				hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (Surname != null ? Surname.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ Age;
				return hashCode;
			}
		}
	}
}
