using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInputValidationPractice.Models
{
    /// <summary>
    /// 使用抛出Exception的方式进行验证
    /// </summary>
    public class Invoice : BindableBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }
        private string _productName;
        public string ProductName
        {
            get { return _productName; }
            set
            {
                if (value.Length < 3 || value.Length > 10)
                {
                    //抛出的异常可以是任意类型，但是推荐使用ArgumentException，异常中的Message会被显示到界面中。
                    throw new ArgumentException("ProductName must between 3-10 characters.");
                }
                SetProperty(ref _productName, value);
            }
        }

        private DateTime _pickupDate;
        public DateTime PickupDate
        {
            get { return _pickupDate; }
            set { SetProperty(ref _pickupDate, value); }
        }

        private DateTime _deliverDate;
        public DateTime DeliverDate
        {
            get { return _deliverDate; }
            set { SetProperty(ref _deliverDate, value); }
        }
        private double _min_Temp;
        public double Min_Temp
        {
            get { return _min_Temp; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Min_Temp cannot be negative.");
                }
                SetProperty(ref _min_Temp, value);
            }
        }
        private double _max_Temp;
        public double Max_Temp
        {
            get { return _max_Temp; }
            set { SetProperty(ref _max_Temp, value); }
        }
    }
}
