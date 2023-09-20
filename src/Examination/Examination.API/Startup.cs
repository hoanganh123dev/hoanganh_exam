using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examination.Application.Commands.StartExam;
using Examination.Application.Mapping;
using Examination.Domain.AggregateModels.ExamAggregate;
using Examination.Domain.AggregateModels.ExamResultAggregate;
using Examination.Domain.AggregateModels.UserAggregate;
using Examination.Infrastructure.Repositories;
using Examination.Infrastructure.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

namespace Examination.API
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
            services.AddApiVersioning(options=>{
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(
                           options =>
                           {
                               // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                               // note: the specified format code will format the version as "'v'major[.minor][-status]"
                               options.GroupNameFormat = "'v'VVV";

                               // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                               // can also be used to control the format of the API version in route templates
                               options.SubstituteApiVersionInUrl = true;
                           });
            services.AddSingleton<IMongoClient>(c =>
            {
                var user = Configuration.GetValue<string>("DatabaseSettings:User");
                var password = Configuration.GetValue<string>("DatabaseSettings:Password");
                var server = Configuration.GetValue<string>("DatabaseSettings:Server");
                var databaseName = Configuration.GetValue<string>("DatabaseSettings:DatabaseName");
                return new MongoClient(
                    "mongodb://" + user + ":" + password + "@" + server + "/" + databaseName + "?authSource=admin");
            });
            services.AddScoped(c => c.GetService<IMongoClient>()?.StartSession());
            services.AddAutoMapper(cfg => { cfg.AddProfile(new MappingProfile()); });
            services.AddMediatR(typeof(StartExamCommandHandler).Assembly);
            services.AddControllers();
            services.AddCors(options =>
            {
                //cho phép làm việc với all domain  
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Examination.API V1", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "Examination.API V2", Version = "v2" });
            });
            services.Configure<ExamSettings>(Configuration);

            services.AddTransient<IExamRepository, ExamRepository>();
            services.AddTransient<IExamResultRepository, ExamResultRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Examination.API v1");
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "Examination.API v2");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
