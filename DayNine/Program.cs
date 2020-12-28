using System;
using System.Collections.Generic;
using System.IO;

namespace DayNine
{
	class Program
	{
		static void Main(string[] args)
		{
			List<double> inputList = new List<double>();
			foreach (string item in File.ReadAllLines("./debug.txt"))
			{
				inputList.Add(double.Parse(item));
			}
			//int[] inputArray = File.ReadAllLines("./input.txt");


			List<double> preambleList = new List<double>();
			List<double> preambleSums = new List<double>();
			int preamble = 5;
			bool skippedOnce = false;

			for (int i = 0; i < preamble; i++)
			{
				preambleList.Add(inputList[i]);
			}
			preambleSums = new List<double>(CalculatePreambleSums(preamble, preambleList));

			for (int i = preamble; i < inputList.Count; i++)
			{
				if (!preambleSums.Contains(inputList[i]))
				{
					if (skippedOnce)
					{
						Console.WriteLine($"{inputList[i]} is wrong!");
					}
					else
					{
						Console.WriteLine($"This does not compute, {inputList[i]}. Old sums is {preambleSums.Count} long");
						preambleList.Add(inputList[i]);
						preambleSums.Clear();
						preambleSums = new List<double>(CalculatePreambleSums(preamble, preambleList));
						Console.WriteLine($"Realculated, number of sums is {preambleSums.Count}");
						skippedOnce = true;
					}
				}
			}
		}

		static List<double> CalculatePreambleSums(int preamble, List<double> pl)
		{
			List<double> sums = new List<double>();

			for (int x = 0; x < preamble; x++)
			{
				for (int y = 0; y < preamble; y++)
				{
					if(!sums.Contains(pl[x] + pl[y]))
					{
						sums.Add(pl[x] + pl[y]);
					}
				}
			}

			return sums;
		}
	}
}
