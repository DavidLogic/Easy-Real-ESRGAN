﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Easy_Real_ESRGAN
{
    public class CustomCommand : ICommand
    {
        private readonly Action<object> execAction;
        private readonly Func<object, bool> changeFunc;
        public CustomCommand(Action<object> execAction, Func<object, bool> changeFunc)
        {
            this.execAction = execAction;
            this.changeFunc = changeFunc;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return this.changeFunc.Invoke(parameter);
        }
        public void Execute(object parameter)
        {
            this.execAction.Invoke(parameter);
        }
    }
}
