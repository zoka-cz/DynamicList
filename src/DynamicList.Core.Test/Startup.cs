using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DynamicList.Core.Test.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicList.Core.Test
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options => {
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.Add(new ServiceDescriptor(typeof(Models.CompanyHolder), new Models.CompanyHolder(new ViewModels.CompanyModel() {
				CompanyName = "My Greatest Company", 
				Employees = new List<EmployeeModel> {
					new ViewModels.EmployeeModel() { Name = "Fred", Surname = "Oldman", Age = 65 },
					new ViewModels.EmployeeModel() { Name = "John", Surname = "Newman", Age = 19 },
					new ViewModels.EmployeeModel() { Name = "Jessica", Surname = "Middlewoman", Age = 37 }
				}
			})));

			services
				.AddMvc()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
				.AddMvcOptions(o => o.ModelMetadataDetailsProviders.Add(new Zoka.AspNetCore.Components.DynamicList.DynamicListAttribute()));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseMvc(routes => {
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
