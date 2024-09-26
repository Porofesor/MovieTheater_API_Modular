using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
namespace Swagger.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        #region Comments for future dev on how to make it work
        // Go to Main Project properties -> build -> errors -> suppers error/warming 1591
        // Dont forget to add a path(properties of individual module) with file name inside "XML documentation file path" /bin/debuf/net6.0/Name_Of_Module.xml  its important
        // You need to add a path inside every module + list it below with: c.IncludeXmlComments ...

        //https://blog.georgekosmidis.net/swagger-in-asp-net-core-tips-and-tricks.html
        // Official version of comment below

        // To suppress error/warning 1591 (missing XML comments), go to the main project's properties 
        // and enable "XML Documentation File" under Build -> Output.
        // Important: For each module, go to the module's project properties and specify the XML documentation file path
        // (e.g., /bin/debug/net6.0/ModuleName.xml).
        // Ensure that you include the XML comments from each module in Swagger by listing them with c.IncludeXmlComments().
        #endregion 
        public static IServiceCollection AddSwaggerCore(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API",
                    Version = "v1",
                    Description = "API for test purposes",
                    TermsOfService = new Uri("http://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "___________",
                        Email = "___________",
                        Url = new Uri("https://example.com")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Employee API LIC",
                        Url = new Uri("https://example.com/license")
                    }
                });
                // Include the XML comments for the current assembly.
                //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);

                // Include XML comments from the Movies module
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Modules.Movies.xml"));

                // Uncomment and add other modules as needed
                // c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Modules.Tickets.xml"));
                // c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "ModuleB.xml"));

                //c.IncludeXmlComments(xmlPath);  // Include current module's XML
            });

            return services;
        }
    }
}
