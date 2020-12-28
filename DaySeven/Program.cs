using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DaySeven
{
	class Program
	{
		static void Main(string[] args)
		{
			string inputFile = "./input.txt";
			string[] inputArray = File.ReadAllLines(inputFile);
			List<Bag> bags = new List<Bag>();
			//Parse input
			foreach (string item in inputArray)
			{
				string itemFix = item.Replace("bags", "").Replace("bag", "").Replace(".", "");
				string[] itemSplit = itemFix.Split(" contain ");
				string[] childs = itemSplit[1].Split(",");
				foreach (string child in childs)
				{
					Bag bag = new Bag
					{
						Name = Regex.Replace(child, "[0-9]", "").Trim(),
						Parent = Regex.Replace(itemSplit[0], "[0-9]", "").Trim()
					};
					bags.Add(bag);
				}
			}

			SolvePartOne(bags);
			SolvePartTwo(inputFile);
		}

		static void SolvePartOne(List<Bag> bList)
		{
			List<string> canContainGold = new List<string>();
			List<string> haveChecked = new List<string>();
			List<string> haveBag = new List<string>();
			int bagCount = 0;
			foreach (Bag bag in bList.Where(b => b.Name == "shiny gold"))
			{
				canContainGold.Add(bag.Parent);
				//Console.WriteLine($"{bag.Parent} can contain the gold bag");
				haveBag.Add(bag.Parent);
				bagCount++;
			}
			while (canContainGold.Count > 0)
			{
				string bagToCheck = canContainGold[0];
				haveChecked.Add(bagToCheck);
				canContainGold.RemoveAt(0);
				foreach (Bag bag in bList.Where(b => b.Name == bagToCheck))
				{
					if(bag.Parent != "no other" || bag.Parent != null)
					{
						if (!haveChecked.Contains(bag.Parent) && !haveBag.Contains(bag.Parent))
						{
							bagCount++;
							canContainGold.Add(bag.Parent);
							//Console.WriteLine($"{bag.Parent} can contain gold");
							haveBag.Add(bag.Parent);
						}
					}
				}
			}
			Console.WriteLine($"Part One Solution: {bagCount}");
		}

		static void SolvePartTwo(string inputFile)
		{
			string[] inputArray = File.ReadAllLines(inputFile);
			List<BagTwo> bags = new List<BagTwo>();
			foreach (string item in inputArray)
			{
				string parent = item.Split(" contain ")[0];
				string childString = item.Split(" contain ")[1];
				List<BagChild> childs = new List<BagChild>();
				string[] childArray = childString.Split(",");
				foreach (string c in childArray)
				{
					int count = c[0];
					string name = Regex.Replace(c, "[0-9]", "").Trim();
					BagChild child = new BagChild
					{
						Name = name.Replace("bags", "").Replace("bag", "").Replace(".", ""),
						count = count
					};
					childs.Add(child);
				}
				BagTwo bag = new BagTwo
				{
					Name = Regex.Replace(parent, "[0-9]", "").Replace("bags", "").Replace("bag", "").Replace(".", "").Trim(),
					Childs = childs
				};
				bags.Add(bag);
			}

			foreach (BagTwo bag in bags.Where(b => b.Name == "shiny gold"))
			{
				Console.WriteLine($"Shiny gold bag contains:");
				foreach (BagChild child in bag.Childs)
				{
					Console.WriteLine($"{child.Name}");
				}
			}
		}
	}

	class Bag
	{
		public string Name { get; set; }
		public string Parent { get; set; }
	}

	class BagTwo
	{
		public string Name { get; set; }
		public List<BagChild> Childs { get; set; }
	}

	class BagChild
	{
		public string Name { get; set; }
		public int count { get; set; }
	}
}
