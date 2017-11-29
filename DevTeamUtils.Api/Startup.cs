using DevTeamUtils.Api.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace DevTeamUtils.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
		/*
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
		*/

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });

            services.AddMongo(Configuration.GetSection("ConnectionStrings"));

            services.AddMvc();
            
            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "DevTeam Utils", Version = "v1" });
            });

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        [Microsoft.AspNetCore.Cors.EnableCors("AllowAll")]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();
            //app.UseFileServer();

            loggerFactory.AddConsole();
            app.UseStatusCodePages();
            //env.EnvironmentName = EnvironmentName.Production;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");
            app.UseMvc();
            app.UseOwin();
            
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {   
                #if DEBUG
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DeveTeamUtils API v1 (DEBUG)");
                #else
                    c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "DeveTeamUtils API v1 (RELEASE)");
                #endif
            });

            app.UseSignalR(routes =>
            {
                routes.MapHub<Chat>("messenger");
            });
        }
    }
}
