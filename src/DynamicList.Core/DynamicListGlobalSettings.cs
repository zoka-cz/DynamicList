using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Zoka.AspNetCore.Components.DynamicList
{
	/// <summary>
	/// Global scope settings for all of DynamicList instances made during application lifetime, unless
	/// explicitly changed
	/// </summary>
	public static class DynamicListGlobalSettings
	{
		/// <summary>The default partial view, which renders the single list item.</summary>
		public static string								DefaultListItemTemplatePartial { get; set; } = "_DynamicListDefaultRowPartial";

		/// <summary>The default partial view, which renders the "Add button", which adds new item into list</summary>
		public static string								DefaultAddItemTemplatePartial { get; set; } = "_DynamicListDefaultAddRowPartial";

		/// <summary>
		///		Will configure the services so the DynamicList component is working correctly.
		///		Specifically it allows DynamicListAttribute to be used on the model property, which inform razor to work
		///		with List&lt;T&gt; as with DynamicList component.
		///		Next, it tells Mvc to serve some files from embedded resources (~/lib/DynamicList/DynamicList.js)
		/// </summary>
		public static void									UseDynamicListComponent(this IServiceCollection services)
		{
			services.Configure<MvcOptions>(o => {
				o.ModelMetadataDetailsProviders.Add(new DynamicListAttribute());
			});
			services.ConfigureOptions(typeof(DynamicListScriptsFromResourceConfigureOptions));
		}
	}
}
