using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Zoka.AspNetCore.Components.DynamicList.Controllers
{
	/// <summary>
	///		Default controller action to create editor for single item from DynamicList.
	///		It creates the model render its editor (adapted to use indexing) and return it as PartialView.
	/// </summary>
	public class DynamicListItemController : Controller
	{
		/// <summary>
		///		Will return new item editor as PartialView for desired model type, using naming composed
		///		using <paramref name="model_prefix">model_prefix</paramref> and <paramref name="item_index">item_index</paramref>.
		/// </summary>
		/// <param name="item_type_fullname">
		///		The name of the model Type to be created. As the model is accessed from different assembly, you should
		///		pass the AssemblyQualifiedName property of the Type.
		/// </param>
		/// <param name="model_prefix">
		///		The prefix of the property names to be used when rendering properties of model
		/// </param>
		/// <param name="item_index">
		///		The index of the item in the DynamicList. Usually it should be the latest item index + 1.
		/// </param>
		/// <param name="row_template_name">
		///		[Optional] The name of the PartialView template, which is used to render item editor for DynamicList.
		///		If not set, the default value defined in <see cref="DynamicListGlobalSettings.DefaultListItemTemplatePartial"/>
		///		is used.
		/// </param>
		public IActionResult								NewItem(string item_type_fullname, string model_prefix, int item_index, string row_template_name = null)
		{
			// Create model type
			var object_type = Type.GetType(item_type_fullname);
			var obj = Activator.CreateInstance(object_type);

			// The partial view to render the editor need following values to work correctly
			ViewData.Add("HtmlFieldPrefix", model_prefix);
			ViewData.Add("ItemIndex", item_index);

			// Render as partial view
			return PartialView(row_template_name ?? DynamicListGlobalSettings.DefaultListItemTemplatePartial, obj);
		}
	}
}
