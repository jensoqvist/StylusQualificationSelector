using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace StylusQualificationSelector
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void appStart(object sender, StartupEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(e.Args);
          
            mainWindow.Show();

        }
    }
}
