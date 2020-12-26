using System;
using System.Collections.Generic;
using System.IO;

namespace DaySix
{
	class Program
	{
		static void Main(string[] args)
		{
			string inputString = File.ReadAllText("./input.txt");
			inputString = inputString.Replace("\n\n", ";");
			inputString = inputString.Replace("\n", ":");
			string[] groupAnswers = inputString.Split(";");
			int totalSum = 0;
			int partTwoSum = 0;
			foreach (string item in groupAnswers)
			{
				Dictionary<char, int> answerDict = new Dictionary<char, int>();
				int peopleCount = 0;
				List<char> differentAnswers = new List<char>();
				string[] answers = item.Split(":");
				foreach (string a in answers)
				{
					peopleCount++;
					foreach (char c in a)
					{
						if (answerDict.ContainsKey(c))
						{
							answerDict[c]++;
						}
						else
						{
							answerDict.Add(c, 1);
						}
						if (!differentAnswers.Contains(c))
						{
							differentAnswers.Add(c);
						}
					}
				}
				totalSum += differentAnswers.Count;
				differentAnswers.Clear();
				foreach (KeyValuePair<char, int> a in answerDict)
				{
					if(a.Value == peopleCount)
					{
						partTwoSum++;
					}
				}
			}
			Console.WriteLine($"Answer sum part 1: {totalSum}");
			Console.WriteLine($"Answer sum part 2: {partTwoSum}");
		}
	}
}
