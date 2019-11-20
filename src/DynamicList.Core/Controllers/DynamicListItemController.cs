using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Zoka.AspNetCore.Components.DynamicList.Controllers
{
	/// <summary></summary>
	public class DynamicListItemController : Controller
	{
		/// <summary></summary>
		public IActionResult								NewItem(string item_type_fullname, string model_prefix, int item_index)
		{
			var object_type = Type.GetType(item_type_fullname);
			var obj = Activator.CreateInstance(object_type);

			ViewData.Add("HtmlFieldPrefix", model_prefix);
			ViewData.Add("ItemIndex", item_index);

			return PartialView("_DynamicListDefaultRowPartial", obj);
		}
	}
}
