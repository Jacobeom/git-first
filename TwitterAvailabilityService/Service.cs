using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Common.Logging;
using Quartz;
using Quartz.Impl;
using log4net.Config;

namespace TwitterAvailabilityService
{
    public partial class Service : ServiceBase
    {

        public string CronExpression
        {
            get { return ConfigurationManager.AppSettings["CronExpression"]; }
        }


        ILog log = LogManager.GetLogger(typeof(Service));
        private ISchedulerFactory schedulerFactory = null;
        private IScheduler scheduler = null;

        public Service()
        {
            InitializeComponent();

            try
            {
                XmlConfigurator.Configure();

                log.Info("--------------------- Initializing Service ----------------------");
                
                schedulerFactory = new StdSchedulerFactory();
                scheduler = schedulerFactory.GetScheduler();


                IJobDetail job = JobBuilder.Create<TwitterServiceWrapper>()
                    .WithIdentity("TwitterServiceJobr")
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("TwitterTrigger")
                    .WithCronSchedule(CronExpression)
                    .Build();

                scheduler.ScheduleJob(job, trigger);
                
            }
            catch (Exception ex)
            {
                log.Error("Fatall error Initializing Windows Service", ex);   
                if (scheduler != null && scheduler.IsStarted)
                    scheduler.Shutdown();

            }
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                log.Info("--------------------- Starting Service ----------------------");
                scheduler.Start();
            }
             catch (Exception ex)
             {
                 log.Error("Fatall Error Stating Windows Service", ex);
                 if (scheduler != null && scheduler.IsStarted)
                     scheduler.Shutdown();

             }
        }

        protected override void OnStop()
        {
            try
            {
                log.Info("--------------------- Stopping Service ----------------------");
                scheduler.Start();
            }
            catch (Exception ex)
            {
                log.Error("Fatall Error Stopping Windows Service", ex);
                if (scheduler != null && scheduler.IsStarted)
                    scheduler.Shutdown();

            }
        }
    }
}
