﻿using System;
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
    /// 使用IDataErrorInfo来验证
    /// </summary>
    public class OrderViewModel:BindableBase
    {
        private Order _createdOrder;
        public Order CreatedObject
        {
            get { return _createdOrder; }
            set { SetProperty(ref _createdOrder, value); }
        }

        public ICommand CreateCommand { get; set; }

        public OrderViewModel()
        {
            CreateCommand = new DelegateCommand(createOrderSave, canCreateOrderSave).ObservesProperty(() => CreatedObject.IsValid);
            CreatedObject = new Order() { PickupDate = DateTime.Now, DeliverDate = DateTime.Now };

        }

        private bool canCreateOrderSave()
        {
            bool canSave = CreatedObject.IsValid;
            return canSave;
        }

        private void createOrderSave()
        {
            MessageBox.Show($"Id={CreatedObject.Id}\n" +
                $"ProductName={CreatedObject.ProductName}\n" +
                $"PickupDate={CreatedObject.PickupDate}\n" +
                $"DeliverDate={CreatedObject.DeliverDate}\n" +
                $"Min_Temp={CreatedObject.Min_Temp}\n" +
                $"Max_Temp={CreatedObject.Max_Temp}\n");
        }
    }
}
