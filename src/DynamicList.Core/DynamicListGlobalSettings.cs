using System;
using System.Collections.Generic;
using System.Text;

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
	}
}
