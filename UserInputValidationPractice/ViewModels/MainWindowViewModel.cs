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

        private Customer _createdCustomer;
        public Customer CreatedCustomer
        {
            get { return _createdCustomer; }
            set { SetProperty(ref _createdCustomer, value); }
        }

        private Job _createdJob;
        public Job CreatedJob
        {
            get { return _createdJob; }
            set { SetProperty(ref _createdJob, value); }
        }
        public ICommand CreateOrderCommand { get; set; }
        public ICommand CreateCustomerCommand { get; set; }
        public ICommand CreateJobCommand { get; set; }


        public MainWindowViewModel()
        {
            CreateOrderCommand = new DelegateCommand(createOrderSave, canCreateOrderSave).ObservesProperty(()=>CreatedOrder.IsValid);
            CreateCustomerCommand = new DelegateCommand(createCustomerSave, canCreateCustomerSave).ObservesProperty(()=>CreatedCustomer.HasErrors);
            CreateJobCommand = new DelegateCommand(createJobSave, canCreateJobSave);
            CreatedOrder = new Order() { PickupDate = DateTime.Now, DeliverDate = DateTime.Now };
            CreatedCustomer=new Customer() { PickupDate = DateTime.Now, DeliverDate = DateTime.Now };
            CreatedJob=new Job() { PickupDate = DateTime.Now, DeliverDate = DateTime.Now };
        }

        private bool canCreateJobSave()
        {
            return true;
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

        private void createJobSave()
        {
            MessageBox.Show($"Id={CreatedJob.Id}\n" +
                $"ProductName={CreatedJob.ProductName}\n" +
                $"PickupDate={CreatedJob.PickupDate}\n" +
                $"DeliverDate={CreatedJob.DeliverDate}\n" +
                $"Min_Temp={CreatedJob.Min_Temp}\n" +
                $"Max_Temp={CreatedJob.Max_Temp}\n");
        }
    }
}
