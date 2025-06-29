using System;
using System.Threading.Tasks;

namespace MathGame
{
	internal class OperationsInterface : IOperationInterface
	{

		/// <summary>
		/// Adds two numbers.
		/// </summary>
		/// <param name="a">First number.</param>
		/// <param name="b">Second number.</param>
		/// <returns>The sum of a and b.</returns>
		public double Addition(double a, double b)
		{
			return a + b;
		}

		/// <summary>
		/// Subtracts the second number from the first.
		/// </summary>
		/// <param name="a">First number.</param>
		/// <param name="b">Second number.</param>
		/// <returns>The result of a minus b.</returns>
		public double Subtraction(double a, double b)
		{
			return a - b;
		}

		/// <summary>
		/// Multiplies two numbers.
		/// </summary>
		/// <param name="a">First number.</param>
		/// <param name="b">Second number.</param>
		/// <returns>The product of a and b.</returns>
		public double Mutliplication(double a, double b)
		{
			return a * b;
		}

		/// <summary>
		/// Divides the first number by the second.
		/// </summary>
		/// <param name="a">Numerator.</param>
		/// <param name="b">Denominator.</param>
		/// <returns>The result of a divided by b.</returns>
		/// <exception cref="DivideByZeroException">Thrown when b is zero.</exception>
		public double Division(double a, double b)
		{
			if (b == 0)
			{
				throw new DivideByZeroException("Cannot divide by zero.");
			}
			return a / b;
		}

		/// <summary>
		/// Raises a number to the power of another number.
		/// </summary>
		/// <param name="a">Base number.</param>
		/// <param name="b">Exponent.</param>
		/// <returns>a raised to the power of b.</returns>
		public double Power(double a, double b)
		{
			return Math.Pow(a, b);
		}

		/// <summary>
		/// Raises a number to the power of another number using repeated addition (not accurate for non-integer exponents).
		/// </summary>
		/// <param name="a">Base number.</param>
		/// <param name="b">Exponent.</param>
		/// <returns>a added to itself b times.</returns>
		public double PowerScratch(double a, double b)
		{
			double result = default(double);
			for (int i = 0; i < b; i++)
			{
				result += a;
			}
			return result;
		}

		/// <summary>
		/// Returns the square root of a number.
		/// </summary>
		/// <param name="a">The number.</param>
		/// <returns>The square root of a.</returns>
		/// <exception cref="ArgumentException">Thrown when a is negative.</exception>
		public double SquareRoot(double a)
		{
			if (a < 0)
			{
				throw new ArgumentException("Cannot take the square root of a negative nubmer.");
			}
			return Math.Sqrt(a);
		}

		/// <summary>
		/// Returns the square root of a number using the Newton-Raphson method.
		/// </summary>
		/// <param name="a">The number.</param>
		/// <returns>The square root of a.</returns>
		/// <exception cref="ArgumentException">Thrown when a is negative.</exception>
		public double SquareRootScratch(double a)
		{
			if (a < 0)
			{
				throw new ArgumentException("Cannot take the square root of a negative nubmer.");
			}
			double result = a;
			double lastResult = 0;
			while (Math.Abs(result - lastResult) > 0.0001)
			{
				lastResult = result;
				result = (result + a / result) / 2;
			}
			return result;
		}

		/// <summary>
		/// Returns the square root of a number using binary search.
		/// </summary>
		/// <param name="a">The number.</param>
		/// <returns>The square root of a.</returns>
		/// <exception cref="ArgumentException">Thrown when a is negative.</exception>
		public double SquareRootBinaryScratch(double a)
		{
			if (a < 0)
			{
				throw new ArgumentException("Cannot take the square root of a negative nubmer.");
			}
			double low = 0;
			double high = a;
			double mid = 0;
			while (low <= high)
			{
				mid = (low + high) / 2;
				if (Math.Abs(mid * mid - a) < 0.0001)
				{
					return mid;
				}
				else if (mid * mid < a)
				{
					low = mid + 0.0001;
				}
				else
				{
					high = mid - 0.0001;
				}
			}
			return Math.Abs(mid * mid - a);
		}

		/// <summary>
		/// Returns the cube root of a number.
		/// </summary>
		/// <param name="a">The number.</param>
		/// <returns>The cube root of a.</returns>
		public double CubeRoot(double a)
		{
			return Math.Cbrt(a);
		}

		/// <summary>
		/// Returns the cube root of a number using the Newton-Raphson method.
		/// </summary>
		/// <param name="a">The number.</param>
		/// <returns>The cube root of a.</returns>
		public double CubeRootScratch(double a)
		{
			double result = a;
			double lastResult = 0;
			while (Math.Abs(result - lastResult) > 0.0001)
			{
				lastResult = result;
				result = (2 * result + a / (result * result)) / 3;
			}
			return result;
		}

		/// <summary>
		/// Returns e raised to the power of a.
		/// </summary>
		/// <param name="a">Exponent.</param>
		/// <returns>e^a.</returns>
		public double Exponential(double a)
		{
			return Math.Exp(a);
		}

