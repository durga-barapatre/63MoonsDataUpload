using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataUpload.App_Start;
using Serilog;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(AppPreStart), nameof(AppPreStart.PreApplicationStart))]
namespace DataUpload.App_Start
{
    public class AppPreStart
    {
            public static void PreApplicationStart()
            {
                LogConfig.Configure();
                var logger = Log.ForContext<AppPreStart>();
                logger.Information("App is starting ...");

                // ... snip ...
                // very early things like IoC config, AutoMapper config ...
                // ... snip ...

                logger.Debug("Done with AppPreStart");
            }
        }
    }
