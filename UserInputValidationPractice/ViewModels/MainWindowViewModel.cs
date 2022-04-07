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
        public ICommand CreateSaveCommand { get; set; }
        public MainWindowViewModel()
        {
            CreateSaveCommand = new DelegateCommand(createSave, canCreateSave).ObservesProperty(()=>CreatedOrder.IsValid);
            CreatedOrder = new Order() { PickupDate = DateTime.Now, DeliverDate = DateTime.Now };
        }

        private bool canCreateSave()
        {
            bool canSave = CreatedOrder.IsValid;
            return canSave;
        }

        private void createSave()
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
