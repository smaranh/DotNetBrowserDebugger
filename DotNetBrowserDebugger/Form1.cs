using System.Threading.Tasks;
using System.Windows.Forms;
using DotNetBrowser.Engine;
using DotNetBrowser.Browser;
using DotNetBrowserDebugger;

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
                //browser.Navigation.LoadUrl("http://localhost:9222");
                //browser.Navigation.LoadUrl("https://harihars.esri.com:4000");
                browser.Navigation.LoadUrl("https://gmail.com");
                //browser.Navigation.LoadUrl("https://hydra.esri.com/portal");
            }, TaskScheduler.FromCurrentSynchronizationContext());
            InitializeComponent();
        }

        public static IEngine createEngine(string userdatadir)
        {
            IEngine engine = EngineFactory.Create(new EngineOptions.Builder
            {
                RenderingMode = RenderingMode.HardwareAccelerated,
                ChromiumDirectory = @"C:\DEV\testing\dotnetbrowser\DotNetBrowserDebugger\binaries",
                RemoteDebuggingPort = 8000,
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

        private void button1_Click(object sender, System.EventArgs e)
        {
            Form formD = new debug();
            formD.Show();
        }
    }
}
