using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CorrelationCalculator
{
	/// <summary>
	/// Class that manages the input file
	/// </summary>
	public class FileManager
	{
		private string _firstChoice;
		private string _secondChoice;
		private string[] _headers;

		/// <summary>
		/// Opens a dialog box for the user to select a .csv file and returns the path of the file.
		/// </summary>
		/// <returns>
		/// File path.
		/// </returns>
		public string GetPath()
		{
			Console.WriteLine("Please select a file: \n");
			var fd = new OpenFileDialog
			{
				Filter = "CSV files (*.csv)|*csv"				
			};
			fd.ShowDialog();
			return fd.FileName;
		}

		public void InitialiseHeaders(string path)
		{
			_headers = File.ReadLines(path)
				.First()
				.Split(',');
		}

		public void SelectColumns()
		{
			Console.WriteLine("Please pick a column from these given options:");
			foreach (var column in _headers)
			{
				Console.WriteLine(column);
			}
			Console.WriteLine("Your first choice: ");
			_firstChoice = Console.ReadLine().Trim();
			Console.WriteLine();

			Console.WriteLine("Please pick a column from these remaining options: ");
			foreach (var column in _headers)
			{
				if (column != _firstChoice)
				{
					Console.WriteLine(column);
				}
			}
			Console.WriteLine();
			Console.WriteLine("Your second choice:");
			_secondChoice = Console.ReadLine().Trim();
			Console.WriteLine();
		}

		public IEnumerable<Sample> InitialiseSamples(string path)
		{
			var column1Index = Array.IndexOf(_headers, _firstChoice);
			var column2Index = Array.IndexOf(_headers, _secondChoice);

			var lines = File.ReadLines(path)
				.Skip(1)
				.Where(l => l.Length > 1);

			foreach (var item in lines)
			{
				var entry = item.Split(',');
				if (double.TryParse(entry[column1Index], out double xValue) && double.TryParse(entry[column2Index], out double yValue))
				{
					yield return new Sample()
					{
						X = xValue,
						Y = yValue
					};
				}
				else
				{
					// Validation here
				}
			}
		}

		public static void SetRanks(IEnumerable<Sample> samples) // DRY VIOLATIONS - NOT SURE HOW TO FIX
		{
			// RankX
			int rank = 1;
			foreach (var sample in samples.OrderByDescending(s => s.X))
			{
				sample.RankX = rank;
				rank++;
			}

			// Average out same ranks
			foreach (var ranking in samples.OrderByDescending(s => s.X).GroupBy(s => s.X))
			{
				var average = ranking.Average(s => s.RankX);
				foreach (var sample in ranking)
				{
					sample.RankX = average;
				}
			}

			// RankY
			rank = 1;
			foreach (var sample in samples.OrderByDescending(s => s.Y))
			{
				sample.RankY = rank;
				rank++;
			}

			// Average out same ranks
			foreach (var ranking in samples.OrderByDescending(s => s.Y).GroupBy(s => s.Y))
			{
				var average = ranking.Average(s => s.RankY);
				foreach (var sample in ranking)
				{
					sample.RankY = average;
				}
			}
		}

		public IEnumerable<Sample> ProcessFile()
		{
			var path = GetPath();
			InitialiseHeaders(path);
			SelectColumns();
			var samples = InitialiseSamples(path).ToList();
			SetRanks(samples);

			return samples;
		}
	}
}