namespace CorrelationCalculator
{
	/// <summary>
	/// Class that holds the data of each entry.
	/// </summary>
	public class Sample
	{
		/// <summary>
		/// Property that holds the data from the first column of the entry.
		/// </summary>
		public double X
		{
			get;
			set;
		}

		/// <summary>
		/// Property that holds the data from the second column of the entry.
		/// </summary>
		public double Y
		{
			get;
			set;
		}

		/// <summary>
		/// Property that holds the rank of the first column of the entry.
		/// </summary>
		public double RankX
		{
			get;
			set;
		}

		/// <summary>
		/// Property that holds the rank of the second column of the entry.
		/// </summary>
		public double RankY
		{
			get;
			set;
		}
	}
}