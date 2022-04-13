using Prism.Mvvm;
using System.Windows.Input;
using UserInputValidationPractice.Models;
using Prism.Commands;
using System;
using System.Windows;
using Prism.Regions;

namespace UserInputValidationPractice.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ICommand NavToOrderCommand { get; set; }
        public ICommand NavToCustomerCommand { get; set; }
        public ICommand NavToJobCommand { get; set; }
        public ICommand NavToDepotCommand { get; set; }

        private IRegionManager _regionManager;

        public MainWindowViewModel(IRegionManager regionManager)
        {
            NavToCustomerCommand = new DelegateCommand(navToCustomer);
            NavToOrderCommand = new DelegateCommand(navToOrder);
            NavToJobCommand = new DelegateCommand(navToJob);
            NavToDepotCommand = new DelegateCommand(navToDepot);
            _regionManager = regionManager;
        }

        private void navToDepot()
        {
            _regionManager.RequestNavigate("ContentRegion", "DepotView");
        }

        private void navToJob()
        {
            //将目标Region导航到目标View
            //参数都为字符串，第一个参数是Region名称，第二个参数是View的URI（在App.xmal.cs中设置View的URI）
            _regionManager.RequestNavigate("ContentRegion","JobView");
        }

        private void navToOrder()
        {
            _regionManager.RequestNavigate("ContentRegion", "OrderView");
        }

        private void navToCustomer()
        {
            _regionManager.RequestNavigate("ContentRegion", "CustomerView");
        }
    }
}
