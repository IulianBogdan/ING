using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChallengeING.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ChallengeING.Models.Interfaces;
using ChallengeING.Data.Repositories;
using Microsoft.OpenApi.Models;

namespace ChallengeING.Api
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
            services.AddDbContext<SqlDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MSSQL")));

            services.AddSingleton(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddSingleton<IAccountRepository, AccountRepository>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("ING Challenge", new OpenApiInfo
                {
                    Title = "ING Interview Challenge",
                    Version = "v1",
                    Description = "Interview coding challenge to assess skills.",
                    TermsOfService = new Uri("https://wikipedia.com"),
                    Contact = new OpenApiContact
                    {
                        Name = "Bogdan Iulian Vasile",
                        Email = string.Empty,
                        Url = new Uri("https://www.linkedin.com/in/bogdan-iulian-vasile-03484312a/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Awesome license",
                        Url = new Uri("http://google.com")
                    }
                });
            });

            services.AddHealthChecks()
                .AddSqlServer(Configuration["Database:ConnectionString"])
                .AddUrlGroup(new Uri("https://google.com"), "Some endpoint");

            services.AddControllers();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/ING Challenge/swagger.json", "ING Coding Challenge");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
