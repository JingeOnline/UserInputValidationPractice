using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UserInputValidationPractice.Models;

namespace UserInputValidationPractice.ViewModels
{
    public class InvoiceViewModel: BindableBase
    {
        private Invoice _createdObject;
        public Invoice CreatedObject
        {
            get { return _createdObject; }
            set { SetProperty(ref _createdObject, value); }
        }

        public DelegateCommand CreateCommand { get; set; }

        public InvoiceViewModel()
        {
            CreateCommand = new DelegateCommand(createSave, canCreateSave);
            CreatedObject = new Invoice() { PickupDate = DateTime.Now, DeliverDate = DateTime.Now };
        }

        private bool canCreateSave()
        {
            return true;
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
    }

}
