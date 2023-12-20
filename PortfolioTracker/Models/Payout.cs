using System;
using System.Collections.Generic;

namespace PortfolioTracker.Models
{
	public class Payout
	{
		/// <summary>
		/// Unique identifier
		/// </summary>
		public int Id { get; private set; }

		/// <summary>
		/// The yield of this payout, as a fraction of the total value of the the asset.
		/// </summary>
		public decimal Yield { get; set; }

		/// <summary>
		/// The tax incurred on this payout.
		/// </summary>
		public decimal Tax { get; set; }

		/// <summary>
		/// The commission paid on this payout.
		/// </summary>
		public decimal Commission { get; set; }

		/// <summary>
		/// Number of months between each payout.
		/// </summary>
		public int period { get; set; }

		/// <summary>
		/// The date when the payout was last paid.
		/// </summary>
		public DateOnly LastPaid { get; set; }

		/// <summary>
		/// Initializes a new instance of the Payout class with specified parameters.
		/// </summary>
		/// <param name="yield">The yield of the payout.</param>
		/// <param name="tax">The tax amount associated with the payout.</param>
		/// <param name="commission">The commission amount deducted from the payout.</param>
		/// <param name="period">Number of months between each payout.</param>
		/// <param name="lastPaid">The date when the payout was last paid.</param>
		public Payout(decimal yield, decimal tax, decimal commission, int period, DateOnly lastPaid)
		{
			this.Yield = yield;
			this.Tax = tax;
			this.Commission = commission;
			this.LastPaid = lastPaid;

		}


		/// <summary>
		/// Returns a list of the dates of the next payments.
		/// </summary>
		/// <param name="n">Number of future payments to calculate.</param>
		/// <returns>A List that contains the dates of the next n payments. The list is sorted from earliest to latest.</DateOnly></returns>
		public List<DateOnly> getFuturePayouts(int n)
		{
			List<DateOnly> result = new List<DateOnly>();
			for (int i = 1; i <= n; ++i)
			{
				result.Add(this.LastPaid.AddMonths(i * this.period));
			}
			return result;
		}
	}
}
