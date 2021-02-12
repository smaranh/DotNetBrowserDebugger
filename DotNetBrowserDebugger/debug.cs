using DotNetBrowser.Browser;
using DotNetBrowser.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotNetBrowserDebugger
{
    public partial class debug : Form
    {
        IEngine Engine;
        IBrowser browser;
        public debug()
        {
            Engine = createEngine(@"C:\DEV\testing\dotnetbrowser\DotNetBrowserDebugger\debugcache");
            Task.Run(() =>
                Engine.CreateBrowser()
            ).ContinueWith(t =>
            {
                browser = t.Result;
                browserView1.InitializeFrom(browser);
                browser.Navigation.LoadUrl("http://localhost:8000");
            }, TaskScheduler.FromCurrentSynchronizationContext());
            InitializeComponent();
        }

        public static IEngine createEngine(string userdatadir)
        {
            IEngine engine = EngineFactory.Create(new EngineOptions.Builder
            {
                RenderingMode = RenderingMode.HardwareAccelerated,
                ChromiumDirectory = @"C:\DEV\testing\dotnetbrowser\DotNetBrowserDebugger\debugbinaries",
                UserDataDirectory = userdatadir
            }
            .Build());
            return engine;
        }

        private void Debug_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            Engine?.Dispose();
            browser?.Dispose();
        }
    }
}
