using CompanyEmployees.Extensions;
using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using NLog;

namespace CompanyEmployees
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //step 4: Injecting logging services
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),
            "/nlog.config"));


            // Add services to the container.
            //step 2: call those extension method
            builder.Services.ConfigureCors();
            builder.Services.ConfigureIISIntegration();
            builder.Services.ConfigureLoggerService();
            builder.Services.ConfigureRepositoryManaager();
            builder.Services.ConfigureServiceManager();
            builder.Services.ConfigureSqlContext(builder.Configuration);
            builder.Services.AddControllers()
                .AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly);
            builder.Services.AddAutoMapper(typeof(Program));
            //step 8: Identity set up
            builder.Services.AddAuthentication();
            builder.Services.ConfigureIdentity();
            builder.Services.ConfigureJWT(builder.Configuration);

            //step 7: for patching
            NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter() =>
                new ServiceCollection().AddLogging().AddMvc().AddNewtonsoftJson()
                .Services.BuildServiceProvider()
                .GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters
                .OfType<NewtonsoftJsonPatchInputFormatter>().First();

            //step 6: Content negotiation
            builder.Services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
                config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
            }).AddXmlDataContractSerializerFormatters()
            .AddCustomCsvFormatter()
            .AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly);
            

            builder.Services.AddControllers();
            //step 7: custom responses from action
            builder.Services.Configure<ApiBehaviorOptions>(options => { 
                options.SuppressModelStateInvalidFilter= true;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            //step 5: adding global exception handler
            var logger = app.Services.GetRequiredService<ILoggerManager>();
            app.ConfigureExceptionHandler(logger);

            if (app.Environment.IsProduction())
            {
                app.UseHsts();
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //step 3: configure request pipeline
            /* if (app.Environment.IsDevelopment())
                 app.UseDeveloperExceptionPage();
             else
                 app.UseHsts();*/


            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });
            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}