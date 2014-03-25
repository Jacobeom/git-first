using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Common.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quartz;
using Quartz.Impl;
using log4net.Config;


namespace TwitterAvailability.Test
{
    public class  MyJob : IJob
    {
        ILog log = LogManager.GetLogger(typeof(MyJob));

        public void Execute(IJobExecutionContext context)
        {
            log.Info("Line!!!");
        }
    }

    [TestClass]
    public class UnitTest2
    {
        ILog log = LogManager.GetLogger(typeof(UnitTest2));

        [TestMethod]
        public void TestMethod1()
        {
            XmlConfigurator.Configure();

            log.Info("------- Initializing ----------------------");
            // First we must get a reference to a scheduler
            ISchedulerFactory sf = new StdSchedulerFactory();
            IScheduler sched = sf.GetScheduler();
          
            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<MyJob>()
                .WithIdentity("MyJob")
                .Build();

            // Trigger the job to run on the next round minute
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("MyTrigger")
                .WithCronSchedule("0 0/5 * * * ?")
                .Build();

            // Tell quartz to schedule the job using our trigger
            sched.ScheduleJob(job, trigger);
            
            // Start up the scheduler (nothing can actually run until the 
            // scheduler has been started)
            sched.Start();
            sched.Shutdown();
            Thread.Sleep(300 * 1000);
        }
    }
}
