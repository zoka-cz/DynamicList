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
		/// <inheritdoc />
		public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
		{
			if (!context.Attributes.Any(a => a.GetType() == this.GetType()))
				return;
			context.DisplayMetadata.TemplateHint = "DynamicList";
		}
	}
}
