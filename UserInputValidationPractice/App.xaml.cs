using UserInputValidationPractice.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

namespace UserInputValidationPractice
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
