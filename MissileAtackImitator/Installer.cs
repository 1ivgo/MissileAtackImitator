namespace MissileAtackImitatorNS
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;

    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        public Installer()
        {
            InitializeComponent();
        }

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);

            string userConfigDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            userConfigDir = Path.Combine(userConfigDir, "MissileAtackImitator");

            if (Directory.Exists(userConfigDir))
            {
                Directory.Delete(userConfigDir, true);
            }

            userConfigDir += "NS";

            if (Directory.Exists(userConfigDir))
            {
                Directory.Delete(userConfigDir, true);
            }
        }
    }
}
