using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BackEndTest.API.Models;
using BackEndTest.API.Mutations;
using BackEndTest.API.Queries;
using BackEndTest.API.Schema;
using BackEndTest.DataAccess.Repositories;
using BackEndTest.DataAccess.Repositories.Contracts;
using BackEndTest.Database;
using BackEndTest.Types.Movement;
using BackEndTest.Types.User;

namespace BackEndTest.API
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
            services.AddMvc();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IMovementRepository, MovementRepository>();

            services.AddDbContext<BackEndTestContext>(options => options.UseSqlite(Configuration["ConnectionStrings:backend-test"]));
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<QQuery>();
            services.AddSingleton<MMutation>();
            services.AddSingleton<UserType>();
            services.AddSingleton<UserInputType>();
            services.AddSingleton<MovementType>();
            services.AddSingleton<MovementInputType>();

            var sp = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(new BackEndTestSchema(new FuncDependencyResolver(type => sp.GetService(type))));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, BackEndTestContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGraphiQl();
            app.UseMvc();
        }
    }
}
