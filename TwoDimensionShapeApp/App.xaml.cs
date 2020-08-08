using System.Windows;

namespace TwoDimensionShapeApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Bootstrapper trap = new Bootstrapper();
            trap.Run();
        }
    }
}
