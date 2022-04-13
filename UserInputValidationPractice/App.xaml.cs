using UserInputValidationPractice.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using Prism.Regions;

namespace UserInputValidationPractice
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            IRegionManager regionManager=Container.Resolve<IRegionManager>();
            //设置Region初始状态绑定的View
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(OrderView));

            //返回应用启动后的首页
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //注册所有需要导航的View
            containerRegistry.RegisterForNavigation<OrderView>();
            containerRegistry.RegisterForNavigation<CustomerView>();
            containerRegistry.RegisterForNavigation<JobView>();
        }
    }
}
