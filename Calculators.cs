using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrelationCalculator
{
	public static class Calculators
	{
		public static void AdjustRanks(IEnumerable<Sample> samples)
		{
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
		}

		public static double LinearCorrelation(IEnumerable<Sample> samples)
		{
			var n = samples.Count();
			var sumOfX = samples.Sum(s => s.X);
			var sumOfXSquared = samples.Sum(s => s.X * s.X);
			var sumOfY = samples.Sum(s => s.Y);
			var sumOfYSquared = samples.Sum(s => s.Y * s.Y);
			var sumOfXY = samples.Sum(s => s.X * s.Y);

			return (n * sumOfXY - sumOfX * sumOfY) / Math.Sqrt((n * sumOfXSquared - sumOfX * sumOfX) * (n * sumOfYSquared - sumOfY * sumOfY));
		}

		public static double SpearmanCorrelation(List<Sample> samples)
		{
			var n = samples.Count;
			return 1 - 6 * samples.Sum(s => Math.Pow((s.RankX - s.RankY), 2)) / (Math.Pow(n, 3) - n);
		}
	}
}
