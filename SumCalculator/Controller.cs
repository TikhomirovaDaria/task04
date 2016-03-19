using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumCalculator
{
	/// <summary>
	/// Класс организует работу пользователя с объектом класса <see = cref "Calculator"/> и его методами
	/// </summary>
	class Controller
	{
		private Calculator calc; 
		private Dictionary<char, AddFunction> keyMapper;

		private delegate double AddFunction(string input);
		private AddFunction Add;
		/// <summary>
		/// Конструктор по умолчанию
		/// Создает объект <see = cref "calc"/> класса <see = cref "Calculator"/> 
		/// Инициализирует <see = cref "keyMapper"/> версиями функции Add объекта <see = cref "calc"/>
		/// </summary>
		public Controller()
		{
			calc = new Calculator();
			keyMapper = new Dictionary<char, AddFunction>();
			keyMapper.Add('1', calc.Add1);
			keyMapper.Add('2', calc.Add2);
			keyMapper.Add('3', calc.Add3);
			keyMapper.Add('4', calc.Add4);
			keyMapper.Add('5', calc.Add5);
		}

		/// <summary>
		/// Вывод на экран пользовательского меню
		/// </summary>
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

		/// <summary>
		/// Ввод аргуметов для функций сложения
		/// </summary>
		/// <returns>Строка, содержащая пользовательские аргументы</returns>
		string UserArgumens()
		{
			Console.Clear();
			Console.Write("Enter arguments: ");

			return Console.ReadLine();
		}

		/// <summary>
		/// Выбор пользователем метода сложения по ключу <see = cref "choice"/> 
		/// </summary>
		/// <param name="choice">Ключ, по которому выбирается нужная версия функции сложения</param>
		void ChoseFunc(char choice)
		{
			if (keyMapper.ContainsKey(choice))
				Add = keyMapper[choice];
			else
				Add = null;
		}

		/// <summary>
		/// Безопасный вызов функции <see = cref "Add"/>
		/// Обработка исключительнх ситуаций, выбрасываемых функцией <see = cref "Add"/>
		/// </summary>
		/// <param name="arguments">Пользовательские аргуметы, используемые при сложении</param>
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

		/// <summary>
		/// Логика работы меню, которое видит пользовтель  
		/// </summary>
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
