using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Zoka.AspNetCore.Components.DynamicList.TagHelper
{
	/// <summary>
	///	<para>
	///		Tag helper, which allows user to render scripts from EditorTemplates or Partials like he is used to
	///		do in @section Scripts in regular View.
	/// </para>
	/// <para>
	///		section Scripts may be used only in Views, and rendered later in Layout page. But when some section is used
	///		in the PartialView or Editor/DisplayTemplate, it is ignored when requested to render. This helpers helps to
	///		overcome the issue.
	/// </para>
	/// <example>
	///	<para>
	///		Here is the example of the usage. First the code to insert into your PartialView or Editor/DisplayTemplate.
	///	</para>
	///	<code>
	///		&lt;template-scripts key-name="HelloWorldScript" &gt;
	///			&lt;script&gt;
	///				alert("Hello from partial");
	///			&lt;/script&gt;
	///		&lt;/template-scripts&gt;
	/// </code>
	/// <para>
	///		Then you place this code in you view, or even in the Layout page.
	///	</para>
	/// <code>
	///		&lt;template-scripts key-name="HelloWorldScript" render &gt;&lt;/template-scripts&gt;
	/// </code>
	/// <para>
	///		This code ensures, that the code javascript code is rendered on the place in you View/Layout.
	/// </para>
	/// <para>
	///		The key-name attribute is not necessary to use. When not used, the TagHelpers accumulate the scripts
	///		(or other content) in the default storage. On rendering it renders either from default storage, or
	///		from the storage marked by the key-name.
	/// </para>
	/// </example>
	///		
	///
	///		
	/// </summary>
	public class TemplateScriptsTagHelper : Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
	{
		/// <summary>Injected ViewContext to get access to the storage</summary>
		[ViewContext]
		public ViewContext									ViewContext { get; set; }

		/// <summary>
		///		The name of the storage to store inner html into.
		///		May be ommited, in which case the html is stored into default storage name.
		/// </summary>
		public string										KeyName { get; set; }

		/// <summary>
		///		When specified in tag, the content in the <see cref="KeyName"/> storage is rendered.
		///		If there is any other HTML inside the tag, it is ignored when render attribute is used.
		/// </summary>
		public bool Render { get; set; }

		/// <summary>Name of the default storage.</summary>
		public const string									DEFAULT_KEY_NAME = "TemplateScripts";

		/// <inheritdoc />
		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			// resolve the key of the storage
			var key = string.IsNullOrEmpty(KeyName) ? DEFAULT_KEY_NAME : KeyName;
			if (Render)
			{
				if (ViewContext.TempData.ContainsKey(key))
				{
					// render onto page, without any boundary tags
					output.TagName = null;
					output.Content.Clear();
					output.Content.AppendHtml(ViewContext.TempData[key] as string);
				}
				else
				{
					// but if there is not content, do not render anything
					output.SuppressOutput();
				}
			}
			else
			{
				// get inner html
				var inner_html = output.GetChildContentAsync().Result;

				// and store it into the storage (appending)
				if (ViewContext.TempData.ContainsKey(key))
					ViewContext.TempData[key] = ViewContext.TempData[key] + inner_html.GetContent();
				else
					ViewContext.TempData[key] = inner_html.GetContent();

				// and do not render anything on the original place, it will be rendered later
				output.SuppressOutput();
			}
		}

	}
}
