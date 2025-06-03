using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsft.VisualStudio.TestTools.UnitTesting.Extensions;
using System;
using System.Core;
using MathGame;									

namespace MathGameTest
{
	[TestClass]
	public class OperationsTest
	{
		private Operations _operations;

		[TestInitialize]
		public void MathGameInit()
		{
			_operations = new Operations();
		}

		/// <summary>
		/// Tests Add method with valid input.
		/// </summary>
		[TestMethod]
		public void Add_Success()
		{
			// arrange
			double a = 5, b = 3;


			// act
			double result = _operations.Add(a, b);

			// assert
			Assert.AreEqual(8, result);
		
		}

		/// <summary>
		/// Tests Add method with valid input with output that will fail.
		/// </summary>
		[TestMethod]
		public void Add_Failure()
		{
			// arrange
			double a = 5, b = 3;

			// act
			double result = _operations.Add(a, b);

			// assert
			Assert.AreNotEqual(20, result);
		}

		/// <summary>
		/// Tests Subtraction method with valid input.
		/// </summary>
		[TestMethod]
		public void Subtract_Success()
		{
			// arrange
			double a = 10, b = 4;

			// act
			double result = _operations.Subtraction(a, b);

			// assert
			Assert.AreEqual(6, result);
		}

		/// <summary>
		/// Tests Mutliplication method with valid input.
		/// </summary>
		[TestMethod]
		public void Multiply_Success()
		{
			// arrange
			double a = 7, b = 6;

			// act
			double result = _operations.Mutliplication(a, b);

			// assert
			Assert.AreEqual(42, result);
		}

		/// <summary>
		/// Tests Division method with valid input.
		/// </summary>
		[TestMethod]
		public void Divide_Success()
		{
			// arrange
			double a = 20, b = 4;

			// act
			double result = _operations.Division(a, b);

			// assert
			Assert.AreEqual(5, result);
		}

		/// <summary>
		/// Tests Division method with zero denominator (should throw).
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(DivideByZeroException))]
		public void Divide_Failure_DivideByZero()
		{
			// arrange
			double a = 10, b = 0;

			// act
			_operations.Division(a, b);

			// assert handled by ExpectedException
		}

		/// <summary>
		/// Tests Power method with valid input.
		/// </summary>
		[TestMethod]
		public void Power_Success()
		{
			// arrange
			double a = 2, b = 3;

			// act
			double result = _operations.Power(a, b);

			// assert
			Assert.AreEqual(8, result);
		}

		/// <summary>
		/// Tests PowerScratch method with valid input.
		/// </summary>
		[TestMethod]
		public void PowerScratch_Success()
		{
			// arrange
			double a = 2, b = 3;

			// act
			double result = _operations.PowerScratch(a, b);

			// assert
			Assert.AreEqual(6, result); // 2 + 2 + 2 = 6
		}

		/// <summary>
		/// Tests SquareRoot method with valid input.
		/// </summary>
		[TestMethod]
		public void SquareRoot_Success()
		{
			// arrange
			double a = 9;

			// act
			double result = _operations.SquareRoot(a);

			// assert
			Assert.AreEqual(3, result, 0.0001);
		}

		/// <summary>
		/// Tests SquareRoot method with negative input (should throw).
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void SquareRoot_Failure_Negative()
		{
			// arrange
			double a = -1;

			// act
			_operations.SquareRoot(a);

			// assert handled by ExpectedException
		}

		/// <summary>
		/// Tests Modulus method with valid input.
		/// </summary>
		[TestMethod]
		public void Modulus_Success()
		{
			// arrange
			double a = 10, b = 3;

			// act
			double result = _operations.Modulus(a, b);

			// assert
			Assert.AreEqual(1, result, 0.0001);
		}

		/// <summary>
		/// Tests Modulus method with zero divisor (should throw).
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(DivideByZeroException))]
		public void Modulus_Failure_DivideByZero()
		{
			// arrange
			double a = 10, b = 0;

			// act
			_operations.Modulus(a, b);

			// assert handled by ExpectedException
		}

		/// <summary>
		/// Tests Factorial method with valid input.
		/// </summary>
		[TestMethod]
		public void Factorial_Success()
		{
			// arrange
			int n = 5;

			// act
			double result = _operations.Factorial(n);

			// assert
			Assert.AreEqual(120, result);
		}

		/// <summary>
		/// Tests Factorial method with negative input (should throw).
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Factorial_Failure_Negative()
		{
			// arrange
			int n = -1;

			// act
			_operations.Factorial(n);

			// assert handled by ExpectedException
		}

		/// <summary>
		/// Tests LogarithmBase10 method with valid input.
		/// </summary>
		[TestMethod]
		public void LogarithmBase10_Success()
		{
			// arrange
			double a = 100;

			// act
			double result = _operations.LogarithmBase10(a);

			// assert
			Assert.AreEqual(2, result, 0.0001);
		}

		/// <summary>
		/// Tests LogarithmBase10 method with zero input (should throw).
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void LogarithmBase10_Failure_Zero()
		{
			// arrange
			double a = 0;

			// act
			_operations.LogarithmBase10(a);

			// assert handled by ExpectedException
		}

		/// <summary>
		/// Tests AbsoluteValue method with negative input.
		/// </summary>
		[TestMethod]
		public void AbsoluteValue_Success()
		{
			// arrange
			double a = -5;

			// act
			double result = _operations.AbsoluteValue(a);

			// assert
			Assert.AreEqual(5, result);
		}

		/// <summary>
		/// Tests AbsoluteValue method with positive input.
		/// </summary>
		[TestMethod]
		public void AbsoluteValue_Positive_Success()
		{
			// arrange
			double a = 5;

			// act
			double result = _operations.AbsoluteValue(a);

			// assert
			Assert.AreEqual(5, result);
		}
	}
}
