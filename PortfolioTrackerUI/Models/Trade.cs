using System;

namespace PortfolioTracker.Models
{
	public class Trade : IEquatable<Trade>
	{
		/// <summary>
		/// Unique identifier.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// The name of the asset traded.
		/// </summary>
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// The ticker symbol of the asset traded.
		/// </summary>
		public string Ticker { get; set; } = string.Empty;

		public bool IsBuyOrder { get; set; }

		/// <summary>
		/// The date when the trade took place.
		/// </summary>
		public DateTime Date { get; set; }
		/// <summary>
		/// The amount of shares traded.
		/// </summary>
		public decimal Quantity { get; set; }
		/// <summary>
		/// The price per share.
		/// </summary>
		public decimal Price { get; set; }
		/// <summary>
		/// Total tax paid on the trade
		/// </summary>
		public decimal Tax { get; set; }
		/// <summary>
		/// Total commission paid on the trade
		/// </summary>
		public decimal Commission { get; set; }

		public Trade(string name, string ticker, bool isBuyOrder, DateTime date, string quantity, string price, string tax, string commission)
		{
			this.Name = name;
			this.Ticker = ticker;
			this.IsBuyOrder = isBuyOrder;
			this.Date = date;

			decimal quantityValue, priceValue, taxValue, commissionValue;
			decimal.TryParse(quantity, out quantityValue);
			decimal.TryParse(price, out priceValue);
			decimal.TryParse(tax, out taxValue);
			decimal.TryParse(commission, out commissionValue);

			this.Quantity = quantityValue;
			this.Price = priceValue;
			this.Tax = taxValue;
			this.Commission = commissionValue;
		}

		public static bool operator ==(Trade? lvalue, Trade? rvalue)
		{
			if (lvalue is null && rvalue is null)
				return true;

			return lvalue is not null && lvalue.Equals(rvalue);
		}

		public static bool operator !=(Trade? lvalue, Trade? rvalue)
		{
			return !(lvalue == rvalue);
		}

		public bool Equals(Trade? obj)
		{
			return obj is Trade trade && this.Id == trade.Id;
		}

		public override bool Equals(object? obj)
		{
			return obj is Trade trade && this.Id == trade.Id;
		}

		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}
	}
}
