using System;
using Prism.Unity;
using System.Windows;
using Microsoft.Practices.Unity;
using DataAccessLayer;

namespace TwoDimensionShapeApp
{
    public class Bootstrapper : UnityBootstrapper
    {
        public override void Run(bool runWithDefaultConfiguration)
        {
            base.Run(runWithDefaultConfiguration);
        }
        //Create the Shell
        // Show the Shell
        protected override DependencyObject CreateShell()
        {
            Container.RegisterType<IRepository<IEntityBase>, Repository<IEntityBase>>(new ContainerControlledLifetimeManager());
            return Container.Resolve<Shell>();
        }
        protected override void InitializeShell()
        {
            //Set Main Window for Prism
            App.Current.MainWindow = (Window)Shell;
            App.Current.MainWindow.Show();
        }
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            Type sampleType = typeof(RegisterRegion);
            ModuleCatalog.AddModule(new Prism.Modularity.ModuleInfo { ModuleName = sampleType.Name, ModuleType = sampleType.AssemblyQualifiedName });
        }
    }
}
