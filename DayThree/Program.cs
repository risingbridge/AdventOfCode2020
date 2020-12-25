using System;
using System.Collections.Generic;
using System.IO;

namespace DayThree
{
	class Program
	{
		static void Main(string[] args)
		{
			//Parse input
			int mapWidth = File.ReadAllLines("./input.txt")[0].Length;
			int mapHeight = 0;
			foreach (string line in File.ReadAllLines("./input.txt"))
			{
				mapHeight++;
			}

			Console.WriteLine($"Map Width: {mapWidth}, Map Height: {mapHeight}");
			MapFeature[,] MapArray = new MapFeature[mapWidth, mapHeight];
			int y = 0;
			foreach (string line in File.ReadAllLines("./input.txt"))
			{
				int x = 0;
				foreach (char c in line)
				{
					MapFeature currentFeature = MapFeature.Open;
					if(c == '#')
					{
						currentFeature = MapFeature.Tree;
					}
					MapArray[x, y] = currentFeature;
					x++;
				}
				y++;
			}

			List<int> TreeCountList = new List<int>();
			List<SledSettings> settings = new List<SledSettings>
			{
				new SledSettings{x=1,y=1 },
				new SledSettings{x=3,y=1 },
				new SledSettings{x=5,y=1 },
				new SledSettings{x=7,y=1 },
				new SledSettings{x=1,y=2 }
			};
			foreach (SledSettings setting in settings)
			{
				//Find route
				int currentX = 0;
				int currentY = 0;

				int MoveX = setting.x;
				int moveY = setting.y;

				int treeCount = 0;
				while (currentY < mapHeight)
				{
					//Console.WriteLine($"X: {currentX}, Y: {currentY} - {MapArray[currentX, currentY]}");
					if (MapArray[currentX, currentY] == MapFeature.Tree)
					{
						treeCount++;
					}
					currentX += MoveX;
					if (currentX >= mapWidth)
					{
						currentX -= mapWidth;
					}
					currentY += moveY;
				}
				TreeCountList.Add(treeCount);
				Console.WriteLine($"You hit {treeCount} trees with the settings X{MoveX}Y{moveY}");
			}

			double multipliedTreeCount = 1;
			foreach (int TreeCount in TreeCountList)
			{
				multipliedTreeCount *= TreeCount;
			}
			Console.WriteLine($"The multiplied answer is {multipliedTreeCount}");
		}
	}

	class SledSettings
	{
		public int x { get; set; }
		public int y { get; set; }
	}

	enum MapFeature
	{
		Open,
		Tree
	}
}
