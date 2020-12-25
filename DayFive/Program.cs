using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DayFive
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> boardingPasses = new List<string>();
			List<Seat> seats = new List<Seat>();
			int minRow = 0;
			int maxRow = 127;
			int minColumn = 0;
			int maxColumn = 7;
			
			foreach(string line in File.ReadAllLines("./input.txt"))
			{
				boardingPasses.Add(line);
			}

			foreach (string pass in boardingPasses)
			{
				string rowRule = pass.Substring(0, 7);
				string columnRule = pass.Substring(7);
				int currentMinRow = 0;
				int currentMaxRow = 127;
				int currentMinColumn = 0;
				int currentMaxColumn = 7;
				foreach (char c in pass)
				{
					if(c == 'F')
					{
						currentMaxRow = (currentMaxRow + currentMinRow) / 2;
					}else if(c == 'B')
					{
						currentMinRow = (int)Math.Ceiling((decimal)(currentMaxRow + currentMinRow) / 2);
					}else if(c == 'L')
					{
						currentMaxColumn = (currentMaxColumn + currentMinColumn) / 2;
					}else if(c == 'R')
					{
						currentMinColumn = (int)Math.Ceiling((decimal)(currentMaxColumn + currentMinColumn) / 2);
					}
				}
				int seatID = currentMaxRow * 8 + currentMaxColumn;
				Seat seat = new Seat
				{
					Index = seatID,
					Column = currentMaxColumn,
					Row = currentMaxRow,
					Reference = pass
				};
				//Console.WriteLine($"Row: {currentMaxRow}, column: {currentMaxColumn}, ID: {seatID}");
				seats.Add(seat);
				//Console.WriteLine($"Current boardingpass: {pass}, Current row: {currentMaxRow}, Current Column: {currentMaxColumn}, Seat Index: {seatID}");
			}
			Console.WriteLine($"Number of boarding cards: {seats.Count}. First in dataset is {seats.First().Reference}. Last in dataset is {seats.Last().Reference}");
			Console.WriteLine($"Solution part one: {seats.Max(seat => seat.Index)}");
			seats = seats.OrderBy(seat => seat.Index).ToList();

			for (int i = 0; i < seats.Count; i++)
			{
				if(seats.Exists(s => s.Index == i))
				{
					if(!seats.Exists(se => se.Index == i+1 && seats.Exists(sea => sea.Index == i+2)))
					{
						if(!seats.Exists(s => s.Index == i + 1))
						{
							Console.WriteLine($"My seat index is {i + 1}");
						}
					}
				}
			}
		}
	}

	class Seat
	{
		public int Index { get; set; }
		public int Column { get; set; }
		public int Row { get; set; }
		public string Reference { get; set; }

	}
}
