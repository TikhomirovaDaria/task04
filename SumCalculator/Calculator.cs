using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Globalization;

namespace SumCalculator
{
	public class Calculator
	{
		private NumberFormatInfo separator = new NumberFormatInfo();

		public Calculator()
		{
			separator.NumberDecimalSeparator = ".";
		}

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
					negative.Data.Add(string.Format("Position: {0}", j), doubleArgs[i]);
					j++;
				}
				result += doubleArgs[i];
			}

			if (negative.Data.Count != 0)
				throw negative;

			return result;
		}

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
