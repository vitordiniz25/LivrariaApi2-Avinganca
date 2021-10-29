using ElmahCore;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using Livraria2.Api.Middlewares;
using Livraria2.Domain.Handlers;
using Livraria2.Domain.Interfaces.Repositories;
using Livraria2.Infra.Data.DataContexts;
using Livraria2.Infra.Data.Repositories;
using Livraria2.Infra.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Livraria2.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region AppSettings

            AppSettings appSettings = new();
            Configuration.GetSection("AppSettings").Bind(appSettings);
            services.AddSingleton(appSettings);

            #endregion

            #region Elmah
            services.AddElmah();
            services.AddElmah<XmlFileErrorLog>(option =>
            {
                option.LogPath = "~/log";
            });
            services.AddElmah<SqlErrorLog>(option =>
            {
                option.ConnectionString = appSettings.ConnectionString;
            });

            #endregion

            #region DataContext

            services.AddScoped<DataContext>();

            #endregion

            #region Repositories

            services.AddTransient<ILivroRepository, LivroRepository>();

            #endregion

            #region Handlers

            services.AddTransient<LivroHandler, LivroHandler>();

            #endregion


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Livraria2.Api", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Livraria2.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseElmah();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
