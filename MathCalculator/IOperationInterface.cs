using System;


namespace MathGame
{
	internal interface IOperationInterface
	{

		/// <summary>
		/// Adds two numbers.
		/// </summary>
		/// <param name="a">First number.</param>
		/// <param name="b">Second number.</param>
		/// <returns>The sum of a and b.</returns>
		public double Addition(double a, double b);

		/// <summary>
		/// Subtracts the second number from the first.
		/// </summary>
		/// <param name="a">First number.</param>
		/// <param name="b">Second number.</param>
		/// <returns>The result of a minus b.</returns>
		public double Subtraction(double a, double b);

		/// <summary>
		/// Multiplies two numbers.
		/// </summary>
		/// <param name="a">First number.</param>
		/// <param name="b">Second number.</param>
		/// <returns>The product of a and b.</returns>
		public double Mutliplication(double a, double b);

		/// <summary>
		/// Divides the first number by the second.
		/// </summary>
		/// <param name="a">Numerator.</param>
		/// <param name="b">Denominator.</param>
		/// <returns>The result of a divided by b.</returns>
		/// <exception cref="DivideByZeroException">Thrown when b is zero.</exception>
		public double Division(double a, double b);

		/// <summary>
		/// Raises a number to the power of another number.
		/// </summary>
		/// <param name="a">Base number.</param>
		/// <param name="b">Exponent.</param>
		/// <returns>a raised to the power of b.</returns>
		public double Power(double a, double b);

		/// <summary>
		/// Raises a number to the power of another number using repeated addition (not accurate for non-integer exponents).
		/// </summary>
		/// <param name="a">Base number.</param>
		/// <param name="b">Exponent.</param>
		/// <returns>a added to itself b times.</returns>
		public double PowerScratch(double a, double b);

		/// <summary>
		/// Returns the square root of a number.
		/// </summary>
		/// <param name="a">The number.</param>
		/// <returns>The square root of a.</returns>
		/// <exception cref="ArgumentException">Thrown when a is negative.</exception>
		public double SquareRoot(double a);

		/// <summary>
		/// Returns the square root of a number using the Newton-Raphson method.
		/// </summary>
		/// <param name="a">The number.</param>
		/// <returns>The square root of a.</returns>
		/// <exception cref="ArgumentException">Thrown when a is negative.</exception>
		public double SquareRootScratch(double a);

		/// <summary>
		/// Returns the square root of a number using binary search method.
		/// </summary>
		/// <param name="a">The number.</param>
		/// <returns>The square root of a.</returns>
		/// <exception cref="ArgumentException">Thrown when a is negative.</exception>
		public double SquareRootBinaryScratch(double a);

		/// <summary>
		/// Returns the cube root of a number.
		/// </summary>
		/// <param name="a">The number.</param>
		/// <returns>The cube root of a.</returns>
		public double CubeRoot(double a);

		/// <summary>
		/// Returns the cube root of a number using the Newton-Raphson method.
		/// </summary>
		/// <param name="a">The number.</param>
		/// <returns>The cube root of a.</returns>
		public double CubeRootScratch(double a);

		/// <summary>
		/// Returns e raised to the power of a.
		/// </summary>
		/// <param name="a">Exponent.</param>
		/// <returns>e^a.</returns>
		public double Exponential(double a);

		/// <summary>
		/// Returns e raised to the power of a using Taylor series.
		/// </summary>
		/// <param name="a">Exponent.</param>
		/// <returns>e^a.</returns>
		public double ExponentialScratch(double a);

		/// <summary>
		/// Returns the base-10 logarithm of a number.
		/// </summary>
		/// <param name="a">The number.</param>
		/// <returns>log10(a).</returns>
		/// <exception cref="ArgumentException">Thrown when a is not positive.</exception>
		public double LogarithmBase10(double a);

		/// <summary>
		/// Returns the base-10 logarithm of a number using approximation.
		/// </summary>
		/// <param name="a">The number.</param>
		/// <returns>log10(a).</returns>
		/// <exception cref="ArgumentException">Thrown when a is not positive.</exception>
		public double LogarithmBase10Scratch(double a);

		/// <summary>
		/// Returns the factorial of a number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>n!.</returns>
		/// <exception cref="ArgumentException">Thrown when n is negative.</exception>
		public double Factorial(int n);

		/// <summary>
		/// Returns the factorial of a number using a scratch implementation.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>n!.</returns>
		/// <exception cref="ArgumentException">Thrown when n is negative.</exception>
		public double FactorialScratch(int n);

		/// <summary>
		/// Returns the logarithm of a number with a specified base.
		/// </summary>
		/// <param name="a">The number.</param>
		/// <param name="b">The base.</param>
		/// <returns>log_b(a).</returns>
		/// <exception cref="ArgumentException">Thrown when a is not positive or b is less than or equal to 1.</exception>
		public double Logarithm(double a, double b);

		/// <summary>
		/// Returns the logarithm of a number with a specified base using approximation.
		/// </summary>
		/// <param name="a">The number.</param>
		/// <param name="b">The base.</param>
		/// <returns>log_b(a).</returns>
		/// <exception cref="ArgumentException">Thrown when a is not positive or b is less than or equal to 1.</exception>
		public double LogarithmScratch(double a, double b);