		/// <summary>
		/// Returns e raised to the power of a using Taylor series.
		/// </summary>
		/// <param name="a">Exponent.</param>
		/// <see cref="https://en.wikipedia.org/wiki/Taylor_series"/>
		/// <returns>e^a.</returns>
		public double ExponentialScratch(double a)
		{
			double result = 1;
			double term = 1;
			int n = 1;
			while (Math.Abs(term) > 0.0001)
			{
				term *= a / n;
				result += term;
				n++;
			}
			return result;
		}

		/// <summary>
		/// Returns the base-10 logarithm of a number.
		/// </summary>
		/// <param name="a">The number.</param>
		/// <returns>log10(a).</returns>
		/// <exception cref="ArgumentException">Thrown when a is not positive.</exception>
		public double LogarithmBase10(double a)
		{
			if (a <= 0)
			{
				throw new ArgumentException("Cannot take the logarithm of a non-positive number.");
			}
			return Math.Log10(a);
		}

		/// <summary>
		/// Returns the base-10 logarithm of a number using approximation.
		/// </summary>
		/// <param name="a">The number.</param>
		/// <returns>log10(a).</returns>
		/// <exception cref="ArgumentException">Thrown when a is not positive.</exception>
		public double LogarithmBase10Scratch(double a)
		{
			if (a <= 0)
			{
				throw new ArgumentException("Cannot take the logarithm of a non-positive number.");
			}
			double result = 0;
			while (Math.Pow(10, result) < a)
			{
				result += 0.0001;
			}
			return result;
		}

		/// <summary>
		/// Returns the factorial of a number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>n!.</returns>
		/// <exception cref="ArgumentException">Thrown when n is negative.</exception>
		public double Factorial(int n)
		{
			if (n < 0)
			{
				throw new ArgumentException("Cannot take the factorial of a negative number.");
			}
			if (n == 0 || n == 1)
			{
				return 1;
			}
			double result = 1;
			for (int i = 2; i <= n; i++)
			{
				result *= i;
			}
			return result;
		}

		/// <summary>
		/// Returns the factorial of a number using a scratch implementation.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>n!.</returns>
		/// <exception cref="ArgumentException">Thrown when n is negative.</exception>
		public double FactorialScratch(int n)
		{
			if (n < 0)
			{
				throw new ArgumentException("Cannot take the factorial of a negative number.");
			}
			if (n == 0 || n == 1)
			{
				return 1;
			}
			double result = 1;
			for (int i = 2; i <= n; i++)
			{
				result *= i;
			}
			return result;
		}

		/// <summary>
		/// Returns the logarithm of a number with a specified base.
		/// </summary>
		/// <param name="a">The number.</param>
		/// <param name="b">The base.</param>
		/// <returns>log_b(a).</returns>
		/// <exception cref="ArgumentException">Thrown when a is not positive or b is less than or equal to 1.</exception>
		public double Logarithm(double a, double b)
		{
			if (a <= 0 || b <= 1)
			{
				throw new ArgumentException("Invalid base or argument for logarithm.");
			}

			return Math.Log(a, b);
		}

		/// <summary>
		/// Returns the logarithm of a number with a specified base using approximation.
		/// </summary>
		/// <param name="a">The number.</param>
		/// <param name="b">The base.</param>
		/// <returns>log_b(a).</returns>
		/// <exception cref="ArgumentException">Thrown when a is not positive or b is less than or equal to 1.</exception>
		public double LogarithmScratch(double a, double b)
		{
			if (a <= 0 || b <= 1)
			{
				throw new ArgumentException("Invalid base or argument for logarithm.");
			}
			double result = 0;
			while (Math.Pow(b, result) < a)
			{
				result += 0.0001;
			}
			return result;
		}

		/// <summary>
		/// Returns the natural logarithm of a number.
		/// </summary>
		/// <param name="a">The number.</param>
		/// <returns>ln(a).</returns>
		/// <exception cref="ArgumentException">Thrown when a is not positive.</exception>
		public double LogarithmNatural(double a)
		{
			if (a <= 0)
			{
				throw new ArgumentException("Cannot take the logarithm of a non-positive number.");
			}
			return Math.Log(a);
		}

		/// <summary>
		/// Returns the natural logarithm of a number using approximation.
		/// </summary>
		/// <param name="a">The number.</param>
		/// <returns>ln(a).</returns>
		/// <exception cref="ArgumentException">Thrown when a is not positive.</exception>
		public double LogarithmNaturalScratch(double a)
		{
			if (a <= 0)
			{
				throw new ArgumentException("Cannot take the logarithm of a non-positive number.");
			}
			double result = 0;
			while (Math.Exp(result) < a)
			{
				result += 0.0001;
			}
			return result;
		}

		/// <summary>
		/// Returns the absolute value of a number.
		/// </summary>
		/// <param name="a">The number.</param>
		/// <returns>The absolute value of a.</returns>
		public double AbsoluteValue(double a)
		{
			return Math.Abs(a);
		}

		/// <summary>
		/// Returns the absolute value of a number using a scratch implementation.
		/// </summary>
		/// <param name="a">The number.</param>
		/// <returns>The absolute value of a.</returns>
		public double AbsoluteValueScratch(double a)
		{
			if (a < 0)
			{
				return -a;
			}
			return a;
		}

