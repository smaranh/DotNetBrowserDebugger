using System.Threading.Tasks;
using System.Windows.Forms;
using DotNetBrowser.Engine;
using DotNetBrowser.Browser;

namespace ESRI.ArcGIS.Mapping.OfficeIntegration.Core
{
    public partial class Form1 : Form
    {
        IEngine Engine;
        IBrowser browser;
        public Form1()
        {
            Engine = createEngine(@"C:\DEV\testing\dotnetbrowser\DotNetBrowserDebugger\cache");
            Task.Run(() =>
                Engine.CreateBrowser()
            ).ContinueWith(t =>
            {
                browser = t.Result;
                browserView1.InitializeFrom(browser);
                browser.Navigation.LoadUrl("http://localhost:4500");
            }, TaskScheduler.FromCurrentSynchronizationContext());
            InitializeComponent();
        }

        public static IEngine createEngine(string userdatadir)
        {
            IEngine engine = EngineFactory.Create(new EngineOptions.Builder
            {
                RenderingMode = RenderingMode.HardwareAccelerated,
                ChromiumDirectory = @"C:\DEV\testing\dotnetbrowser\DotNetBrowserDebugger\binaries",
                UserDataDirectory = userdatadir
            }
            .Build());
            return engine;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Engine?.Dispose();
            browser?.Dispose();
        }
    }
}
