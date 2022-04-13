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
    public class DepotViewModel : BindableBase
    {
        private Depot _createdDepot;
        public Depot CreatedObject
        {
            get { return _createdDepot; }
            set { SetProperty(ref _createdDepot, value); }
        }

        public ICommand CreateCommand { get; set; }

        public DepotViewModel()
        {
            CreateCommand = new DelegateCommand(createSave, canCreateSave);
            CreatedObject = new Depot() { PickupDate = DateTime.Now, DeliverDate = DateTime.Now };
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
