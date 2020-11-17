using Serilog;
using Serilog.Events;
using SerilogWeb.Classic;
using SerilogWeb.Classic.Enrichers;
using SerilogWeb.Classic.WebApi.Enrichers;


namespace DataUpload.App_Start
{
    public class LogConfig
    {
        static public void Configure()
        {
            ApplicationLifecycleModule.LogPostedFormData = LogPostedFormDataOption.OnlyOnError;
            ApplicationLifecycleModule.FormDataLoggingLevel = LogEventLevel.Debug;
            ApplicationLifecycleModule.RequestLoggingLevel = LogEventLevel.Debug;

            var loggerConfiguration = new LoggerConfiguration().ReadFrom.AppSettings()
                    .Enrich.FromLogContext()
                    .Enrich.With<HttpRequestIdEnricher>()
                    .Enrich.With<UserNameEnricher>()
                    .Enrich.With<HttpRequestUrlEnricher>()
                    .Enrich.With<WebApiRouteTemplateEnricher>()
                    .Enrich.With<WebApiControllerNameEnricher>()
                    .Enrich.With<WebApiRouteDataEnricher>()
                    .Enrich.With<WebApiActionNameEnricher>()
                ;

            Log.Logger = loggerConfiguration.CreateLogger();
        }
    }
}