using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInputValidationPractice.Models
{
    /// <summary>
    /// 使用DataAnnotations和IDataErrorInfo验证
    /// 常用验证规则:Required,RegularExpression,StringLength,Range,Phone,Email,Url,CreditCard
    /// 先Set属性，再执行验证。
    /// </summary>
    public class Job : BindableBase, IDataErrorInfo
    {
        private int _id;
        [Required]
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        private string _productName;
        [StringLength(5, MinimumLength = 3)]
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
        [Range(0.0, 100.0)]
        public double Min_Temp
        {
            get { return _min_Temp; }
            set { SetProperty(ref _min_Temp, value); }
        }
        private double _max_Temp;
        [Range(0.0, 100.0)]
        public double Max_Temp
        {
            get { return _max_Temp; }
            set { SetProperty(ref _max_Temp, value); }
        }

        public string Error => null;

        public string this[string propertyName]
        {
            get
            {
                object value = GetType().GetProperty(propertyName).GetValue(this);

                ValidationContext validationContext = new ValidationContext(this)
                {
                    MemberName = propertyName
                };

                List<ValidationResult> validationResults = new List<ValidationResult>();

                bool propertyIsValid = Validator.TryValidateProperty(value, validationContext, validationResults);

                if (propertyIsValid)
                {
                    return null;
                }
                else
                {
                    return validationResults.First().ErrorMessage;
                }

            }
        }
    }
}
