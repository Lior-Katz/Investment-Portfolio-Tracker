using System;

namespace PortfolioTracker.Commands
{
	class CancelAddTransactionCommand : CommandsBase
	{
		/// <summary>
		/// Cancels the addition of a new transaction and returns to dashboard.
		/// </summary>
		/// <param name="parameter"></param>
		/// <exception cref="NotImplementedException"></exception>
		public override void Execute(object? parameter) => throw new NotImplementedException();
	}
}
