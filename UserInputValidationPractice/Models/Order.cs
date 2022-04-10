using System;
using System.Collections.Generic;
using System.ComponentModel;
using Prism.Mvvm;

namespace UserInputValidationPractice.Models
{
    
    public class Order:BindableBase, IDataErrorInfo
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _productName;
        public  string ProductName
        {
            get { return _productName; }
            set { SetProperty(ref _productName, value); }
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
            set { SetProperty(ref _min_Temp, value); }
        }

        private double  _max_Temp;
        public double Max_Temp
        {
            get { return _max_Temp; }
            set { SetProperty(ref _max_Temp, value); }
        }

        public string Error
        {
            get
            {
                return null;
            }
        }

        private bool _isValid;
        public bool IsValid
        {
            get { return _isValid; }
            set { SetProperty(ref _isValid, value); }
        }

        #region 设置该方法是因为，当初始化载入对象的时候，会检查对象的所有属性。即使前面的属性有错误，只要后面的属性没错误，最终会返回IsValid。
        //用来储存对象中每个属性的验证状态，合法为true，非法为false，key为属性名称。
        private Dictionary<string, bool> PropertyValidDictionary = new Dictionary<string, bool>();

        //查看一个对象的所有属性中是否存在非法值，如果存在任意一个非法值，返回false。
        private bool varifyEachProperty(string key, bool value)
        {
            //存在则更新，不存在则创建
            PropertyValidDictionary[key] = value;

            foreach (bool val in PropertyValidDictionary.Values)
            {
                if (!val)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        //该方法每次为对象的任意属性赋值的时候，都会执行一次
        public string this[string propertyName]
        {
            get
            {
                string errorMessage = string.Empty;
                switch (propertyName)
                {
                    case nameof(ProductName):
                        {
                            if (string.IsNullOrEmpty(ProductName))
                                errorMessage = "Product Name cannot be null.";
                            break;
                        }
                    case nameof(PickupDate):
                        {
                            if (PickupDate > DeliverDate)
                                errorMessage = "PickupDate cannot later than DeliverDate.";
                            break;
                        }
                    case nameof(DeliverDate):
                        {
                            if (PickupDate > DeliverDate)
                                errorMessage = "DeliverDate cannot earlier than PickupDate.";
                            break;
                        }
                    case nameof(Min_Temp):
                        {
                            if (Min_Temp > Max_Temp)
                                errorMessage = "Min_Temp cannot bigger than Max_Temp.";
                            break;
                        }
                    case nameof(Max_Temp):
                        {
                            if (Min_Temp > Max_Temp)
                                errorMessage = "Min_Temp cannot bigger than Max_Temp.";
                            break;
                        }
                }
                IsValid=varifyEachProperty(propertyName, errorMessage == string.Empty);
                return errorMessage;
            }
        }
    }
}
