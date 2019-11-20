using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Zoka.AspNetCore.Components.DynamicList.TagHelper
{
	/// <summary></summary>
	public class TemplateScriptsTagHelper : Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
	{
		/// <summary></summary>
		[ViewContext]
		public ViewContext ViewContext { get; set; }

		/// <summary></summary>
		public string KeyName { get; set; }

		/// <summary></summary>
		public bool Render { get; set; }

		/// <summary></summary>
		public const string									DEFAULT_KEY_NAME = "TemplateScripts";

		/// <inheritdoc />
		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			var key = string.IsNullOrEmpty(KeyName) ? DEFAULT_KEY_NAME : KeyName;
			if (Render)
			{

				if (ViewContext.TempData.ContainsKey(key))
				{
					output.TagName = null;
					output.Content.Clear();
					output.Content.AppendHtml(ViewContext.TempData[key] as string);
				}
				else
				{
					output.SuppressOutput();
				}
			}
			else
			{
				var inner_html = output.GetChildContentAsync().Result;

				if (ViewContext.TempData.ContainsKey(key))
					ViewContext.TempData[key] = ViewContext.TempData[key] + inner_html.GetContent();
				else
					ViewContext.TempData[key] = inner_html.GetContent();
				output.SuppressOutput();
			}
		}

	}
}
