using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumCalculator
{
	/// <summary>
	/// Точка входа программы
	/// </summary>
	class Program
	{
		/// <summary>
		/// Создание объекта класса <see = cref "Controller"/> и
		/// вызов функции <see = cref "menu.MenuExe"/> для предоставления
		/// пользователю возможности корректной работы с функциями сложения
		/// </summary>
		static void Main(string[] args)
		{
			Controller menu = new Controller();
			menu.MenuExe();
		}
	}
}