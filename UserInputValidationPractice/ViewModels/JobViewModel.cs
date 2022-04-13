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
    /// 使用IDataErrorInfo + Data Annotations来验证
    /// </summary>
    public class JobViewModel:BindableBase
    {
        private Job _createdJob;
        public Job CreatedObject
        {
            get { return _createdJob; }
            set { SetProperty(ref _createdJob, value); }
        }

        public ICommand CreateCommand { get; set; }

        public JobViewModel()
        {
            CreateCommand = new DelegateCommand(createJobSave, canCreateJobSave);
            CreatedObject = new Job() { PickupDate = DateTime.Now, DeliverDate = DateTime.Now };
        }

        private bool canCreateJobSave()
        {
            return true;
        }

        private void createJobSave()
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
