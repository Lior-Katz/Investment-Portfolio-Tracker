using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
	public class Payout
	{
		/// <summary>
		/// Unique identifier
		/// </summary>
		public int Id { get; set; }
        public decimal Yield { get; set; }
        public decimal Tax { get; set; }
        public decimal Commission { get; set; }
    }
}
