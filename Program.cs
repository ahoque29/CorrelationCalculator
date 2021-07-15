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
		}
	}
}