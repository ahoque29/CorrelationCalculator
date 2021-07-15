using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;

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
			Console.WriteLine("Your first choice: ");
			var column1 = Console.ReadLine().Trim().ToLower();

			Console.WriteLine("Please pick a column from these remaining options: ");
			foreach (var column  in columns)
			{
				if (column != column1)
				{
					Console.WriteLine(column);
				}
			}
			var column2 = Console.ReadLine().Trim().ToLower();

		}
	}
}