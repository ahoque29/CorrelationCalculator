using System;
using System.Collections.Generic;
using System.Linq;

namespace CorrelationCalculator
{
	public static class Calculators
	{
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

		public static double SpearmanCorrelation(IEnumerable<Sample> samples)
		{
			var n = samples.Count();
			return 1 - 6 * samples.Sum(s => Math.Pow((s.RankX - s.RankY), 2)) / (Math.Pow(n, 3) - n);
		}
	}
}