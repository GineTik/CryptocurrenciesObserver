using System;
using System.Windows.Input;

namespace UI.ViewModels.Base;

public class ViewModelCommand : ICommand
{
    private readonly Action<object?> _executeAction;
    private readonly Predicate<object?>? _canExecuteAction;
    
    public ViewModelCommand(Action<object?> executeAction, Predicate<object?>? canExecuteAction = null)
    {
        _executeAction = executeAction;
        _canExecuteAction = canExecuteAction;
    }

    public bool CanExecute(object? parameter)
    {
        return _canExecuteAction?.Invoke(parameter) ?? true;
    }

    public void Execute(object? parameter)
    {
        _executeAction(parameter);
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}