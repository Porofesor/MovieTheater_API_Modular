using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using System.Runtime.CompilerServices;

namespace OpenTelemetry
{
    public static class ServiceCollectionExtensions
    {
        public static IHostApplicationBuilder ConfigureOpenTelemetryService(this IHostApplicationBuilder builder)
        {
            builder.Logging.AddOpenTelemetry(x =>
            {
                x.IncludeScopes = true;
                x.IncludeFormattedMessage = true;
            });

            builder.Services.AddOpenTelemetry()
                .WithMetrics(x =>
                {
                    x.AddAspNetCoreInstrumentation();
                    x.AddHttpClientInstrumentation();
                    //x.AddRuntimeInstrumentation() // idk what is going on here
                    //    .AddMeter("Microsoft.AspNetCore.Hosting",
                    //    "Microsoft.AspNetCore.Server.Kestrel",
                    //    "System.Net.Http");
                })
                .WithTracing(x =>
                {
                    if (builder.Environment.IsDevelopment())
                    {
                        x.SetSampler<AlwaysOnSampler>();
                    }
                    x.AddAspNetCoreInstrumentation()
                        //.AddGrpcClientInstrumentation() dosnt work/ no package
                        .AddHttpClientInstrumentation();
                        //.AddEntityFrameworkCoreInstrumentation();
                });

            // Telemetry Exporter
            builder.AddOpentTelemetryExporters();

            return builder;
        }

        private static IHostApplicationBuilder AddOpentTelemetryExporters(this IHostApplicationBuilder builders)
        {
            var useOtlpExporter = !string.IsNullOrEmpty(builders.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);

            if (useOtlpExporter)
            {
                builders.Services.Configure<OpenTelemetryLoggerOptions>(logging => logging.AddOtlpExporter());
                builders.Services.ConfigureOpenTelemetryLoggerProvider(metrics => metrics.AddOtlpExporter());
                builders.Services.ConfigureOpenTelemetryTracerProvider(tracing => tracing.AddOtlpExporter());
            }

            // I could have add it earlier above
            //builders.Services.AddOpenTelemetry().WithMetrics(x => x.AddPrometheusExporter()); //isnt supported by .net 7.0, only works on 8.0 & 9.0

            return builders;
        }
    }
}   