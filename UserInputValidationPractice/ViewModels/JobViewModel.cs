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
    public class JobViewModel:BindableBase
    {
        private Job _createdJob;
        public Job CreatedJob
        {
            get { return _createdJob; }
            set { SetProperty(ref _createdJob, value); }
        }

        public ICommand CreateJobCommand { get; set; }

        public JobViewModel()
        {
            CreateJobCommand = new DelegateCommand(createJobSave, canCreateJobSave);
            CreatedJob = new Job() { PickupDate = DateTime.Now, DeliverDate = DateTime.Now };
        }

        private bool canCreateJobSave()
        {
            return true;
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
