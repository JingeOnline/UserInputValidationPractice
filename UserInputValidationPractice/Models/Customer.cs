using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInputValidationPractice.Models
{
    /// <summary>
    /// 该Model类直接继承ValidationModelBase类,INotifyDataErrorInfo接口
    /// 现在的问题是，当创建新对象的时候，由于没有调用属性的Set方法，新对象的属性无法得到验证。只有当用户赋值的时候，才能验证。
    /// </summary>
    public class Customer: ValidationModelBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _productName;
        public string ProductName
        {
            get { return _productName; }
            set 
            { 
                SetProperty(ref _productName, value);
                //这里添加验证逻辑，可以把验证逻辑单独提出来，创建一个ValidateProductName()方法
                if (string.IsNullOrEmpty(ProductName))
                {
                    AddError("ProductName is required.");
                }
                if (ProductName?.Length < 3)
                {
                    AddError("ProductName must be at least 3 characters long.");
                }
                else
                {
                    ClearErrors();
                }
            }
        }

        private DateTime _pickupDate;
        public DateTime PickupDate
        {
            get { return _pickupDate; }
            set 
            { 
                SetProperty(ref _pickupDate, value);
                if (PickupDate > DeliverDate)
                {
                    AddError("PickupDate cannot be later than DeliverDate");
                }
                else
                {
                    //同时清除两个属性的错误，保证用户修改一个时间，两个时间控件的错误都被清除
                    ClearErrors();
                    ClearErrors(nameof(DeliverDate));
                }
            }
        }

        private DateTime _deliverDate;
        public DateTime DeliverDate
        {
            get { return _deliverDate; }
            set 
            { 
                SetProperty(ref _deliverDate, value);
                if (PickupDate > DeliverDate)
                {
                    AddError("PickupDate cannot be later than DeliverDate");
                }
                else
                {
                    //同时清除两个属性的错误，保证用户修改一个时间，两个时间控件的错误都被清除
                    ClearErrors();
                    ClearErrors(nameof(PickupDate));
                }
            }
        }

        private double _min_Temp;
        public double Min_Temp
        {
            get { return _min_Temp; }
            set { SetProperty(ref _min_Temp, value); }
        }

        private double _max_Temp;

        public double Max_Temp
        {
            get { return _max_Temp; }
            set { SetProperty(ref _max_Temp, value); }
        }


    }
}
