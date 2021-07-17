using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CorrelationCalculator
{
	/// <summary>
	/// Class that manages the input file
	/// </summary>
	public class FileManager
	{
		public string FirstChoice { get; set; }
		public string SecondChoice { get; set; }
		public string[] Headers { get; set; }
		public IEnumerable<string> FileData { get; set; }


		public string GetPath()
		{
			Console.WriteLine("Please select a file: \n");
			var fd = new OpenFileDialog();
			fd.ShowDialog();
			return fd.FileName;
		}

		public void SelectColumns(string path)
		{
			Headers = File.ReadLines(path)
				.First()
				.Split(',');

			Console.WriteLine("Please pick a column from these given options: ");
			foreach (var column in Headers)
			{
				Console.WriteLine(column);
			}
			Console.WriteLine();
			Console.WriteLine("Your first choice: ");
			FirstChoice = Console.ReadLine().Trim();
			Console.WriteLine();

			Console.WriteLine("Please pick a column from these remaining options: ");
			foreach (var column in Headers)
			{
				if (column != FirstChoice)
				{
					Console.WriteLine(column);
				}
			}
			Console.WriteLine();
			Console.WriteLine("Your second choice:");
			SecondChoice = Console.ReadLine().Trim();
			Console.WriteLine();
		}

		public IEnumerable<Sample> Sampler(string firstChoice, string secondChoice, string path)
		{
			
			var column1Index = Array.IndexOf(Headers, firstChoice);
			var column2Index = Array.IndexOf(Headers, secondChoice);

			var samples = new List<Sample>();

			var FileData = File.ReadAllLines(path)
				.Skip(1)
				.Where(l => l.Length > 1);

			foreach (var item in FileData)
			{
				var entry = item.Split(',');

				samples.Add(new Sample()
				{
					X = double.Parse(entry[column1Index]),
					Y = double.Parse(entry[column2Index])
				});
			}

			return samples;
		}
	}
}
