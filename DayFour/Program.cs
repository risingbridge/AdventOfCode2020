using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace DayFour
{
	class Program
	{
		static void Main(string[] args)
		{
			//Parse input
			string[] Passports = File.ReadAllText("./input.txt").Split("\n\n");
			List<Passport> passportList = new List<Passport>();
			List<Passport> validPassports = new List<Passport>();
			List<Passport> extraValidPassports = new List<Passport>();
			for (int i = 0; i < Passports.Length; i++)
			{
				string p = Passports[i].Replace("\n", " ");
				Passports[i] = p;
			}

			foreach (var item in Passports)
			{
				string[] splitString = item.Split(" ");
				Passport currentPassport = new Passport();
				foreach (string part in splitString)
				{
					if (part.Contains("byr"))
					{
						currentPassport.byr = part.Split(":")[1];
					}
					else if (part.Contains("iyr"))
					{
						currentPassport.iyr = part.Split(":")[1];
					}
					if (part.Contains("eyr"))
					{
						currentPassport.eyr = part.Split(":")[1];
					}
					if (part.Contains("hgt"))
					{
						currentPassport.hgt = part.Split(":")[1];
					}
					if (part.Contains("hcl"))
					{
						currentPassport.hcl = part.Split(":")[1];
					}
					if (part.Contains("ecl"))
					{
						currentPassport.ecl = part.Split(":")[1];
					}
					if (part.Contains("pid"))
					{
						currentPassport.pid = part.Split(":")[1];
					}
					if (part.Contains("cid"))
					{
						currentPassport.cid = part.Split(":")[1];
					}
				}
				passportList.Add(currentPassport);
			}
			Console.WriteLine($"Added passports: {passportList.Count}");

			//Check for valid passports part1
			int validCount = 0;
			foreach (Passport p in passportList)
			{
				if(p.byr != null && p.iyr != null && p.eyr != null && p.hgt != null && p.hcl != null && p.ecl != null && p.pid != null)
				{
					validCount++;
					validPassports.Add(p);
				}
			}

			Console.WriteLine($"Valid passports with part1-rules: {validCount}");

			//Part2-check
			foreach (Passport p in validPassports)
			{
				bool validPassport = true;
				//Sjekker BYR
				if(int.TryParse(p.byr, out int byr))
				{
					if(byr > 2002 || byr < 1920)
					{
						validPassport = false;
					}
				}
				else
				{
					validPassport = false;
				}
				//Sjekker IYR
				if(int.TryParse(p.iyr, out int iyr))
				{
					if(iyr < 2010 || iyr > 2020)
					{
						validPassport = false;
					}
				}
				else
				{
					validPassport = false;
				}
				//Sjekker EYR
				if (int.TryParse(p.eyr, out int eyr))
				{
					if(eyr < 2020 || eyr > 2030)
					{
						validPassport = false;
					}
				}
				else
				{
					validPassport = false;
				}
				//Sjekker høyde
				if (p.hgt.Contains("cm"))
				{
					string temp = p.hgt.Replace("cm", "");
					if(int.TryParse(temp, out int hgt)){
						if(hgt < 150 || hgt > 193)
						{
							validPassport = false;
						}
					}
					else
					{
						validPassport = false;
					}
				}
				else if (p.hgt.Contains("in"))
				{
					string temp = p.hgt.Replace("in", "");
					if (int.TryParse(temp, out int hgt))
					{
						if (hgt < 59 || hgt > 76)
						{
							validPassport = false;
						}
					}
					else
					{
						validPassport = false;
					}
				}
				else
				{
					validPassport = false;
				}
				//Sjekker HCL
				if (!Regex.Match(p.hcl, "^#(?:[0-9a-fA-F]{3}){1,2}$").Success)
				{
					validPassport = false;
				}
				//Sjekker ecl
				if(p.ecl == "amb" || p.ecl == "blu" || p.ecl == "brn" || p.ecl == "gry" || p.ecl == "grn" || p.ecl == "hzl" || p.ecl == "oth")
				{

				}
				else
				{
					validPassport = false;
				}
				//Sjekker PID
				if (int.TryParse(p.pid, out int pid)) {
					if(p.pid.Length != 9)
					{
						validPassport = false;
					}
				}
				else
				{
					validPassport = false;
				}

				if (validPassport)
				{
					extraValidPassports.Add(p);
				}
			}
			Console.WriteLine($"Valid passports after part two-check: {extraValidPassports.Count}");

		}
	}

	class Passport
	{
		public string byr { get; set; }
		public string iyr { get; set; }
		public string eyr { get; set; }
		public string hgt { get; set; }
		public string hcl { get; set; }
		public string ecl { get; set; }
		public string pid { get; set; }
		public string cid { get; set; }


	}
}