		/// <summary>
		/// Returns the modulus of two numbers.
		/// </summary>
		/// <param name="a">Dividend.</param>
		/// <param name="b">Divisor.</param>
		/// <returns>a % b.</returns>
		/// <exception cref="DivideByZeroException">Thrown when b is zero.</exception>
		public double Modulus(double a, double b)
		{
			if (b == 0)
			{
				throw new DivideByZeroException("Cannot divide by zero.");
			}
			return a % b;
		}

		/// <summary>
		/// Returns the modulus of two numbers using a scratch implementation.
		/// </summary>
		/// <param name="a">Dividend.</param>
		/// <param name="b">Divisor.</param>
		/// <returns>a % b.</returns>
		/// <exception cref="DivideByZeroException">Thrown when b is zero.</exception>
		public double ModulusScratch(double a, double b)
		{
			if (b == 0)
			{
				throw new DivideByZeroException("Cannot divide by zero.");
			}
			while (a >= b)
			{
				a -= b;
			}
			return a;
		}

		/// <summary>
		/// Returns the sine of an angle in radians.
		/// </summary>
		/// <param name="a">Angle in radians.</param>
		/// <returns>sin(a).</returns>
		public double Sin(double a)
		{
			return Math.Sin(a);
		}

		/// <summary>
		/// Returns the sine of an angle in radians using Taylor series.
		/// </summary>
		/// <param name="a">Angle in radians.</param>
		/// <see cref="https://en.wikipedia.org/wiki/Taylor_series"/>
		/// <returns>sin(a).</returns>
		public double SinScratch(double a)
		{
			double result = 0;
			double term = a;
			int n = 1;
			while (Math.Abs(term) > 0.0001)
			{
				result += term;
				n += 2;
				term *= -a * a / (n * (n - 1));
			}
			return result;
		}

		/// <summary>
		/// Returns the cosine of an angle in radians.
		/// </summary>
		/// <param name="a">Angle in radians.</param>
		/// <returns>cos(a).</returns>
		public double Cos(double a)
		{
			return Math.Cos(a);
		}

		/// <summary>
		/// Returns the cosine of an angle in radians using Taylor series.
		/// </summary>
		/// <param name="a">Angle in radians.</param>
		/// <see cref="https://en.wikipedia.org/wiki/Taylor_series"/>
		/// <returns>cos(a).</returns>
		public double CosScratch(double a)
		{
			double result = 0;
			double term = 1;
			int n = 0;
			while (Math.Abs(term) > 0.0001)
			{
				result += term;
				n += 2;
				term *= -a * a / (n * (n - 1));
			}
			return result;
		}

		/// <summary>
		/// Returns the tangent of an angle in radians.
		/// </summary>
		/// <param name="a">Angle in radians.</param>
		/// <returns>tan(a).</returns>
		public double Tan(double a)
		{
			return Math.Tan(a);
		}

		/// <summary>
		/// Returns the tangent of an angle in radians using scratch implementations of sine and cosine.
		/// </summary>
		/// <param name="a">Angle in radians.</param>
		/// <returns>tan(a).</returns>
		/// <exception cref="DivideByZeroException">Thrown when cosine is zero.</exception>
		public double TanScratch(double a)
		{
			double sin = SinScratch(a);
			double cos = CosScratch(a);
			if (cos == 0)
			{
				throw new DivideByZeroException("Cannot divide by zero.");
			}
			return sin / cos;
		}

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
		public double SecScratch(double a)
		{
			double cos = CosScratch(a);
			if (cos == 0)
			{
				throw new DivideByZeroException("Cannot divide by zero.");
			}
			return 1 / cos;
		}

		/// <summary>
		/// Returns the cosecant of an angle in radians.
		/// </summary>
		/// <param name="a">Angle in radians.</param>
		/// <returns>csc(a).</returns>
		public double Csc(double a)
		{
			return 1 / Sin(a);
		}

		/// <summary>
		/// Returns the cosecant of an angle in radians using scratch implementation.
		/// </summary>
		/// <param name="a">Angle in radians.</param>
		/// <returns>csc(a).</returns>
		/// <exception cref="DivideByZeroException">Thrown when sine is zero.</exception>
		public double CscScratch(double a)
		{
			double sin = SinScratch(a);
			if (sin == 0)
			{
				throw new DivideByZeroException("Cannot divide by zero.");
			}
			return 1 / sin;
		}

		/// <summary>
		/// Returns the factorial of a number using a scratch implementation (overload for double).
		/// </summary>
		/// <param name="a">The number.</param>
		/// <returns>a!.</returns>
		/// <exception cref="ArgumentException">Thrown when a is negative.</exception>
		public double FactorialScratch(double a)
		{
			if (a < 0)
			{
				throw new ArgumentException("Cannot take the factorial of a negative number.");
			}
			if (a == 0 || a == 1)
			{
				return 1;
			}
			double result = 1;
			for (int i = 2; i <= a; i++)
			{
				result *= i;
			}
			return result;
		}
	}

}
