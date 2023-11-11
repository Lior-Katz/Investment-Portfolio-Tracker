namespace TrackerLibrary
{
	public class Holding
	{
		/// <summary>
		/// Unique identifier
		/// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Asset Name
        /// </summary>
        public string Name { get; set; }
		/// <summary>
		/// The 3- or 4-letter symbol of the asset
		/// </summary>
		public string Ticker { get; set; }
		/// <summary>
		/// Number of shares held.
		/// </summary>
		public decimal Quantity { get; set; }
		/// <summary>
		/// List of all trades of this asset.
		/// </summary>
		public List<Trade> Trades { get; set; } = new List<Trade>();
		/// <summary>
		/// Initial date when this asset was aquired.
		/// </summary>
		public DateTime AquisitionDate { get; set; }
		/// <summary>
		/// The periodical payout of this asset.
		/// </summary>
		public Payout Payout { get; set; }
		/// <summary>
		/// The type of investment vehicle.
		/// </summary>
		/// <example>
		/// Company stock, index fund, bond, etc.
		/// </example>
		public string Type { get; set; }
		/// <summary>
		/// The sector of this asset.
		/// </summary>
		public string Sector { get; set; }
		/// <summary>
		/// The market where this asset is traded.
		/// </summary>
		public string Market { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Asset name </param>
		/// <param name="ticker">Ticker symbol</param>
		/// <param name="quantity">Quantity held</param>
		/// <param name="aquisitionDate">Initial date of aquisition</param>
		/// <param name="payout">The periodic payout</param>
		/// <param name="type">Type of investment vehicle</param>
		/// <param name="secotr">Sector</param>
		/// <param name="market">Market</param>
		public Holding(string name, string ticker, decimal quantity, DateTime aquisitionDate, Payout payout, string type, string secotr, string market)
		{
			this.Name = name;
			this.Ticker = ticker;
			this.Quantity = quantity;
			this.AquisitionDate = aquisitionDate;
			this.Payout = payout;
			this.Type = type;
			this.Sector = secotr;
			this.Market = market;

		}
	}
}
