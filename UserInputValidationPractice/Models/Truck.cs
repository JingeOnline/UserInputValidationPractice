using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System.Diagnostics;
//using System.ComponentModel.DataAnnotations;

namespace UserInputValidationPractice.Models
{
    /// <summary>
    /// 项目中使用的Microsoft.Practices.EnterpriseLibrary.Validation太老了，已经过时了。
    /// 1. 初始化的时候就可以验证对象的所有属性
    /// 2. 先赋值再验证，即使验证失败，值也会被set到属性中
    /// 3. 如果是用户输入类型错误，也就是Exception类型的错误，ValidationResults中并不会接收。因此如果要控制CanExecute，还需要单独获取Exception类型的错误。
    /// </summary>
    public class Truck: BindableBase, IDataErrorInfo
    {
        private int _id;

        [NotNullValidator(MessageTemplate = "Id is required.")]
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _productName;

        [StringLengthValidator(3,5, MessageTemplate ="Product name length is only allowed between 3 - 5.")]
        public string ProductName
        {
            get { return _productName; }
            set { SetProperty(ref _productName, value); }
        }

        private DateTime _pickupDate;

        [PropertyComparisonValidator(nameof(DeliverDate),ComparisonOperator.LessThanEqual)]
        public DateTime PickupDate
        {
            get { return _pickupDate; }
            set { SetProperty(ref _pickupDate, value); }
        }

        private DateTime _deliverDate;

        [PropertyComparisonValidator(nameof(PickupDate), ComparisonOperator.GreaterThanEqual)]
        public DateTime DeliverDate
        {
            get { return _deliverDate; }
            set { SetProperty(ref _deliverDate, value); }
        }

        private double _min_Temp;

        [RangeValidator(0.0,RangeBoundaryType.Inclusive,100.0,RangeBoundaryType.Inclusive)]
        public double Min_Temp
        {
            get { return _min_Temp; }
            set { SetProperty(ref _min_Temp, value); }
        }

        private double _max_Temp;

        [RangeValidator(0.0, RangeBoundaryType.Inclusive, 100.0, RangeBoundaryType.Inclusive)]
        public double Max_Temp
        {
            get { return _max_Temp; }
            set { SetProperty(ref _max_Temp, value); }
        }


        
        public string Error
        {
            //把当前对象的所有错误信息字符串连在一起，返回。
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
        
        //传入属性名称，返回该属性的错误信息。任意控件的PropertyChanged事件都会触发该“所引器”。
        public string this[string propertyName]
        {
            get
            {
                //results对象储存着该对象所有属性的错误。
                ValidationResults results = Validation.ValidateFromAttributes<Truck>(this);
                Debug.WriteLine("results.Count="+results.Count);
                foreach (var result in results)
                {
                    if (result.Key == propertyName)
                    {
                        //返回的result.Message就是显示在UI上的错误提示信息。
                        return result.Message;
                    }
                }

                return string.Empty;
            }
        }

        //返回该对象是否所有字段都有效
        public bool IsValid
        {
            get { return Validation.ValidateFromAttributes<Truck>(this).IsValid; }
        }
    }
}
