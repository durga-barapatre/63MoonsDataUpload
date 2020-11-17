using DataUpload.App_Start;
using Serilog;

[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(AppPostShutDown), nameof(AppPostShutDown.PostApplicationShutDown))]
namespace DataUpload.App_Start
{
    /// <summary>
    /// This runs even before global.asax Application_Start (see WebActivatorConfig)
    /// </summary>    
    public class AppPostShutDown
    {
            private static ILogger Logger = Log.ForContext<AppPostShutDown>();

            public static void PostApplicationShutDown()
            {
                Logger.Debug("PostApplicationShutDown");

                // ... snip ...
                // very late things like IoC dispose ....
                // ... snip ...

                // force flushing the last "not logged" events
                Logger.Debug("Closing the logger! ");
                Log.CloseAndFlush();
            }
        }
    }
