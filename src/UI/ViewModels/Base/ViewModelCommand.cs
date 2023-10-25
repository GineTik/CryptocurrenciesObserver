using System;
using System.Windows.Input;

namespace UI.ViewModels.Base;

public class ViewModelCommand : ICommand
{
    private readonly Action<object> _executeAction;
    private readonly Predicate<object?>? _canExecuteAction;

    public ViewModelCommand(Action<object> executeAction)
    {
        _executeAction = executeAction;
        _canExecuteAction = null;
    }
    
    public ViewModelCommand(Action<object> executeAction, Predicate<object?> canExecuteAction)
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
        throw new NotImplementedException();
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}