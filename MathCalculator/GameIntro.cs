using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static MathGame.OperationsEnum;

namespace MathGame
{
	/// <summary>
	/// Beginning of program after main entry point and initialized
	/// </summary>
	internal class GameIntro
	{
		public string? name = default(string);
		public int score = default(int);


		public string? Name { get; set; }
		
		public int Score { get; set; }

		Operations operations = new Operations();
		GameSelection gameSelection = new GameSelection();	


		public GameIntro() {
			Name = name ?? string.Empty;
			
		}

		/// <summary>
		/// Greeting the user, prompting the user to provide name, prompting user to select a game, and printing out result
		/// </summary>
		/// <return>void</return>
		public void GameIntroMethod()
		{
			do {
				Console.WriteLine("Hello, World!");

				Console.WriteLine("What is your name, Chief...\n");
				Name = Console.ReadLine();

				DateTime date = DateTime.UtcNow;

				Console.WriteLine("-----------------------------------------------------------------------");
				Console.WriteLine($"Hello {Name}, the date is {date}.\nDo you want to play a game with me?");
			} while (string.IsNullOrEmpty(Name));
			gameSelection.GameRequestSelectionUser();
		}

		


		public void GameInputManager(string operation)
		{
			// declaring and initializing string with the default keyword is best practices
			// declaring and initializing numeric variables is unnecessary considering the default values
				// done for my awareness and purposes

			double firstNum = 0.0d;
			//string? firstStringNum = default(string);
			double secondNum = 0.0d;
			//string? secondStringNum = default(string);

			double result = 0.0d;
			double answer = 0.0d;
			string? answerString = default(string);

			Random random = new Random();

			// approach to initialize Operations class with minimal memory overhead 
			// to be used throughout the lifecycle of the class
			// the assigned instance is thread-safe and could provide an optimization through concurrent operations
			// of the utility or helper class Operations
			// how critical is the Operations class to my application requiring consideration
			// of performance and safety and reliability 
			//LazyInitializer.EnsureInitialized(Operations);

			// initial approach to find the method signature, specifically the count of parameters
			//MethodInfo.GetCurrentMethod();
			int numOfParam = typeof(Operations).GetMethod(operation)!.GetParameters().Length;

			//if (numOfParam == 1)
			//{
			//	try
			//	{
			//		do
			//		{
			//			Console.WriteLine("Enter a number");
			//			firstNum = Convert.ToDouble(Console.ReadLine());
			//		} while (Double.TryParse(Console.ReadLine(), out firstNum));

			//	}
			//	catch (NullReferenceException nfe)
			//	{
			//		Console.WriteLine(nfe.Message);
			//	}

			//	typeof(Operations).GetMethod(operation)!.Invoke(this.GetType(), new object[] { firstNum });
			//}
			//else if (numOfParam == 2) 
			//{
			//	try
			//	{
			//		do
			//		{
			//			Console.WriteLine("Enter first number");
			//			firstStringNum = Console.ReadLine();
			//		} while (firstStringNum is null && !Double.TryParse(firstStringNum, out firstNum));
			//		firstNum = Convert.ToDouble(firstStringNum);

			//		do
			//		{
			//			Console.WriteLine("Enter second number");
			//			secondStringNum = Console.ReadLine();
			//		} while (secondStringNum is null && !Double.TryParse(secondStringNum, out secondNum));
			//		secondNum = Convert.ToDouble(secondStringNum);
			//	}
			//	catch (NullReferenceException nfe)
			//	{
			//		Console.WriteLine(nfe.Message);
			//	}


			// // access the specific symbol for the mathematical operation
			// // the operation as the conditional variable
			//	var operationSybmol = typeof(EnumOperationsMethodUnitSymbol)
			//		.GetTypeInfo()
			//		.DeclaredMembers
			//		//.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase)
			//		.SingleOrDefault(m => m.Name.Trim().ToLower() == operation.Trim().ToLower())?
			//		.GetCustomAttributes<DescriptionAttribute>(false)
			//		.First()
			//		.Description
			//		.ToString();


			// provide user feedback concerning the problem to be solved and take input from user
			//	Console.WriteLine($"{operation}: {firstNum} {operationSybmol} {secondNum}");
			//	Console.WriteLine("Please provide an answer");
			//	answerString = Console.ReadLine();
			//	while (answerString is null && !Double.TryParse(answerString, out answer))
			//	{
			//		Console.WriteLine("Please provide an valid answer");
			//		answerString = Console.ReadLine();
			//	}
			//	answer = Convert.ToDouble(answerString);



			//	// two approaches to invoke methods utilizing reflection and type 
			//	//result = (double) typeof(Operations).InvokeMember(operation, BindingFlags.InvokeMethod | BindingFlags.Instance, null, Activator.CreateInstance(typeof(Operations)), new object[] { firstNum, secondNum });
			//	result = (double) typeof(Operations).GetMethod(operation)!.Invoke(Activator.CreateInstance(typeof(Operations)), new object[] { firstNum, secondNum });
			//}
			//else
			//{
			//	Console.WriteLine("No valid numbers were provided!");
			//}


			// generate the numbers for the questions
			if (numOfParam == 1)
			{
				firstNum = random.Next(0, 99);
			}
			if (numOfParam == 2)
			{
				firstNum = random.Next(0, 99);
				secondNum = random.Next(0, 99); 
			}


			// ensure that the dividend it greater than divisor and divisor non-zero number
			if (operation.Trim().ToLower() == "divide")
			{
				firstNum = Math.Max(firstNum, secondNum);
				secondNum = Math.Min(firstNum, secondNum);

				if (secondNum == 0) { secondNum = 1; }

			}

			// access the specific symbol for the mathematical operation
			// the operation as the conditional variable
			var operationSybmol = typeof(EnumOperationsMethodUnitSymbol)
				.GetTypeInfo()
				.DeclaredMembers
				//.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase)
				.SingleOrDefault(m => m.Name.Trim().ToLower() == operation.Trim().ToLower())?
				.GetCustomAttributes<DescriptionAttribute>(false)
				.First()
				.Description
				.ToString();

			// provide user feedback concerning the problem to be solved and take input from user
			Console.WriteLine($"{operation}: {firstNum} {operationSybmol} {secondNum}");
			Console.WriteLine("Please provide an answer");
			answerString = Console.ReadLine();
			while (answerString is null && !Double.TryParse(answerString, out answer))
			{
				Console.WriteLine("Please provide an valid answer");
				answerString = Console.ReadLine();
			}
			answer = Convert.ToDouble(answerString);



			// two approaches to invoke methods utilizing reflection and type 
			//result = (double) typeof(Operations).InvokeMember(operation, BindingFlags.InvokeMethod | BindingFlags.Instance, null, Activator.CreateInstance(typeof(Operations)), new object[] { firstNum, secondNum });
			result = (double)typeof(Operations).GetMethod(operation)!.Invoke(Activator.CreateInstance(typeof(Operations)), new object[] { firstNum, secondNum });

			GameAnswerManager(result, answer);
		}
		// extension method to recover the enum attributes
		// workout to declare enum with strings and access strings
		// creating it as an generic may be an issue
		//public string? ToEnumMember<T>(this T value) where T : Enum
		//{
		//	return typeof(T)
		//		.GetTypeInfo()
		//		.DeclaredMembers
		//		.SingleOrDefault(x => x.Name == value.ToString())?
		//		.GetCustomAttribute<EnumMemberAttribute>(false)?
		//		.Value;
		//}

		public void GameAnswerManager(double result, double answer)
		{
			if (result == answer)
			{
				Console.WriteLine("Your answer was correct! Press any key for the next question.");
				Score++;
				Console.ReadLine();
			}
			else
			{
				Console.WriteLine("Your answer was incorrect! Press any key for the next question.");
				Console.ReadLine();
			}
		}


	}
}
