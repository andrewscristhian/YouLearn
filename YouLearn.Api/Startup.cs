using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using YouLearn.Domain.Interfaces.Repositories;
using YouLearn.Domain.Interfaces.Services;
using YouLearn.Domain.Services;
using YouLearn.Infra.Persistence.EF;
using YouLearn.Infra.Persistence.Repositories;
using YouLearn.Infra.Transactions;

namespace YouLearn.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Adiciona a injeção de dependencia
            services.AddScoped<YouLearnContext, YouLearnContext>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            //Services
            //services.AddTransient<IServiceCanal, ServiceCanal>();
            //services.AddTransient<IServicePlayList, ServicePlayList>();
            //services.AddTransient<IServiceVideo, ServiceVideo>();
            services.AddTransient<IServiceUsuario, ServiceUsuario>();

            //Repositories
            //services.AddTransient<IRepositoryCanal, RepositoryCanal>();
            //services.AddTransient<IRepositoryPlayList, RepositoryPlayList>();
            //services.AddTransient<IRepositoryVideo, RepositoryVideo>();
            services.AddTransient<IRepositoryUsuario, RepositoryUsuario>();

            services.AddRazorPages();

            //Aplicando documentação com swagger
            services.AddSwaggerGen(x => {
                x.SwaggerDoc("v1", new OpenApiInfo() { Title = "YouLearn", Version = "v1" });
            });

            services.AddMvc(Options =>
            {
                Options.EnableEndpointRouting = false;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "YouLearn - V1");
            });

            app.UseRouting();

            // Redireciona o Link para o Swagger, quando acessar a rota principal
            //var option = new RewriteOptions();
            //option.AddRedirect("^$", "swagger");
            //app.UseRewriter(option);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
