using MathGame.Model;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using static MathGame.OperationsEnum;


// keep the instance tracker even though there is another version in the GameIntro class
// both will perform the same function, but this one is a struct and the other is a class
// the class also creates a new instance of GameLogger, and assigns it to the GameLog property
// kept for experimentation and learning purposes
namespace MathGame
{
	/// <summary>
	/// Beginning of program after main entry point and initialized
	/// </summary>
	public class GameIntro
	{
		public string? name = default(string);
		public DateTime? date = default(DateTime?);
		static public int score = 0;
		static public int correctAnswer = 0;
		static public int wrongAnswer = 0;
		public int gameCount = 0;
		public int result = 0;
		public int answer = 0;


		public string? Name { get; set; }
		public DateTime? Date { get; set; }
		static public int Score { get; set; }
		static public int CorrectAnswer { get; set; }
		static public int WrongAnswer { get; set; }
		public int GameCount { get => gameCount; set => gameCount = CorrectAnswer + WrongAnswer; } 
		public GameLogger GameLog { get; set; } 

		// class containing game methods or mathematical operations to be used in the game selection process
		Operations operations = new Operations();
		// class containing the model for game logger and game history
		GameLogger gameLogger = new GameLogger();

		private static readonly List<GameIntro> _instances = new List<GameIntro>();

		public GameIntro() {
			Name = name ?? string.Empty;
			Date = date ?? DateTime.Now;
			GameLog = gameLogger;

			_instances.Add(this);

		}


		//public GameIntro() : GameLogger(string.Empty, DateTime.Now, 0, 0, 0, 0, new GameLogger())
		//{
		//	// this constructor initializes the properties with default values for game logger
		//	Name = string.Empty;
		//	Date = DateTime.Now;
		//	Score = 0;
		//	CorrectAnswer = 0;
		//	WrongAnswer = 0;
		//	GameCount = 0;
		//	GameLog = new GameLogger();
		//	_instances.Add(this);
		//}
	
			

		public static IReadOnlyList<GameIntro> Instances => _instances.AsReadOnly();


		/// <summary>
		/// Greeting the user, prompting the user to provide name, prompting user to select a game, and printing out result
		/// </summary>
		/// <return>void</return>
		public void GameIntroMethod()
		{
			GameSelection gameSelection = new GameSelection();
			do { 
			Console.WriteLine("Hello, All World!");

				Console.WriteLine("What is your name, Chief...\n");
				Name = Console.ReadLine();

				//Name = AnsiConsole.Prompt(
				//	new TextPrompt<string>("What is your name, Chief...\n")
				//		.Validate(input => string.IsNullOrEmpty(input) ? ValidationResult.Error("Name cannot be empty") : ValidationResult.Success())
				//		.PromptStyle("green"));

			Date = DateTime.UtcNow;

			Console.WriteLine("-----------------------------------------------------------------------");
			
			} while (string.IsNullOrEmpty(Name));

			Console.WriteLine($"Hello {Name}, the date is {Date}.\nDo you want to play a game with me?");
			gameSelection.GameRequestSelectionUser();
		}


		//TODO: In Development - manage game selection process and invoke methods based on user input
		//TODO: Try to remember why I created this method and what it is supposed to do
		public void GameInputManager(string operation)
		{
			int firstNum = default(int);
			string? firstStringNum = default(string);
			int secondNum = default(int);
			string? secondStringNum = default(string);

			int numOfParam = 0; // number of parameters for the operation method

			// since operations are critical to the game, we will ensure that the Operations class is initialized
			// and that the methods are available for use in the game selection process without having to create an instance of the class
			// LazyInitializer is used to ensure that the Operations class is initialized only once and is thread-safe
			//System.Threading.LazyInitializer.EnsureInitialized<Operations>(ref operations);
			//System.Threading.LazyInitializer.EnsureInitialized(Operations);

			try
			{
				numOfParam = typeof(Operations).GetMethod(char.ToUpper(operation[0]) + operation.Substring(1))!.GetParameters().Length;
			}
			catch (NullReferenceException nfe) { Console.WriteLine(nfe.Message); return; } // exit if the method is not found 


			if (numOfParam == 1)
			{
				try
				{
					do
					{
						Console.WriteLine("Enter a number");
						firstNum = Convert.ToInt16(Console.ReadLine());
					} while (int.TryParse(Console.ReadLine(), out firstNum));

				}
				catch (NullReferenceException nfe)
				{
					Console.WriteLine(nfe.Message);
				}

				try { 
					var result = (double) typeof(Operations).GetMethod(char.ToUpper(operation[0]) + operation.Substring(1))?.Invoke(this.GetType(), new object[] { firstNum })!;
				}
				catch (NullReferenceException nfe) { Console.WriteLine(nfe.Message); return; } // exit if the method is not found 
			}
			else if (numOfParam == 2) 
			{
				try
				{
					do
					{
						Console.WriteLine("Enter first number");
						firstStringNum = Console.ReadLine();
					} while (firstStringNum is null && !int.TryParse(firstStringNum, out firstNum));
					firstNum = Convert.ToInt16(firstStringNum);

					do
					{
						Console.WriteLine("Enter second number");
						secondStringNum = Console.ReadLine();
					} while (firstStringNum is null && !int.TryParse(secondStringNum, out secondNum));
					secondNum = Convert.ToInt16(secondStringNum);
				}
				catch (NullReferenceException nfe)
				{
					Console.WriteLine(nfe.Message);
				}
				
				try { 
					var result = (double) typeof(Operations).GetMethod(char.ToUpper(operation[0]) + operation.Substring(1))?.Invoke(typeof(Operations), new object[] { firstNum, secondNum })!;
				}
				catch (NullReferenceException nfe) { Console.WriteLine(nfe.Message); return; } // exit if the method is not found 

			}
			else
			{
				Console.WriteLine("No valid numbers were provided!");
			}
		




			// two approaches to invoke methods utilizing reflection and type 
			try {
				var _ = (double) typeof(Operations).InvokeMember(char.ToUpper(operation[0]) + operation.Substring(1), BindingFlags.InvokeMethod | BindingFlags.Instance, null, Activator.CreateInstance(typeof(Operations)), new object[] { firstNum, secondNum })!;
			}
			catch (NullReferenceException nfe) { Console.WriteLine(nfe.Message); return; } // exit if the method is not found 

			try {
				var _ = (double) typeof(Operations).GetMethod(char.ToUpper(operation[0]) + operation.Substring(1))?.Invoke(Activator.CreateInstance(typeof(Operations)), new object[] { firstNum, secondNum })!;
			}
			catch (NullReferenceException nfe) { Console.WriteLine(nfe.Message); return; } // exit if the method is not found 



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
