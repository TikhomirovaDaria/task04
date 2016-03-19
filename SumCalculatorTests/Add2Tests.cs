using System;
using NUnit.Framework;
using SumCalculator;

namespace SumCalculatorTests
{
	[TestFixture]
	public class Add2Tests
	{
		private Calculator calc = new Calculator();

		[Test]
		[TestCase("12.5,-13", ExpectedResult = -0.5)]
		[TestCase("-3,+7", ExpectedResult = 4)]
		[TestCase("0,0", ExpectedResult = 0)]
		[TestCase("-0,+0", ExpectedResult = 0)]
		[TestCase("1,-1,1,1,-1", ExpectedResult = 1)]
		[TestCase("12.5,13,+25.00,1E-1,0", ExpectedResult = 50.6)]
		[TestCase("12.5,-13,+25.00,1E-1,0", ExpectedResult = 24.6)]

		public double Add2_StringWhithSomeNumbers_Sum(string input)
		{
			return new Calculator().Add2(input);
		}


		[Test]
		[TestCase("")]
		[TestCase("_")]
		[TestCase("+")]
		[TestCase("-")]
		[TestCase("++")]
		[TestCase("--")]
		[TestCase("12")]
		[TestCase("12.5")]
		[TestCase("-12")]
		[TestCase("12.5-13")]
		[TestCase("12,5-13")]
		[TestCase("s1,2")]
		[TestCase("12,34Ы")]
		[TestCase("-1,1-")]
		[TestCase(",")]
		[TestCase("1,")]
		[TestCase(",1")]
		[TestCase("1,-")]
		[TestCase("-,1")]
		[TestCase("-1 2 3 4")]
		[TestCase("1 2 3 4")]
		[TestCase("1,2,3,z")]
		[TestCase("1,2,3,z,4")]
		[TestCase("1,-1,--1")]
		[TestCase("1,-1,#,1,-1")]
		[TestCase("0,,,")]
		[TestCase("12.5,-13+25.00,1E-1,0")]
		[TestCase("5 12.5 13#+25.00$1E-1%0")]
		[TestCase("@@#")]
		public void Add2_StringWhithInvalidArgs_ArgumentExcepion(string input)
		{
			Assert.Throws<ArgumentException>(delegate() { calc.Add2(input); });
		}
	}
}
