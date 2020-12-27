using System;
using System.Collections.Generic;
using System.IO;

namespace DayEight
{
	class Program
	{
		static void Main(string[] args)
		{
			//Instructions:
			//ACC - Legger til eller trekker fra accumulatoren
			//JMP - Hopper til ny kodelinje, relativ til denne, f.eks jmp +4 hopper fire linjer videre
			//NOP - No operations. Går videre til neste.
			//

			string[] inputArray = File.ReadAllLines("./input.txt");
			List<Operation> ProgramQueue = new List<Operation>();
			foreach (string item in inputArray)
			{
				string[] i = item.Split(" ");
				Operation o = new Operation
				{
					Instruction = i[0],
					Value = int.Parse(i[1])
				};
				ProgramQueue.Add(o);
			}

			RunProgram(ProgramQueue);
		}

		static void RunProgram(List<Operation> PQ)
		{
			int programCounter = 0;
			int accumulator = 0;
			bool halt = false;
			bool loopDetected = false;
			List<int> testedPositions = new List<int>();

			List<int> previouslyVisited = new List<int>();
			while (!halt)
			{
				previouslyVisited.Add(programCounter);
				//Console.Clear();
				if(programCounter >= PQ.Count)
				{
					halt = true;
					break;
				}
				Operation currentOP = PQ[programCounter];
				//Console.WriteLine($"Accumulator: {accumulator}, Program counter: {programCounter}, Inst: {currentOP.Instruction}, val: {currentOP.Value}");
				string inst = currentOP.Instruction;

				if (loopDetected)
				{
					if (inst == "jmp" && !testedPositions.Contains(programCounter) || inst == "nop" && !testedPositions.Contains(programCounter))
					{
						loopDetected = false;
						Console.WriteLine($"Loop detected, trying new fix at line {programCounter}");
						testedPositions.Add(programCounter);
						if (inst == "jmp")
						{
							inst = "nop";
						}
						else if (inst == "nop")
						{
							inst = "jmp";
						}
					}
				}

				if(inst == "jmp")
				{
					programCounter += currentOP.Value;
				}else if(inst == "acc")
				{
					accumulator += currentOP.Value;
					programCounter++;
				}else if(inst == "nop")
				{
					programCounter += 1;
				}

				//Loop detected!
				if(previouslyVisited.Contains(programCounter))
				{
					previouslyVisited.Clear();
					accumulator = 0;
					programCounter = 0;
					loopDetected = true;
				}
				if(programCounter > PQ.Count)
				{
					halt = true;
				}
				//Console.ReadKey();
			}
			Console.WriteLine($"Program halted. Accumulator: {accumulator}");
		}
	}

	class Operation
	{
		public string Instruction { get; set; }
		public int Value { get; set; }

	}
}
