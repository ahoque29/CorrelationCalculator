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
		private string firstChoice;
		private string secondChoice;
		private string[] headers;

		public string GetPath()
		{
			Console.WriteLine("Please select a file: \n");
			var fd = new OpenFileDialog();
			fd.ShowDialog();
			return fd.FileName;
		}

		public void SelectColumns(string path)
		{
			headers = File.ReadLines(path)
				.First()
				.Split(',');

			Console.WriteLine("Please pick a column from these given options: ");
			foreach (var column in headers)
			{
				Console.WriteLine(column);
			}
			Console.WriteLine();
			Console.WriteLine("Your first choice: ");
			firstChoice = Console.ReadLine().Trim();
			Console.WriteLine();

			Console.WriteLine("Please pick a column from these remaining options: ");
			foreach (var column in headers)
			{
				if (column != firstChoice)
				{
					Console.WriteLine(column);
				}
			}
			Console.WriteLine();
			Console.WriteLine("Your second choice:");
			secondChoice = Console.ReadLine().Trim();
			Console.WriteLine();
		}

		public IEnumerable<Sample> GetSamples(string path)
		{
			var column1Index = Array.IndexOf(headers, firstChoice);
			var column2Index = Array.IndexOf(headers, secondChoice);

			var samples = new List<Sample>();

			var lines = File.ReadAllLines(path)
				.Skip(1)
				.Where(l => l.Length > 1);

			foreach (var item in lines)
			{
				var entry = item.Split(',');

				yield return new Sample()
				{
					X = double.Parse(entry[column1Index]),
					Y = double.Parse(entry[column2Index])
				};
			}
		}

		public static void SetRanks(IEnumerable<Sample> samples) // MASSIVE DRY VIOLATIONS
		{
			// RankX
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

			// RankY
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
		}

		public IEnumerable<Sample> ProcessFile()
		{
			var path = GetPath();
			SelectColumns(path);
			var samples = GetSamples(path).ToList();
			SetRanks(samples);

			return samples;
		}
	}
}