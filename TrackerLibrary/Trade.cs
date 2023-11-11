namespace TrackerLibrary
{
	public class Trade
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
	}
}
