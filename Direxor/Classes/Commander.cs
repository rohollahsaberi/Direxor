﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Direxor.Classes
{
    public class AppCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public static AppCommand GetInstance() => new AppCommand() { CanExecuteFunc = obj => true };

        public Predicate<object> CanExecuteFunc
        {
            get;
            set;
        }

        public Action<object> ExecuteFunc
        {
            get;
            set;
        }

        public bool CanExecute(object parameter) => CanExecuteFunc(parameter);

        public void Execute(object parameter) => ExecuteFunc(parameter);
    }
}
