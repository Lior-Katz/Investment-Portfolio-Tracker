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
		public decimal Yield { get; set; }
		public decimal Tax { get; set; }
		public decimal Commission { get; set; }
		public int period { get; set; }
		public DateOnly LastPaid { get; set; }


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
		/// <param name="n">Number of future payments.</param>
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
