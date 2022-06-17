using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq.Expressions;
using Prism.Mvvm;

namespace UserInputValidationPractice.Models
{
    /// <summary>
    /// 该类是从网上直接COPY的代码
    /// https://stackoverflow.com/questions/26527818/wpf-mvvm-validation-using-data-annotation
    /// 本质上和Job类的验证方法一样
    /// 缺点是：遇到非法值，先set属性值，再验证。
    /// </summary>
    public class AnnotationValidationBase : BindableBase, IDataErrorInfo
    {
        #region Fields

        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();

        #endregion

        //#region Protected

        ///// <summary>
        ///// Sets the value of a property.
        ///// </summary>
        ///// <typeparam name="T">The type of the property value.</typeparam>
        ///// <param name="propertySelector">Expression tree contains the property definition.</param>
        ///// <param name="value">The property value.</param>
        //protected void SetValue<T>(Expression<Func<T>> propertySelector, T value)
        //{
        //    string propertyName = GetPropertyName(propertySelector);

        //    SetValue<T>(propertyName, value);
        //}

        ///// <summary>
        ///// Sets the value of a property.
        ///// </summary>
        ///// <typeparam name="T">The type of the property value.</typeparam>
        ///// <param name="propertyName">The name of the property.</param>
        ///// <param name="value">The property value.</param>
        //protected void SetValue<T>(string propertyName, T value)
        //{
        //    if (string.IsNullOrEmpty(propertyName))
        //    {
        //        throw new ArgumentException("Invalid property name", propertyName);
        //    }

        //    _values[propertyName] = value;
        //    //NotifyPropertyChanged(propertyName);
        //}

        ///// <summary>
        ///// Gets the value of a property.
        ///// </summary>
        ///// <typeparam name="T">The type of the property value.</typeparam>
        ///// <param name="propertySelector">Expression tree contains the property definition.</param>
        ///// <returns>The value of the property or default value if not exist.</returns>
        //protected T GetValue<T>(Expression<Func<T>> propertySelector)
        //{
        //    string propertyName = GetPropertyName(propertySelector);

        //    return GetValue<T>(propertyName);
        //}

        ///// <summary>
        ///// Gets the value of a property.
        ///// </summary>
        ///// <typeparam name="T">The type of the property value.</typeparam>
        ///// <param name="propertyName">The name of the property.</param>
        ///// <returns>The value of the property or default value if not exist.</returns>
        //protected T GetValue<T>(string propertyName)
        //{
        //    if (string.IsNullOrEmpty(propertyName))
        //    {
        //        throw new ArgumentException("Invalid property name", propertyName);
        //    }

        //    object value;
        //    if (!_values.TryGetValue(propertyName, out value))
        //    {
        //        value = default(T);
        //        _values.Add(propertyName, value);
        //    }

        //    return (T)value;
        //}

        /// <summary>
        /// Validates current instance properties using Data Annotations.
        /// </summary>
        /// <param name="propertyName">This instance property to validate.</param>
        /// <returns>Relevant error string on validation failure or <see cref="System.String.Empty"/> on validation success.</returns>
        protected virtual string OnValidate(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("Invalid property name", propertyName);
            }

            string error = string.Empty;
            Object value = GetValue(propertyName);
            List<ValidationResult> results = new List<ValidationResult>(1);
            bool result = Validator.TryValidateProperty(
                value,
                new ValidationContext(this, null, null)
                {
                    MemberName = propertyName
                },
                results);

            if (!result)
            {
                var validationResult = results.First();
                error = validationResult.ErrorMessage;
            }

            return error;
        }



        #region Data Validation

        string IDataErrorInfo.Error
        {
            get
            {
                throw new NotSupportedException("IDataErrorInfo.Error is not supported, use IDataErrorInfo.this[propertyName] instead.");
            }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string s= OnValidate(propertyName);
                return s;
            }
        }

        #endregion

        #region Privates

        //private string GetPropertyName(LambdaExpression expression)
        //{
        //    var memberExpression = expression.Body as MemberExpression;
        //    if (memberExpression == null)
        //    {
        //        throw new InvalidOperationException();
        //    }

        //    return memberExpression.Member.Name;
        //}

        private object GetValue(string propertyName)
        {
            object value;
            if (!_values.TryGetValue(propertyName, out value))
            {
                var propertyDescriptor = TypeDescriptor.GetProperties(GetType()).Find(propertyName, false);
                if (propertyDescriptor == null)
                {
                    throw new ArgumentException("Invalid property name", propertyName);
                }

                value = propertyDescriptor.GetValue(this);
                _values.Add(propertyName, value);
            }
            var propertyDescriptor1 = TypeDescriptor.GetProperties(GetType()).Find(propertyName, false);
            if (propertyDescriptor1 == null)
            {
                throw new ArgumentException("Invalid property name", propertyName);
            }

            value = propertyDescriptor1.GetValue(this);
            return value;
        }

        #endregion

    }
}
