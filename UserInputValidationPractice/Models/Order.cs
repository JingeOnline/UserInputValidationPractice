using System;
using System.Collections.Generic;
using System.ComponentModel;
using Prism.Mvvm;

namespace UserInputValidationPractice.Models
{
    /// <summary>
    /// 使用IDataErrorInfo，优点是简单易用。
    /// 缺点是无论输入值是否合法，都会执行属性的set方法，然后再去String this[string PropertyName]查询，返回验证结果。
    /// </summary>
    public class Order : BindableBase, IDataErrorInfo
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

        private double _max_Temp;
        public double Max_Temp
        {
            get { return _max_Temp; }
            set { SetProperty(ref _max_Temp, value); }
        }

        public string Error
        {
            get { return null; }
        }

        private bool _isValid;
        /// <summary>
        /// 该属性是用来判断整个对象有没有非法的属性，只有所有属性都通过验证，才返回true。
        /// 该属性是用来给CanSaveCommand用的。
        /// </summary>
        public bool IsValid
        {
            get { return _isValid; }
            set { SetProperty(ref _isValid, value); }
        }

        #region 设置该方法是因为，当初始化载入对象的时候，会检查对象的所有属性。即使前面的属性有错误，只要后面的属性没错误，最终会返回IsValid。
        //用来储存对象中每个属性的验证状态，合法为true，非法为false，key为属性名称。
        private Dictionary<string, bool> PropertyValidDictionary = new Dictionary<string, bool>();

        //查看一个对象的所有属性是否全部合法，如果存在任意一个非法值，返回false。
        private void varifyObject(string propertyName, bool noError)
        {
            //存在则更新，不存在则创建
            PropertyValidDictionary[propertyName] = noError;

            foreach (bool value in PropertyValidDictionary.Values)
            {
                if (!value)
                {
                    IsValid = false;
                    return;
                }
            }
            IsValid = true;
            return;
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
                            if (Min_Temp < 1 || Min_Temp > 100)
                                errorMessage = "Min_Temp must between 1 - 100";
                            if (Min_Temp > Max_Temp)
                                errorMessage = "Min_Temp cannot bigger than Max_Temp.";
                            break;
                        }
                    case nameof(Max_Temp):
                        {
                            if (Max_Temp < 1 || Max_Temp > 100)
                                errorMessage = "Max_Temp must between 1 - 100";
                            if (Min_Temp > Max_Temp)
                                errorMessage = "Min_Temp cannot bigger than Max_Temp.";
                            break;
                        }
                }
                varifyObject(propertyName, errorMessage == string.Empty);
                return errorMessage;
            }
        }
    }
}
