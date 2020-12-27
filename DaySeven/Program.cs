using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DaySeven
{
	class Program
	{
		static void Main(string[] args)
		{
			//Parse input
			string[] rules = File.ReadAllLines("./input.txt");
			Dictionary<string, List<string>> bagRules = new Dictionary<string, List<string>>();
			foreach (string rule in rules)
			{
				string[] bagRule = rule.Split(" contain ");
				List<string> childs = bagRule[1].Split(",").Select(x => x.Replace("bags.", "").Replace("bag.", "").Replace("bags", "").Replace("bag", "").Trim()).ToList();

				if (!bagRules.ContainsKey(bagRule[0]))
				{
					bagRules.Add(bagRule[0].Replace("bags ", ""), childs);
				}
			}

			PartOne(bagRules);
		}

		private static void PartOne(Dictionary<string, List<string>> Bags)
		{
			List<string> canContainGold = new List<string>();
			int canContainGoldCount = 0;
			foreach (KeyValuePair<string, List<string>> item in Bags)
			{
				foreach (string b in item.Value)
				{
					if(string.Concat(b.Where(char.IsLetter)) == "shinygold")
					{
						canContainGold.Add(item.Key);
						canContainGoldCount++;
					}
				}
			}

			while(canContainGold.Count > 0)
			{
				string currentCheck = canContainGold[0];
				canContainGold.RemoveAt(0);
				foreach (KeyValuePair<string, List<string>> item in Bags)
				{
					foreach (string b in item.Value)
					{
						Console.WriteLine($"Checking if {b} == {currentCheck}");
						if(string.Concat(b.Where(char.IsLetter)) == string.Concat(currentCheck.Where(char.IsLetter)).Replace("bags",""))
						{
							canContainGold.Add(item.Key);
							canContainGoldCount++;
						}
					}
				}
			}
			Console.WriteLine($"{canContainGoldCount} bags can hold the gold bag");
		}
	}
}
