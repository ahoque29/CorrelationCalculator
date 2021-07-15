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
		}
	}
}