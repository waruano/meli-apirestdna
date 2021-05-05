using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Meli.ApiRestDNA.Application.Services;
using Meli.ApiRestDNA.Domain;
using Meli.ApiRestDNA.Infrastructure.Mongo.Repositories;
using Meli.ApiRestDNA.Model;
using Meli.ApiRestDNA.Shared.Extensions;
using Meli.ApiRestDNA.Shared.Infrastructure.Mongo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IO;
using System.Reflection;
using Mapster;
using Meli.ApiRestDNA.Infrastructure.Mongo.Finders;

namespace Meli.ApiRestDNA
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Destructure.ToMaximumCollectionCount(10)
                .Destructure.ToMaximumStringLength(1024)
                .Destructure.ToMaximumDepth(5)
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDbSettings"));

            services.AddSingleton<IMongoDbSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);
            services.AddControllers();
            services.AddMvc()
            .AddFluentValidation();
            services.AddValidatorsFromAssemblyContaining(typeof(Startup));
            services.AddTransient<IValidator<DnaRequest>, DnaRequestValidator>();
            services.AddSingleton(typeof(IMongoDbContext<>), typeof(MongoDbContext<>));
            services.AddSingleton<IHumanRepository, HumanRepository>();
            services.AddSingleton<IReportFinder, ReportFinder>();
            services.AddSingleton<IReportRepository, ReportRepository>();
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddScoped<IDnaValidator, DnaValidatorByChar>();
            services.AddScoped<ExceptionMiddleware>();
            services.AddHealthChecks();
            services.AddApiVersioning(
                options =>
                {
                    options.ReportApiVersions = true;
                });
            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(
                options =>
                {
                    options.OperationFilter<SwaggerDefaultValues>();
                    options.IncludeXmlComments(XmlCommentsFilePath);
                });
            //TypeAdapterConfig<HumanReport, ReportResponse>.NewConfig()
            //    .Map(dst => dst.Ratio, src => src.Ratio);
        }

        public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
        {
            readonly IApiVersionDescriptionProvider _provider;

            public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) =>
                _provider = provider;

            public void Configure(SwaggerGenOptions options)
            {
                foreach (var description in _provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(
                        description.GroupName,
                        new OpenApiInfo()
                        {
                            Title = $"Sample API {description.ApiVersion}",
                            Version = description.ApiVersion.ToString(),
                        });
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseExceptionHandlerMiddleware();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSwagger();
            app.UseSerilogRequestLogging();
            app.UseSwaggerUI(
                options =>
                {
                    // build a swagger endpoint for each discovered API version
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/");
                endpoints.MapControllers();
            });
            //app.UseAuthorization();
        }

        private static string XmlCommentsFilePath
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }
    }
}
