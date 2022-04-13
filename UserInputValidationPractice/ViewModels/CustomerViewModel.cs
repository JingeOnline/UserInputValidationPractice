using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using UserInputValidationPractice.Models;

namespace UserInputValidationPractice.ViewModels
{
    /// <summary>
    /// 使用INotifyDataErrorInfo来验证
    /// </summary>
    public class CustomerViewModel:BindableBase
    {

        private Customer _createdCustomer;
        public Customer CreatedCustomer
        {
            get { return _createdCustomer; }
            set { SetProperty(ref _createdCustomer, value); }
        }
        public ICommand CreateCustomerCommand { get; set; }

        public CustomerViewModel()
        {
            CreateCustomerCommand = new DelegateCommand(createCustomerSave, canCreateCustomerSave).ObservesProperty(() => CreatedCustomer.HasErrors);
            CreatedCustomer = new Customer() { PickupDate = DateTime.Now, DeliverDate = DateTime.Now };
        }

        private bool canCreateCustomerSave()
        {
            return !CreatedCustomer.HasErrors;
        }

        private void createCustomerSave()
        {
            MessageBox.Show($"Id={CreatedCustomer.Id}\n" +
                $"ProductName={CreatedCustomer.ProductName}\n" +
                $"PickupDate={CreatedCustomer.PickupDate}\n" +
                $"DeliverDate={CreatedCustomer.DeliverDate}\n" +
                $"Min_Temp={CreatedCustomer.Min_Temp}\n" +
                $"Max_Temp={CreatedCustomer.Max_Temp}\n");
        }
    }
}
