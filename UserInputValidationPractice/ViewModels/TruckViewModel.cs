using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UserInputValidationPractice.Models;

namespace UserInputValidationPractice.ViewModels
{
    public class TruckViewModel : BindableBase
    {
        private Truck _createdObject;
        public Truck CreatedObject
        {
            get { return _createdObject; }
            set
            {
                //需要先判断非空，否则初始化的时候，对象为null，直接报错。
                if (_createdObject != null)
                {
                    _createdObject.PropertyChanged -= createObjectPropertyChanged;
                }
                SetProperty(ref _createdObject, value);
                if (_createdObject != null)
                {
                    _createdObject.PropertyChanged += createObjectPropertyChanged;
                }

            }
        }
        /// <summary>
        /// 该事件处理器负责在CreateObject对象属性变更的时候，手动检测CanExecute方法是否可以执行。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CreateCommand.RaiseCanExecuteChanged();
        }

        public DelegateCommand CreateCommand { get; set; }

        public TruckViewModel()
        {
            //下面代码不起作用，Prism只支持观察当前ViewModel中的属性，需要在CreatedObject属性中监听PropertyChanged事件
            //https://stackoverflow.com/questions/33313190/observesproperty-method-isnt-observing-models-properties-at-prism-6
            //CreateCommand = new DelegateCommand(createSave, canCreateSave).ObservesProperty(() => CreatedObject.Error);

            CreateCommand = new DelegateCommand(createSave, canCreateSave);
            CreatedObject = new Truck() { PickupDate = DateTime.Now, DeliverDate = DateTime.Now };
        }

        private bool canCreateSave()
        {
            return CreatedObject.Error == String.Empty;
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
