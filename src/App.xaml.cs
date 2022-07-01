using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Easy_Real_ESRGAN
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnActivated(EventArgs e)
        {
            Wpf.Ui.Appearance.Watcher.Watch(this.MainWindow, Wpf.Ui.Appearance.BackgroundType.Mica, true,true);
        }
    }
}