		/// <summary>
		/// Returns the natural logarithm of a number.
		/// </summary>
		/// <param name="a">The number.</param>
		/// <returns>ln(a).</returns>
		/// <exception cref="ArgumentException">Thrown when a is not positive.</exception>
		public double LogarithmNatural(double a);

		/// <summary>
		/// Returns the natural logarithm of a number using approximation.
		/// </summary>
		/// <param name="a">The number.</param>
		/// <returns>ln(a).</returns>
		/// <exception cref="ArgumentException">Thrown when a is not positive.</exception>
		public double LogarithmNaturalScratch(double a);

		/// <summary>
		/// Returns the absolute value of a number.
		/// </summary>
		/// <param name="a">The number.</param>
		/// <returns>The absolute value of a.</returns>
		public double AbsoluteValue(double a);

		/// <summary>
		/// Returns the absolute value of a number using a scratch implementation.
		/// </summary>
		/// <param name="a">The number.</param>
		/// <returns>The absolute value of a.</returns>
		public double AbsoluteValueScratch(double a);

		/// <summary>
		/// Returns the modulus of two numbers.
		/// </summary>
		/// <param name="a">Dividend.</param>
		/// <param name="b">Divisor.</param>
		/// <returns>a % b.</returns>
		/// <exception cref="DivideByZeroException">Thrown when b is zero.</exception>
		public double Modulus(double a, double b);

		/// <summary>
		/// Returns the modulus of two numbers using a scratch implementation.
		/// </summary>
		/// <param name="a">Dividend.</param>
		/// <param name="b">Divisor.</param>
		/// <returns>a % b.</returns>
		/// <exception cref="DivideByZeroException">Thrown when b is zero.</exception>
		public double ModulusScratch(double a, double b);

		/// <summary>
		/// Returns the sine of an angle in radians.
		/// </summary>
		/// <param name="a">Angle in radians.</param>
		/// <returns>sin(a).</returns>
		public double Sin(double a);

		/// <summary>
		/// Returns the sine of an angle in radians using Taylor series.
		/// </summary>
		/// <param name="a">Angle in radians.</param>
		/// <returns>sin(a).</returns>
		public double SinScratch(double a);

		/// <summary>
		/// Returns the cosine of an angle in radians.
		/// </summary>
		/// <param name="a">Angle in radians.</param>
		/// <returns>cos(a).</returns>
		public double Cos(double a);

		/// <summary>
		/// Returns the cosine of an angle in radians using Taylor series.
		/// </summary>
		/// <param name="a">Angle in radians.</param>
		/// <returns>cos(a).</returns>
		public double CosScratch(double a);

		/// <summary>
		/// Returns the tangent of an angle in radians.
		/// </summary>
		/// <param name="a">Angle in radians.</param>
		/// <returns>tan(a).</returns>
		public double Tan(double a);
			
		/// <summary>
		/// Returns the tangent of an angle in radians using scratch implementations of sine and cosine.
		/// </summary>
		/// <param name="a">Angle in radians.</param>
		/// <returns>tan(a).</returns>
		/// <exception cref="DivideByZeroException">Thrown when cosine is zero.</exception>
		public double TanScratch(double a);

		/// <summary>
		/// Returns the cotangent of an angle in radians.
		/// </summary>
		/// <param name="a">Angle in radians.</param>
		/// <returns>cot(a).</returns>
		public double Cot(double a)
		{
			return 1 / Tan(a);
		}

		/// <summary>
		/// Returns the cotangent of an angle in radians using scratch implementations.
		/// </summary>
		/// <param name="a">Angle in radians.</param>
		/// <returns>cot(a).</returns>
		/// <exception cref="DivideByZeroException">Thrown when sine is zero.</exception>
		public double CotScratch(double a)
		{
			double sin = SinScratch(a);
			double cos = CosScratch(a);
			if (sin == 0)
			{
				throw new DivideByZeroException("Cannot divide by zero.");
			}
			return cos / sin;
		}

		/// <summary>
		/// Returns the secant of an angle in radians.
		/// </summary>
		/// <param name="a">Angle in radians.</param>
		/// <returns>sec(a).</returns>
		public double Sec(double a)
		{
			return 1 / Cos(a);
		}

		/// <summary>
		/// Returns the secant of an angle in radians using scratch implementation.
		/// </summary>
		/// <param name="a">Angle in radians.</param>
		/// <returns>sec(a).</returns>
		/// <exception cref="DivideByZeroException">Thrown when cosine is zero.</exception>
		public double SecScratch(double a);

		/// <summary>
		/// Returns the cosecant of an angle in radians.
		/// </summary>
		/// <param name="a">Angle in radians.</param>
		/// <returns>csc(a).</returns>
		public double Csc(double a);

		/// <summary>
		/// Returns the cosecant of an angle in radians using scratch implementation.
		/// </summary>
		/// <param name="a">Angle in radians.</param>
		/// <returns>csc(a).</returns>
		/// <exception cref="DivideByZeroException">Thrown when sine is zero.</exception>
		public double CscScratch(double a);

		/// <summary>
		/// Returns the factorial of a number using a scratch implementation (overload for double).
		/// </summary>
		/// <param name="a">The number.</param>
		/// <returns>a!.</returns>
		/// <exception cref="ArgumentException">Thrown when a is negative.</exception>
		public double FactorialScratch(double a);
		
	}

}
