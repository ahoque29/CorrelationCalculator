using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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
		private static void Main()
		{
			Console.WriteLine("Please select a file: \n");
			var fd = new OpenFileDialog();
			fd.ShowDialog();
			var path = fd.FileName;

			var columns = File.ReadLines(path)
				.First()
				.Split(',');

			Console.WriteLine("Please pick a column from these given options: ");
			foreach (var column in columns)
			{
				Console.WriteLine(column);
			}
			Console.WriteLine();
			Console.WriteLine("Your first choice: ");
			var choice1 = Console.ReadLine().Trim();
			Console.WriteLine();

			Console.WriteLine("Please pick a column from these remaining options: ");
			foreach (var column in columns)
			{
				if (column != choice1)
				{
					Console.WriteLine(column);
				}
			}
			Console.WriteLine();
			Console.WriteLine("Your second choice:");
			var choice2 = Console.ReadLine().Trim();
			Console.WriteLine();

			// Readying the data
			var column1Index = Array.IndexOf(columns, choice1);
			var column2Index = Array.IndexOf(columns, choice2);

			var samples = new List<Sample>();

			var query = File.ReadAllLines(path)
				.Skip(1)
				.Where(l => l.Length > 1);

			foreach (var item in query)
			{
				var entry = item.Split(',');

				samples.Add(new Sample()
				{
					X = double.Parse(entry[column1Index]),
					Y = double.Parse(entry[column2Index])
				});
			}

			// Linear correlation coefficient
			var n = samples.Count();
			var sumOfX = samples.Sum(s => s.X);
			var sumOfXSquared = samples.Sum(s => s.X * s.X);
			var sumOfY = samples.Sum(s => s.Y);
			var sumOfYSquared = samples.Sum(s => s.Y * s.Y);
			var sumOfXY = samples.Sum(s => s.X * s.Y);

			var linear = (n * sumOfXY - sumOfX * sumOfY) / Math.Sqrt((n * sumOfXSquared - sumOfX * sumOfX) * (n * sumOfYSquared - sumOfY * sumOfY));
			Console.WriteLine($"Linear Correleation Coefficient: {linear}");

			// Spearman correlation coefficient
			int rank = 1;
			foreach (var sample in samples.OrderByDescending(s => s.X))
			{
				sample.RankX = rank;
				rank++;
			}

			foreach (var ranking in samples.OrderByDescending(s => s.X).GroupBy(s => s.X))
			{
				double sumOfRank = ranking.Sum(s => s.RankX);

				foreach (var sample in ranking)
				{
					sample.RankX = sumOfRank / ranking.Count();
				}
			}

			rank = 1;
			foreach (var sample in samples.OrderByDescending(s => s.Y))
			{
				sample.RankY = rank;
				rank++;
			}

			foreach (var ranking in samples.OrderByDescending(s => s.Y).GroupBy(s => s.Y))
			{
				double sumOfRank = ranking.Sum(s => s.RankY);

				foreach (var sample in ranking)
				{
					sample.RankY = sumOfRank / ranking.Count();
				}
			}

			var spearman = 1 - 6 * samples.Sum(s => Math.Pow((s.RankX - s.RankY), 2)) / (Math.Pow(n, 3) - n);
			Console.WriteLine($"Spearman Rank Correleation Coefficient: {spearman}");
		}
	}
}