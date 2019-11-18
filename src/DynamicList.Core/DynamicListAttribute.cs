using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Zoka.AspNetCore.Components.DynamicList
{
	/// <summary></summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class DynamicListAttribute : Attribute, IDisplayMetadataProvider
	{
		/// <summary>If defined, and PartialView by this name exists, it is used instead of default template for rendering the Item in the list</summary>
		public string										ListItemTemplatePartial { get; set; }
		/// <summary>If defined, and PartialView by this name exists, it is used instead of default template for rendering the "Add item to the list" button</summary>
		public string										AddItemTemplatePartial { get; set; }

		/// <inheritdoc />
		public void											CreateDisplayMetadata(DisplayMetadataProviderContext context)
		{
			var dynamic_list_attribute = context.Attributes.FirstOrDefault(a => a.GetType() == this.GetType()) as DynamicListAttribute;
			if (dynamic_list_attribute == null)
				return;
			context.DisplayMetadata.TemplateHint = "DynamicList";
			if (!string.IsNullOrWhiteSpace(dynamic_list_attribute.ListItemTemplatePartial))
				context.DisplayMetadata.AdditionalValues.Add("DynamicListItemTemplatePartial", dynamic_list_attribute.ListItemTemplatePartial);
			if (!string.IsNullOrWhiteSpace(dynamic_list_attribute.AddItemTemplatePartial))
				context.DisplayMetadata.AdditionalValues.Add("DynamicListAddItemTemplatePartial", dynamic_list_attribute.AddItemTemplatePartial);
		}
	}
}
