using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace Zoka.AspNetCore.Components.DynamicList
{
	/// <summary>
	///		Configure options class which allows the static files to be served also from embedded resources.
	/// </summary>
	/// <remarks>The code has been taken from: https://stackoverflow.com/a/53024912/1038496. </remarks>
	public class DynamicListScriptsFromResourceConfigureOptions : IPostConfigureOptions<StaticFileOptions>
	{
		/// <summary></summary>
		public DynamicListScriptsFromResourceConfigureOptions(IHostingEnvironment environment)
		{
			Environment = environment;
		}

		/// <summary></summary>
		public IHostingEnvironment Environment { get; }

		/// <inheritdoc />
		public void PostConfigure(string name, StaticFileOptions options)
		{
			name = name ?? throw new ArgumentNullException(nameof(name));
			options = options ?? throw new ArgumentNullException(nameof(options));

			// Basic initialization in case the options weren't initialized by any other component
			options.ContentTypeProvider = options.ContentTypeProvider ?? new FileExtensionContentTypeProvider();
			if (options.FileProvider == null && Environment.WebRootFileProvider == null)
			{
				throw new InvalidOperationException("Missing FileProvider.");
			}

			options.FileProvider = options.FileProvider ?? Environment.WebRootFileProvider;

			var basePath = "wwwroot";

			var filesProvider = new ManifestEmbeddedFileProvider(GetType().Assembly, basePath);
			options.FileProvider = new CompositeFileProvider(options.FileProvider, filesProvider);
		}
	}
}
