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
    /// 使用System.ComponentModel.DataAnnotations作为验证方法的辅助方法;
    /// 使用ValidationContext和Validator来验证。
    /// </summary>
    public class DataAnnotationBase:BindableBase
    {
        protected virtual void ValidateProperty(string popertyName,object value)
        {
            ValidationContext context = new ValidationContext(this, null, null);
            context.MemberName = popertyName;
            //如果验证规则不通过，该方法会抛出异常
            //在调试状态下运行，程序会在异常的时候跳到此处，然后Continue可以继续运行。如果在Release模式下运行，就不会在跳到此处。
            Validator.ValidateProperty(value, context);
            //这里还可以使用validator.TryValidateProperty()方法，配合IDataErrorInfo使用。
        }
    }
}
