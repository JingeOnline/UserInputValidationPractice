using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInputValidationPractice.Models
{
    public class DataAnnotationBase:BindableBase
    {
        protected virtual void ValidateProperty(string popertyName,object value)
        {
            ValidationContext context = new ValidationContext(this, null, null);
            context.MemberName = popertyName;
            //如果验证规则不通过，该方法会抛出异常
            Validator.ValidateProperty(value, context);
        }
    }
}
