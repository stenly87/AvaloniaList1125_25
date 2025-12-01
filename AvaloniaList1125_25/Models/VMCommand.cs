using System;
using System.Windows.Input;

namespace AvaloniaList1125_25.Models;

// реализация паттерна Команда 
// нужен для создания свойств-команд, чтобы к ним можно было создать привязку через Binding
public class VMCommand : ICommand
{
    private Action action;

    public VMCommand(Action action)
    {
        this.action = action;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        action();
    }

    public event EventHandler? CanExecuteChanged;
}