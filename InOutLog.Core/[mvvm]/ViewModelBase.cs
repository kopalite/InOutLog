using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
using System.Runtime.CompilerServices;

namespace InOutLog.Core
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private readonly string[] _properties;
        private List<RelayCommand> _commands;
        
        public ViewModelBase()
        {
            _properties = GetType().GetTypeInfo().DeclaredProperties.Select(x => x.Name).ToArray();
            _commands = new List<RelayCommand>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void NotifyPropertyChanged(Expression<Func<object>> propertyExpression)
        {
            var unaryExpression = propertyExpression.Body as UnaryExpression;
            var memberExpression = unaryExpression == null ? (MemberExpression)propertyExpression.Body : (MemberExpression)unaryExpression.Operand;
            var propertyName = memberExpression.Member.Name;

            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void RaiseAllPropertyChanged()
        {
            foreach (var property in _properties)
            {
                NotifyPropertyChanged(property);
            }
        }

        public RelayCommand RegisterCommand(RelayCommand command)
        {
            _commands.Add(command);
            return command;
        }

        public void RaiseAllCanExecute()
        {
            foreach (var command in _commands)
            {
                command.OnCanExecuteChanged();
            }
        }
    }
}
