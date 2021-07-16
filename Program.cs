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
	internal class Program
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
			Console.WriteLine("Your second choice:");
			var choice2 = Console.ReadLine().Trim();
			Console.WriteLine();

			// Readying the data
			var column1Index = Array.IndexOf(columns, choice1);
			var column2Index = Array.IndexOf(columns, choice2);

			List<double> column1 = new List<double>();
			List<double> column2 = new List<double>();

			var query = File.ReadAllLines(path)
				.Skip(1)
				.Where(l => l.Length > 1);

			foreach (var item in query)
			{
				var entry = item.Split(',');

				column1.Add((double.Parse(entry[column1Index])));
				column2.Add((double.Parse(entry[column2Index])));
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

			var n = column1.Count;
			var sumOfX = column1.Sum();
			var sumOfXSquared = column1.Sum(d => d * d);
			var sumOfY = column2.Sum();
			var sumOfYSquared = column2.Sum(d => d * d);

			double sumOfXY = 0;
			for (int i = 0; i < column1.Count; i++)
			{
				sumOfXY += column1[i] * column2[i];
			}

			var Linear = (n * sumOfXY - sumOfX * sumOfY) / Math.Sqrt((n * sumOfXSquared - Math.Pow(sumOfX, 2)) * (n * sumOfYSquared - Math.Pow(sumOfY, 2)));

			Console.WriteLine($"Linear = {Linear}");
		}
	}
}