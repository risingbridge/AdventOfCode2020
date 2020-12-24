using System;
using System.Collections.Generic;
using System.IO;

namespace DayTwo
{
	class Program
	{
		static void Main(string[] args)
		{
			//Behandler dataene
			List<Password> Inputs = new List<Password>();
			foreach (string line in File.ReadLines("./input.txt"))
			{
				string key = line.Split(": ")[0];
				string password = line.Split(": ")[1];
				Inputs.Add(new Password { key = key, value = password });
			}
			Console.WriteLine("Input parsed!");

			int validPassword = 0;
			//Sjekker etter gyldige passord
			foreach (Password pw in Inputs)
			{
				int minCount = int.Parse(pw.key.Split("-")[0]);
				int maxCount = int.Parse(pw.key.Split("-")[1].Split(" ")[0]);
				string mustContain = pw.key.Split(" ")[1];
				//Console.WriteLine($"Min: {minCount} - Max: {maxCount} - Must contain: {mustContain}");
				int count = 0;
				if(pw.value.Substring(minCount -1, 1) == mustContain)
				{
					count++;
				}
				if (pw.value.Substring(maxCount - 1, 1) == mustContain)
				{
					count++;
				}
				if(count == 1)
				{
					validPassword++;
				}
			}

			Console.WriteLine($"Number of valid passwords: {validPassword}");
		}
	}

	class Password
	{
		public string key { get; set; }
		public string value { get; set; }
	}
}
