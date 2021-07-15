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
			// Prompt user for file
			Console.WriteLine("Please select a file: \n");
			var fd = new OpenFileDialog();
			fd.ShowDialog();
			var path = fd.FileName;

			// Prompt user to pick two columns
			var optionsArray = File.ReadLines(path)
				.First()
				.Split(',');

			var options = new Dictionary<int, string>();
			var optionsCounter = 1;

			foreach (var item in optionsArray)
			{
				options.Add(optionsCounter, item);
				optionsCounter++;
			}

			Console.WriteLine("Here are your options:");
			foreach (var option in options)
			{
				Console.WriteLine($"{option.Key}: {option.Value}");
			}
			Console.WriteLine("Please write the number corresponding to the first column you wish to select: ");
			var column1 = int.Parse(Console.ReadLine());
			options.Remove(column1);

			Console.WriteLine("Here are your remaining options: ");
			foreach (var option in options)
			{
				Console.WriteLine($"{option.Key}: {option.Value}");
			}
			Console.WriteLine("Please write the number corresponding to the first column you wish to select: ");
			var column2 = int.Parse(Console.ReadLine());
		}
	}
}