using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumCalculator
{
	class Controller
	{
		private Calculator calc = new Calculator();
		private delegate double AddFunction(string input);
		private AddFunction Add;
		private Dictionary<char, AddFunction> keyMapper = new Dictionary<char, AddFunction>();

		public Controller()
		{
			keyMapper.Add('1', calc.Add1);
			keyMapper.Add('2', calc.Add2);
			keyMapper.Add('3', calc.Add3);
			keyMapper.Add('4', calc.Add4);
			keyMapper.Add('5', calc.Add5);
		}
		void PrintMenu()
		{
			Console.Clear();
			Console.WriteLine("Press:");
			Console.WriteLine("\t1 - Add1 (2 numbers)");
			Console.WriteLine("\t2 - Add2 (some numbers)");
			Console.WriteLine("\t3 - Add3 (some numbers, 1 delim)");
			Console.WriteLine("\t4 - Add4 (positive numbers, 1 delim)");
			Console.WriteLine("\t5 - Add5 (positive numbers, some delims)");
			Console.WriteLine("\tESC - exit");
			Console.Write("Choice: ");
		}
		string UserArgumens()
		{
			Console.Clear();
			Console.Write("Enter arguments: ");

			return Console.ReadLine();
		}
		void ChoseFunc(char choice)
		{
			if (keyMapper.ContainsKey(choice))
				Add = keyMapper[choice];
			else
				Add = null;
		}
		void AddExe(string arguments)
		{
			Console.Clear();
			try
			{
				double result = Add(arguments);
				Console.WriteLine("{0} = {1}", arguments, result);
			}
			catch (ArgumentException ex)
			{
				Console.WriteLine("Argument exception\n");
				if (ex.Data.Count > 0)
				{
					Console.WriteLine("Negatives not expected, but got:");
					foreach (DictionaryEntry neg in ex.Data)
						Console.WriteLine("{0}\t Value: {1}", neg.Key.ToString(), neg.Value);
				}

			}
		}
		public void MenuExe()
		{
			ConsoleKeyInfo key;
			string arguments;

			while (true)
			{
				PrintMenu();

				key = Console.ReadKey();
				ChoseFunc(key.KeyChar);

				if (Add == null)
					Console.WriteLine("Wrong input\n");
				if (key.Key == ConsoleKey.Escape)
					return;

				arguments = UserArgumens();
				AddExe(arguments);
				Console.ReadKey();
			}
		}
	}
}
