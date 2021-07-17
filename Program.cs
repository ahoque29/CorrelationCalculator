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
			// Prompt user for file and store the path
			Console.WriteLine("Please select a file: \n");
			var fd = new OpenFileDialog();
			fd.ShowDialog();
			var path = fd.FileName;

			// Prompt user to pick two columns and store the names of the columns
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

			var Samples = new List<Sample>();

			var query = File.ReadAllLines(path)
				.Skip(1)
				.Where(l => l.Length > 1);

			foreach (var item in query)
			{
				var entry = item.Split(',');

				Samples.Add(new Sample()
				{ 
					X = double.Parse(entry[column1Index]),
					Y = double.Parse(entry[column2Index])
				});
			}

			// Linear correlation coefficient
			// Formula = (n(sum(xy)) - (sum(x)sum(y))/Sqrt((n(sum(x^2)) - (sum(x))^2)(n(sum(x^2)) - (sum(x))^2))
			// Required:
			// n
			// Sum of x
			// Sum of x^2
			// Sum of y
			// Sum of y^2
			// Sum of xy

			var n = Samples.Count();
			var sumOfX = Samples.Sum(s => s.X);
			var sumOfXSquared = Samples.Sum(s => s.X * s.X);
			var sumOfY = Samples.Sum(s => s.Y);
			var sumOfYSquared = Samples.Sum(s => s.Y * s.Y);
			var sumOfXY = Samples.Sum(s => s.X * s.Y);

			var Linear = (n * sumOfXY - sumOfX * sumOfY) / Math.Sqrt((n * sumOfXSquared - sumOfX * sumOfX) * (n * sumOfYSquared - sumOfY * sumOfY));
			Console.WriteLine($"Linear Correleation Coefficient: {Linear}");

			// Spearman correlation coefficient

		}
	}
}