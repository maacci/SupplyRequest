using SupplyRequestAPI.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace SupplyRequestAPI
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
			services.AddScoped<IOrderItemRepository, OrderItemRepository>();
			services.AddScoped<IOrderRepository, OrderRepository>();			
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IVendorRepository, VendorRepository>();
			services.AddScoped<IOrderVendorRepository, OrderVendorRepository>();
			services.AddControllers().AddJsonOptions(x =>
				{
					x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
					x.JsonSerializerOptions.MaxDepth = 0;
				});
			services.AddDbContext<Models.SupplyRequestContext>(o =>
				o.UseLazyLoadingProxies()
				.UseSqlite("Data Source=MedicalSupply.db"));
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "SupplyRequestAPI", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SupplyRequestAPI v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
