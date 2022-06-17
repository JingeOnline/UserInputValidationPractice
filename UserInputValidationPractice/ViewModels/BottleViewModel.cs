using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UserInputValidationPractice.Models;

namespace UserInputValidationPractice.ViewModels
{
    public class BottleViewModel:BindableBase
    {
        private Bottle _createdObject;
        public Bottle CreatedObject
        {
            get { return _createdObject; }
            set { SetProperty(ref _createdObject, value); }
        }
        public ICommand CreateCommand { get; set; }

        public BottleViewModel()
        {
            CreateCommand = new DelegateCommand(createSave, canCreateSave);
            CreatedObject = new Bottle() { PickupDate = DateTime.Now, DeliverDate = DateTime.Now };
        }
        private void createSave()
        {
            MessageBox.Show($"Id={CreatedObject.Id}\n" +
                $"ProductName={CreatedObject.ProductName}\n" +
                $"PickupDate={CreatedObject.PickupDate}\n" +
                $"DeliverDate={CreatedObject.DeliverDate}\n" +
                $"Min_Temp={CreatedObject.Min_Temp}\n" +
                $"Max_Temp={CreatedObject.Max_Temp}\n");
        }
        private bool canCreateSave()
        {
            return true;
        }
    }
}
