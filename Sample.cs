using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrelationCalculator
{
	/// <summary>
	/// Class that holds the data of each entry.
	/// </summary>
	public class Sample
	{		
		public double X { get; set; }
		public int RankX { get; set; }
		public double Y { get; set; }
		public int RankY { get; set; }
	}
}
