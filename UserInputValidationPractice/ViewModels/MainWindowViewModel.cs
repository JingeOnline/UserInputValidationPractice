using Prism.Mvvm;
using System.Windows.Input;
using UserInputValidationPractice.Models;
using Prism.Commands;
using System;
using System.Windows;

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

        private Order _createdOrder;
        public Order CreatedOrder
        {
            get { return _createdOrder; }
            set { SetProperty(ref _createdOrder, value); }
        }
        public ICommand CreateOrderCommand { get; set; }

        private Customer _createdCustomer;
        public Customer CreatedCustomer
        {
            get { return _createdCustomer; }
            set { SetProperty(ref _createdCustomer, value); }
        }
        public ICommand CreateCustomerCommand { get; set; }

        public MainWindowViewModel()
        {
            CreateOrderCommand = new DelegateCommand(createOrderSave, canCreateOrderSave).ObservesProperty(()=>CreatedOrder.IsValid);
            CreateCustomerCommand = new DelegateCommand(createCustomerSave, canCreateCustomerSave);
            CreatedOrder = new Order() { PickupDate = DateTime.Now, DeliverDate = DateTime.Now };
            CreatedCustomer=new Customer() { PickupDate = DateTime.Now, DeliverDate = DateTime.Now };
        }

        private bool canCreateCustomerSave()
        {
            return true;
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

        private bool canCreateOrderSave()
        {
            bool canSave = CreatedOrder.IsValid;
            return canSave;
        }

        private void createOrderSave()
        {
            MessageBox.Show($"Id={CreatedOrder.Id}\n" +
                $"ProductName={CreatedOrder.ProductName}\n" + 
                $"PickupDate={CreatedOrder.PickupDate}\n" + 
                $"DeliverDate={CreatedOrder.DeliverDate}\n" +
                $"Min_Temp={CreatedOrder.Min_Temp}\n" +
                $"Max_Temp={CreatedOrder.Max_Temp}\n");
        }
    }
}
