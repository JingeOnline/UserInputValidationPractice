using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInputValidationPractice.Models
{
    /// <summary>
    /// 使用DataAnnotations验证,继承DataAnnotationBase类。
    /// 常用验证规则:Required,RegularExpression,StringLength,Range,Phone,Email,Url,CreditCard
    /// 先验证，验证通过才set属性
    /// </summary>
    public class Depot : DataAnnotationBase
    {
        private int _id;
        [Required(ErrorMessage ="Id field is required.")]
        public int Id
        {
            get { return _id; }
            set
            {
                //这里使用了手动验证，方法来自于自己写的DataAnnotationBase类
                ValidateProperty("Id", value);
                SetProperty(ref _id, value);
            }
        }

        private string _productName;
        [StringLength(5, MinimumLength = 3)]
        public string ProductName
        {
            get { return _productName; }
            set
            {
                //这里使用了手动验证，方法来自于自己写的DataAnnotationBase类
                ValidateProperty("ProductName", value);
                SetProperty(ref _productName, value);
            }
        }
        //如果不使用手动验证，这里的Range根本不起作用，原因不明
        private DateTime _pickupDate;
        [Range(typeof(DateTime), "1/4/2022", "30/4/2022")]
        public DateTime PickupDate
        {
            get { return _pickupDate; }
            set { SetProperty(ref _pickupDate, value); }
        }
        //如果不使用手动验证，这里的Range根本不起作用，原因不明
        private DateTime _deliverDate;
        [Range(typeof(DateTime), "1/4/2022", "30/4/2022")]
        public DateTime DeliverDate
        {
            get { return _deliverDate; }
            set { SetProperty(ref _deliverDate, value); }
        }
        //这里的Range根本不起作用，原因不明
        private double _min_Temp;
        [Range(0.0, 100.0)]
        public double Min_Temp
        {
            get { return _min_Temp; }
            set 
            {
                //这里使用了手动验证，方法来自于自己写的DataAnnotationBase类
                ValidateProperty(nameof(Max_Temp), value);
                SetProperty(ref _min_Temp, value); 
            }
        }
        //这里的Range根本不起作用，原因不明
        private double _max_Temp;
        [Range(0.0, 100.0)]
        public double Max_Temp
        {
            get { return _max_Temp; }
            set 
            {
                //这里使用了手动验证，方法来自于自己写的DataAnnotationBase类
                ValidateProperty(nameof(Max_Temp),value);
                SetProperty(ref _max_Temp, value); 
            }
        }
    }
}
