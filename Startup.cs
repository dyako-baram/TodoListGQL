using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoListGQL.Data;
using TodoListGQL.GraphQl;

namespace TodoListGQL
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration=configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<ApiDbContext>(o=>o.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddGraphQLServer()
                    .AddQueryType<Query>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
            app.UseGraphQLVoyager(new VoyagerOptions(){
                GraphQLEndPoint="/graphql"
            },"/graphql-ui");
        }
    }
}
