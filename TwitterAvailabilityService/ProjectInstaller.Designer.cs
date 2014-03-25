namespace TwitterAvailabilityService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TwitterProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.TwitterInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // TwitterProcessInstaller
            // 
            this.TwitterProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.TwitterProcessInstaller.Password = null;
            this.TwitterProcessInstaller.Username = null;
            // 
            // TwitterInstaller
            // 
            this.TwitterInstaller.Description = "Retrieves Availability data from twitter";
            this.TwitterInstaller.DisplayName = "Twitter Service";
            this.TwitterInstaller.ServiceName = "Twitter Service";
            this.TwitterInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.TwitterProcessInstaller,
            this.TwitterInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller TwitterProcessInstaller;
        private System.ServiceProcess.ServiceInstaller TwitterInstaller;
    }
}