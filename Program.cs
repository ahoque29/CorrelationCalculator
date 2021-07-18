using System;

namespace CorrelationCalculator
{
	/// <summary>
	/// Class containing the main entry point into the console application.
	/// </summary>
	class Program
	{
		/// <summary>
		/// Main entry point into the console application.
		/// </summary>
		[STAThread]
		public static void Main()
		{
			var manager = new FileManager();

			var samples = manager.ProcessFile();
			var linear = Calculators.LinearCorrelation(samples);
			var spearman = Calculators.SpearmanCorrelation(samples);

			Console.WriteLine($"Linear Correleation Coefficient: {linear}");
			Console.WriteLine($"Spearman Rank Correleation Coefficient: {spearman}");
		}
	}
}