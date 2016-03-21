using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Globalization;

namespace SumCalculator
{
	/// <summary>
	/// Класс, реализующий несколько версий функции сложения:
	/// <see = cref "Add1"/>, <see = cref "Add2"/>, <see = cref "Add3"/>,
	/// <see = cref "Add4"/>, <see = cref "Add5"/>
	/// </summary>
	public class Calculator
	{
		private NumberFormatInfo separator;

		/// <summary>
		/// Кнструктор по умочанию
		/// Создает объект класса <see = cref "NumberFormatInfo"/>
		/// и инициализирует его свойство точкой, что позволяет
		/// использовать ее в качестве десятичного разделителя
		/// </summary>
		public Calculator()
		{
			separator = new NumberFormatInfo();
			separator.NumberDecimalSeparator = ".";
		}

		/// <summary>
		/// Извлекает числа типа double из строки
		/// </summary>
		/// <param name="input">Пользовательская строка с числами-аргументами функции сложения</param>
		/// <param name="delims">Строка, в которой перечислены символы - разделители</param>
		/// <returns>Возвращает массив чисел типа double, если в строке <see = cref "input"/>
		/// числ записаны по следующим правилам:
		/// 1) чисел не меньше 2
		/// 2) числа разделены тлько символами сроки <see = cref "input"/>
		/// Иначе возвращает null</returns>
		private double[] GetNumbers(string input, string delims)
		{
			string number = @"(^[+-]?\b[0-9]+(\.?\d+)?\z)|(^\dE[+-]{1}\d\z)";
			string[] strArray = Regex.Split(input, delims);
			List<double> doubleList = new List<double>();

			for (int i = 0; i < strArray.Length; i++)
			{
				if (!Regex.IsMatch(strArray[i], number))
					return null;
				doubleList.Add(double.Parse(strArray[i], separator));
			}
			if (doubleList.Count < 2)
				return null;

			return doubleList.ToArray();
		}

		/// <summary>
		/// Функция сложения, которая работает корректно только при вводе
		/// двух чисел типа double, перечисленных через запятую
		/// </summary>
		/// <param name="input">Пользовательская строка с числами-аргументами функции сложения</param>
		/// <returns>Возвращает сумму двух чисел, если введенная строка удовлетворяет указанным требованиям
		/// и выбрасывает ошибку <see = cref "ArgumentException"/> в противном случае</returns>
		public double Add1(string input)
		{
			double result = 0;
			double[] doubleArgs = GetNumbers(input, ",");

			if (doubleArgs == null || doubleArgs.Length != 2)
				throw new ArgumentException();

			for (int i = 0; i < 2; i++)
				result += doubleArgs[i];
			return result;
		}

		/// <summary>
		/// Функция сложения, которая работает корректно только при вводе
		/// двух и более чисел типа double, перечисленных через запятую
		/// </summary>
		/// <param name="input">Пользовательская строка с числами-аргументами функции сложения</param>
		/// <returns>Возвращает сумму указанных чисел, если введенная строка удовлетворяет указанным требованиям
		/// и выбрасывает ошибку <see = cref "ArgumentException"/> в противном случае</returns>
		public double Add2(string input)
		{
			double result = 0;
			double[] doubleArgs = GetNumbers(input, ",");

			if (doubleArgs == null)
				throw new ArgumentException();

			for (int i = 0; i < doubleArgs.Length; i++)
				result += doubleArgs[i];
			return result;

		}
		/// <summary>
		/// Функция сложения, которая работает корректно только при вводе
		/// двух и более чисел типа double, перечисленных через один произвольный разделитель
		/// из мнжества {~!@#$%^&*()_;,\\s}
		/// </summary>
		/// <param name="input">Пользовательская строка с числами-аргументами функции сложения</param>
		/// <returns>Возвращает сумму указанных чисел, если введенная строка удовлетворяет указанным требованиям
		/// и выбрасывает ошибку <see = cref "ArgumentException"/> в противном случае</returns>
		public double Add3(string input)
		{
			double result = 0;
			double[] doubleArgs = GetNumbers(input, "[~!@#$%^&*()_;,\\s]{1}");

			if (doubleArgs == null)
				throw new ArgumentException();

			for (int i = 0; i < doubleArgs.Length; i++)
				result += doubleArgs[i];
			return result;
		}

		/// <summary>
		/// Функция сложения, которая работает корректно только при вводе
		/// двух и более неотрицательных чисел типа double, перечисленных через один произвольный разделитель
		/// из мнжества {~!@#$%^&*()_;,\\s}
		/// </summary>
		/// <param name="input">Пользовательская строка с числами-аргументами функции сложения</param>
		/// <returns>Возвращает сумму указанных чисел, если если введенная строка удовлетворяет указанным требованиям
		/// и выбрасывает ошибку <see = cref "ArgumentException"/> в противном случае
		/// Если ошибка вызвана наличием отрицательных чисел,
		/// то они записываются в экземпляр negative ошибки</returns>
		public double Add4(string input)
		{
			double result = 0;
			double[] doubleArgs = GetNumbers(input, "[~!@#$%^&*()_;,\\s]{1}");
			ArgumentException negative = new ArgumentException();

			if (doubleArgs == null)
				throw new ArgumentException();

			for (int i = 0, j = 0; i < doubleArgs.Length; i++)
			{
				if (doubleArgs[i] < 0)
				{
					negative.Data.Add(string.Format("Element: {0}", j), doubleArgs[i]);
					j++;
				}
				result += doubleArgs[i];
			}

			if (negative.Data.Count != 0)
				throw negative;

			return result;
		}

		/// <summary>
		/// Функция сложения, которая работает корректно только при вводе
		/// двух и более неотрицательных чисел типа double, перечисленных через 
		/// один или несколько произвольных разделителей из мнжества {~!@#$%^&*()_;,\\s}
		/// </summary>
		/// <param name="input">Пользовательская строка с числами-аргументами функции сложения</param>
		/// <returns>Возвращает сумму указанных чисел, если если введенная строка удовлетворяет указанным требованиям
		/// и выбрасывает ошибку <see = cref "ArgumentException"/> в противном случае
		/// Если ошибка вызвана наличием отрицательных чисел,
		/// то они записываются в экземпляр negative ошибки</returns>
		public double Add5(string input)
		{
			double result = 0;
			double[] doubleArgs = GetNumbers(input, "[~!@#$%^&*()_;,\\s]+");
			ArgumentException negative = new ArgumentException();

			if (doubleArgs == null)
				throw new ArgumentException();

			for (int i = 0, j = 0; i < doubleArgs.Length; i++)
			{
				if (doubleArgs[i] < 0)
				{
					negative.Data.Add(string.Format("Position: {0}", j), doubleArgs[i]);
					j++;
				}

				result += doubleArgs[i];
			}

			if (negative.Data.Count != 0)
				throw negative;

			return result;
		}
	}
}
