using MathGame.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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
		public DateTime? date = default(DateTime?);
		public int score = 0;
		public int correctAnswer = 0;
		public int wrongAnswer = 0;
		public int gameCount = 0;




		public string? Name { get; set; }
		public DateTime? Date { get; set; }
		public int Score { get; set; }
		public int CorrectAnswer { get; set; }
		public int WrongAnswer { get; set; }
		public int GameCount { get => gameCount; set => gameCount = CorrectAnswer + WrongAnswer; } 
		public GameLogger GameLog { get; set; } 

		Operations operations = new Operations();	
		GameLogger gameLogger = new GameLogger();


		public GameIntro() 
		{
			Name = name ?? string.Empty;
			Date = date ?? DateTime.Now;
			Score = score;
			CorrectAnswer = correctAnswer;
			WrongAnswer = wrongAnswer;
			GameCount = gameCount;
			GameLog = gameLogger;
			
		}

		/// <summary>
		/// Greeting the user, prompting the user to provide name, prompting user to select a game, and printing out result
		/// </summary>
		/// <return>void</return>
		public void GameIntroMethod()
		{
			GameSelection gameSelection = new GameSelection();
			do {
				Console.WriteLine("Hello, World!");

				Console.WriteLine("What is your name, Chief...\n");
				Name = Console.ReadLine();

				date = DateTime.UtcNow;

				Console.WriteLine("-----------------------------------------------------------------------");
				Console.WriteLine($"Hello {Name}, the date is {Date}.\nDo you want to play a game with me?");
			} while (string.IsNullOrEmpty(Name));
			gameSelection.GameRequestSelectionUser();
		}

		


		public void GameInputManager(string operation)
		{
			// declaring and initializing string with the default keyword is best practices
			// declaring and initializing numeric variables with default keyword is unnecessary considering the default values
				// done for my awareness and my purposes

			double firstNum = 0.0d;
			//string? firstStringNum = default(string);
			double secondNum = 0.0d;
			//string? secondStringNum = default(string);

			double result = 0.0d;
			double answer = 0.0d;
			string? answerString = default(string);

			Random random = new Random();

			// lazyInitializer is approach to initialize Operations class with minimal memory overhead 
			// to be used throughout the lifecycle of the class
			// the assigned instance is thread-safe and could provide an optimization for concurrent operations
			// of the utility or helper class Operations
			// how critical is the Operations class to my application requiring the consideration
			// of performance and safety and reliability???
			//LazyInitializer.EnsureInitialized(Operations);

			// initial approach to find the method signature, specifically the count of parameters
			//MethodInfo.GetCurrentMethod();
			int numOfParam = typeof(Operations).GetMethod(operation)!.GetParameters().Length;
			bool singleOperation = numOfParam == 1 ? true : false;
			
			// superfluous code took different approach to application design
			// will keep for learning purposes
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


			// generate the random numbers for the questions
			if (singleOperation)
			{
				firstNum = random.Next(0, 99);
			}
			else
			{
				firstNum = random.Next(0, 99);
				secondNum = random.Next(0, 99);
			}


			// ensure that the dividend it greater than divisor and divisor non-zero number
			if (operation.Trim().ToLower() == "division" || operation.Trim().ToLower() == "subtraction")
			{
				firstNum = Math.Max(firstNum, secondNum);
				secondNum = Math.Min(firstNum, secondNum);

				if (secondNum == 0) { secondNum = 1; }

			}

			// access the specific symbol for the mathematical operation
			// the operation as the conditional variable
			var operationSybmol = typeof(OperationsEnum.EnumOperationsMethodUnitSymbol)
				.GetTypeInfo()
				.DeclaredMembers
				//.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase)
				.SingleOrDefault(m => m.Name.Trim().ToLower() == operation.Trim().ToLower())?
				.GetCustomAttributes<DescriptionAttribute>(false)
				.First()
				.Description
				.ToString();

			// provide user feedback concerning the problem to be solved and take input from user
			if (singleOperation) Console.WriteLine($"{operation}: {operationSybmol} - {firstNum}");
			else Console.WriteLine($"{operation}: {firstNum} {operationSybmol} {secondNum}");

			Console.WriteLine("Please provide an answer");
			answerString = Console.ReadLine();
			while (answerString is not null && !Double.TryParse(answerString, out answer))
			{
				if (singleOperation) Console.WriteLine($"{operation}: {operationSybmol} - {firstNum}");
				else Console.WriteLine($"{operation}: {firstNum} {operationSybmol} {secondNum}");
				Console.WriteLine("Please provide an valid answer");
				answerString = Console.ReadLine();
			}
			answer = Convert.ToDouble(answerString);



			// two approaches to invoke methods utilizing reflection and type 
			//result = (double) typeof(Operations).InvokeMember(operation, BindingFlags.InvokeMethod | BindingFlags.Instance, null, Activator.CreateInstance(typeof(Operations)), new object[] { firstNum, secondNum });
			if (singleOperation) result = (double) typeof(Operations).GetMethod(operation)!.Invoke(Activator.CreateInstance(typeof(Operations)), new object[] { firstNum})!;
			else result = (double) typeof(Operations).GetMethod(operation)!.Invoke(Activator.CreateInstance(typeof(Operations)), new object[] { firstNum, secondNum })!;

			GameAnswerManager(result, answer, operation);
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

		public void GameAnswerManager(double result, double answer, string operations)
		{
			GameSelection gameSelection = new GameSelection();
			GameLogManager gameLogManager = new GameLogManager();

			if (result == answer)
			{
				Console.WriteLine("Your answer was correct! Press any key for the next question.");
				Score += 1;
				CorrectAnswer += 1;
				string? _ = Console.ReadLine();
				if (_ is not null) gameSelection.ContinueGameSelectOrEnd(operations, this);
			}
			else
			{
				Console.WriteLine("Your answer was incorrect! Press any key for the next question.");
				WrongAnswer += 1;
				string? _ = Console.ReadLine();
				if (_ is not null) gameSelection.ContinueGameSelectOrEnd(operations, this);
			}

			//gameLogManager.UpdateGameWonAndLoss(operations, new GameLogger
			//{
			//	UserName = this.Name,
			//	Date = (DateTime)this.Date,
			//	Score = this.Score,
			//	CorrectAnswer = this.CorrectAnswer,
			//	WrongAnswer = this.WrongAnswer,
			//	GameCount = (this.CorrectAnswer + this.WrongAnswer)
			//});
			gameLogManager.UpdateGameWonAndLoss(operations, gameLogger);
			gameLogManager.UpdateGameHistory(operations);
		}

		public void GameLogManager()
		{
			GameLogManager gameLogManager = new GameLogManager();

			// create a new game log entry with the current game details
			GameLog = gameLogManager.CreateUpdateGameLog
				(
					this.Name,
					(DateTime)this.Date,
					this.Score,
					this.CorrectAnswer,
					this.WrongAnswer,
					(this.GameCount)
				);
			
			//Console.WriteLine($"Your score is {Score} and you got {CorrectAnswer} correct and {WrongAnswer} wrong. Press any key to continue...");
			//Console.ReadKey();
		}

		//TODO: In Development - manage continuation of game based on conditions of max 10 games and user interrupt
		//public bool ContinueGame()
		//{
		//	const int gameCount = 10;
		//	for (int i = 0; i < gameCount; i++)
		//	{
		//		gameSelection.GameRequestSelectionUser();

		//		if (i == 10) { Console.WriteLine($"Game over, your Great!!!. Your final score is {gameIntro.Score}"); Console.ReadLine(); }

		//	}
		//}



	}
}
