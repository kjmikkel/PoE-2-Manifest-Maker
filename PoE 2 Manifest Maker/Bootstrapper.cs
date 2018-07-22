using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.UnityExtensions;

namespace PoE_2_Manifest_Maker
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override System.Windows.DependencyObject CreateShell()
        {
            return this.Container.Resolve<MainWindow>();
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();
            App.Current.MainWindow = (MainWindow)this.Shell;
            App.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            this.ModuleCatalog.AddModule(null); // Placeholder
        }
    }
}
