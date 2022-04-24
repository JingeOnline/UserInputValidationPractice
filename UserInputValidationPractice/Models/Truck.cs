using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
//using System.ComponentModel.DataAnnotations;

namespace UserInputValidationPractice.Models
{
    /// <summary>
    /// 项目中使用的Microsoft.Practices.EnterpriseLibrary.Validation太老了，已经过时了。
    /// 1. 初始化的时候就可以验证对象的所有属性
    /// 2. 先赋值再验证，即使验证失败，值也会被set到属性中
    /// </summary>
    public class Truck: BindableBase, IDataErrorInfo
    {
        private int _id;
        //[Required]
        [NotNullValidator(MessageTemplate = "Id is required.")]
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _productName;
        //[StringLength(5, MinimumLength = 3)]
        [StringLengthValidator(3,5, MessageTemplate ="Product name length is only allowed between 3 - 5.")]
        public string ProductName
        {
            get { return _productName; }
            set { SetProperty(ref _productName, value); }
        }

        private DateTime _pickupDate;
        //[Range(typeof(DateTime), "1/4/2022", "30/4/2022")]
        [PropertyComparisonValidator(nameof(DeliverDate),ComparisonOperator.LessThanEqual)]
        public DateTime PickupDate
        {
            get { return _pickupDate; }
            set { SetProperty(ref _pickupDate, value); }
        }

        private DateTime _deliverDate;
        //[Range(typeof(DateTime), "1/4/2022", "30/4/2022")]
        [PropertyComparisonValidator(nameof(PickupDate), ComparisonOperator.GreaterThanEqual)]
        public DateTime DeliverDate
        {
            get { return _deliverDate; }
            set { SetProperty(ref _deliverDate, value); }
        }

        private double _min_Temp;
        //[Range(0.0, 100.0)]
        [RangeValidator(0.0,RangeBoundaryType.Inclusive,100.0,RangeBoundaryType.Inclusive)]
        public double Min_Temp
        {
            get { return _min_Temp; }
            set { SetProperty(ref _min_Temp, value); }
        }

        private double _max_Temp;
        //[Range(0.0, 100.0)]
        [RangeValidator(0.0, RangeBoundaryType.Inclusive, 100.0, RangeBoundaryType.Inclusive)]
        public double Max_Temp
        {
            get { return _max_Temp; }
            set { SetProperty(ref _max_Temp, value); }
        }



        public string Error
        {
            get
            {
                StringBuilder error = new StringBuilder();

                ValidationResults results = Validation.ValidateFromAttributes<Truck>(this);
                foreach (var result in results)
                {
                    error.AppendLine(result.Message);
                }
                return error.ToString();
            }
        }

        public string this[string columnName]
        {
            get
            {
                ValidationResults results = Validation.ValidateFromAttributes<Truck>(this);
                foreach (var result in results)
                {
                    if (result.Key == columnName)
                    {
                        return result.Message;
                    }
                }

                return string.Empty;
            }
        }
    }
}
