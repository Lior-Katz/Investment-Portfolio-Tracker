using System;
using System.Windows.Input;

namespace PortfolioTracker.Commands;

public abstract class CommandsBase : ICommand
{
	/// <summary>
	///     Event that notifies when the return value of canExecute may have changed- if one of the input values have changed,
	///     and resulted in the legality of the input changing.
	/// </summary>
	public event EventHandler? CanExecuteChanged;

	/// <summary>
	///     Checks whether the command can be executed in the current state.
	/// </summary>
	/// <param name="parameter">
	///     Data used by the command. If the command does not require data to be passed,
	///     this object can be set to null.
	/// </param>
	/// <returns>True if the command can be executed, false otherwise.</returns>
	public virtual bool CanExecute(object? parameter)
    {
        return true;
    }

	/// <summary>
	///     Defines the method to be called when the command is invoked.
	/// </summary>
	/// <param name="parameter">
	///     Data used by the command. If the command does not require data to be passed,
	///     this object can be set to null.
	/// </param>
	public abstract void Execute(object? parameter);

	/// <summary>
	///     Invokes the CanExecuteChanged event when changes that affect whether or not the command should execute occur.
	/// </summary>
	protected void OnCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, new EventArgs());
    }
}
