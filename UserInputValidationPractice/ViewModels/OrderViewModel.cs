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
    public class OrderViewModel:BindableBase
    {
        private Order _createdOrder;
        public Order CreatedOrder
        {
            get { return _createdOrder; }
            set { SetProperty(ref _createdOrder, value); }
        }

        public ICommand CreateOrderCommand { get; set; }

        public OrderViewModel()
        {
            CreateOrderCommand = new DelegateCommand(createOrderSave, canCreateOrderSave).ObservesProperty(() => CreatedOrder.IsValid);
            CreatedOrder = new Order() { PickupDate = DateTime.Now, DeliverDate = DateTime.Now };

        }

        private bool canCreateOrderSave()
        {
            bool canSave = CreatedOrder.IsValid;
            return canSave;
            //return true;
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
