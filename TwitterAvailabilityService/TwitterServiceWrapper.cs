using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using Quartz;
using TwitterAvailability.Service;

namespace TwitterAvailabilityService
{
    [DisallowConcurrentExecution]
    public class TwitterServiceWrapper : IJob
    {
        ILog log = LogManager.GetLogger(typeof(TwitterServiceWrapper));

       
        public void Execute(IJobExecutionContext context)
        {
            log.Info("Running Twitter service");
          
            try
            {
                TwitterService twitterService = new TwitterService();
                twitterService.Run();
            }
            catch (Exception ex)
            {
                log.Error("Fatall error Running TwitterService", ex);
            }
            
        }
    }
}
