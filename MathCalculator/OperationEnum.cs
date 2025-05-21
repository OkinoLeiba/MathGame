using System.ComponentModel;


namespace MathGame
{
	internal struct OperationsEnum
	{
		public enum EnumOperationsMethod : int
		{
			Addition,
			Subtract,
			Multiply,
			Divide,
			Power,
			PowerScratch,
			SquareRoot,
			SquareRootScratch,
			SquareRootBinaryScratch,
			CubeRoot,
			CubeRootScratch,
			Exponential,
			ExponentialScratch,
			LogarithmBase10,
			LogarithmBase10Scratch,
			Factorial,
			FactorialScratch,
			Logarithm,
			LogarithmScratch,
			LogarithmNatural,
			LogarithmNaturalScratch,
			AbsoluteValue,
			AbsoluteValueScratch,
			Modulus,
			ModulusScratch,
			Sin,
			SinScratch,
			Cos,
			CosScratch,
			Tan,
			TanScratch,
			Cot,
			CotScratch,
			Sec,
			SecScratch,
			Csc,
			CscScratch
		}

		// unicode characters will be used to aviod issues and be able to use a variety of characters
		public enum EnumOperationMethodUnicodeSymbol
		{
			u002B,
			u002D,
			//u002A,
			u0078,
			u00F7,
			u1DBA,
			u221A,
			u221B,
			u212F,
			u33D2,
			u33D1,
			u2226,
			u006D,
			mod,
			sin,
			cos,
			tan,
			cot,
			sec,
			csc
			//+,
			//−,
			//×,
			//÷,
			//xⁿ,
			//√x,
			//³√x,
			//eˣ,
			//log10,
			//ln,
			//|x|,
			//mod,
			//sin,
			//cos,
			//tan,
			//cot,
			//sec,
			//csc


		}

		public enum EnumOperationsMethodUnitSymbol
		{
				[Description("+")] Addition,
				[Description("−")] Subtraction,
				[Description("×")] Multiplication,
				[Description("÷")] Division,
				[Description("xⁿ")] Exponentiation,
				[Description("√x")] SquareRoot,
				[Description("³√x")] CubeRoot,
				[Description("eˣ")] Exponential,
				[Description("log10")] LogarithmBase10,
				[Description("ln")] NaturalLogarithm,
				[Description("|x|")] AbsoluteValue,
				[Description("mod")] Modulus,
				[Description("sin")] Sine,
				[Description("cos")] Cosine,
				[Description("tan")] Tangent,
				[Description("cot")] Cotangent,
				[Description("sec")] Secant,
				[Description("csc")] Cosecant
		}


		public enum EnumOperationsMethodPrefix
		{
			A,
			B,
			C,
			D,
			E,
			F,
			G,
			H,
			I,
			J,
			K,
			L,
			M,
			N,
			O,
			P,
			Q,
			R,
			S,
			T,
			U,
			V,
			W,
			X,
			Y,
			Z,
			a,
			b,
			c,
			d,
			e,
			f,
			g,
			h,
			i,
			j,
			k,
			l,
			m,
			n,
			o,
			p,
			q,
		}
	}
}
