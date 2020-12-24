using System;
using System.Collections.Generic;
using System.IO;

namespace DayOne
{
	class Program
	{
		public static string InputFile = "./input.txt";
		static void Main(string[] args)
		{
			List<int> inputs = new List<int>();
			foreach (string line in File.ReadLines(InputFile))
			{
				inputs.Add(int.Parse(line));
			}
			foreach (int x in inputs)
			{
				foreach (int y in inputs)
				{
					foreach (int z in inputs)
					{
						if(x+y+z == 2020)
						{
							Console.WriteLine(x * y * z);
						}
					}
				}
			}
		}
	}
}
