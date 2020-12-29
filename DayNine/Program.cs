using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DayNine
{
	class Program
	{
		static string inputFile = "./input.txt";
		static int preambleSize = 25;
		static void Main(string[] args)
		{
			string[] input = File.ReadAllLines(inputFile);
			long[] inputs = Array.ConvertAll(input, s => long.Parse(s));

			Console.WriteLine($"Part One: {SolvePartOne(inputs)}");
			Console.WriteLine($"Part two: {SolvePartTwo(inputs, SolvePartOne(inputs))}");
		}

		static long SolvePartTwo(long[] inputs, long findThis)
		{
			for (int x = 0; x < inputs.Length; x++)
			{
				for (int y = x + 2; y < inputs.Length; y++)
				{
					long[] tempArray = inputs[x..y];
					long tempSum = tempArray.Sum();
					if(tempSum == findThis)
					{
						return tempArray.Min() + tempArray.Max();
					}
				}
			}

			return 0;
		}

		static long SolvePartOne(long[] inputs)
		{
			List<long> results = new List<long>();
			for (int i = preambleSize; i < inputs.Length; i++)
			{
				long current = inputs[i];
				int xStart = i - preambleSize;

				for (int x = xStart; x < i; x++)
				{
					for (int y = xStart; y < i; y++)
					{
						if(inputs[x] != inputs[y] && inputs[x] + inputs[y] == current)
						{
							results.Add(current);
						}
					}
				}

				if(!results.Exists(x => x == current))
				{
					return current;
				}
			}

			return 0;
		}
	}
}
