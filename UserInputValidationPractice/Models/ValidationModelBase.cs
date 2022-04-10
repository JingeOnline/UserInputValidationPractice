using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UserInputValidationPractice.Models
{
    /// <summary>
    /// 该类继承了Prism.Mvvm.BindableBase类，可以作为任需要验证的Model类的父类。
    /// </summary>
    public class ValidationModelBase: BindableBase, INotifyDataErrorInfo
    {
        //使用字典来储存每个属性，和该属性的错误
        private readonly Dictionary<string, List<string>> _errorByPropertyName = new Dictionary<string, List<string>>();


        //当触发该事件的时候，会自动重新执行GetErrors方法
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        //如果验证失败，有错误，需要返回true。
        //如果字典中有错误，返回true，没有错误，返回false
        public bool HasErrors => _errorByPropertyName.Any();


        //针对某个特定的属性，返回其错误列表
        public IEnumerable GetErrors(string propertyName)
        {
            if (propertyName != null && _errorByPropertyName.ContainsKey(propertyName))
            {
                //返回该属性的错误列表
                return _errorByPropertyName[propertyName];
            }
            else
            {
                //返回一个空的集合，相当于返回new List<string>(0)。这样写的好处是能够节省内存，不为空集合开辟内存空间。
                return Enumerable.Empty<string>();
            }
        }

        //Raise ErrorsChanged事件 
        protected virtual void OnErrorsChanged(DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
        }

        //添加属性名和其名下的错误
        protected void AddError(string error, [CallerMemberName]string? propertyName=null)
        {
            if (propertyName == null) return;
            //如果该属性一个错误都没有
            if (!_errorByPropertyName.ContainsKey(propertyName))
            {
                _errorByPropertyName[propertyName] = new List<string>();
            }
            //如果该属性有错误，但没有当前这个错误
            if (!_errorByPropertyName[propertyName].Contains(error))
            {
                _errorByPropertyName[propertyName].Add(error);
                //触发事件
                OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
                //触发HasErrors属性，让他重新获取值
                RaisePropertyChanged(nameof(HasErrors));
            }
        }

        protected void ClearErrors([CallerMemberName] string? propertyName = null)
        {
            if (propertyName == null) return;
            if (_errorByPropertyName.ContainsKey(propertyName))
            {
                //删除字典中的键值对，也就是连该属性的key都删除
                _errorByPropertyName.Remove(propertyName);
                //触发事件
                OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
                //触发HasErrors属性，让他重新获取值
                RaisePropertyChanged(nameof(HasErrors));
            }
        }
    }
}
