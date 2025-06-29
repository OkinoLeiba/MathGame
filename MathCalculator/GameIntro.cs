using MathGame.Model;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
		public int result = 0;
		public int answer = 0;


		public string? Name { get; set; }
		public DateTime? Date { get; set; }
		public int Score { get; set; }
		public int CorrectAnswer { get; set; }
		public int WrongAnswer { get; set; }
		public int GameCount { get => gameCount; set => gameCount = CorrectAnswer + WrongAnswer; } 
		public GameLogger GameLog { get; set; } 

		// Class containing game methods or mathematical operations to be used in the game selection process
		Operations operations = new Operations();
		// Class containing the model for game logger and game history
		GameLogger gameLogger = new GameLogger();

		 
		public GameIntro() {
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

			DateTime date = DateTime.UtcNow;

			Console.WriteLine("-----------------------------------------------------------------------");
			Console.WriteLine($"Hello {Name}, the date is {date}.\nDo you want to play a game with me?");
			} while (string.IsNullOrEmpty(Name));
			gameSelection.GameRequestSelectionUser();
		}

	

		public void GameInputManager(string operation)
		{
			int firstNum = default(int);
			string? firstStringNum = default(string);
			int secondNum = default(int);
			string? secondStringNum = default(string);

			// since operations are critical to the game, we will ensure that the Operations class is initialized
			// and that the methods are available for use in the game selection process without having to create an instance of the class
			// LazyInitializer is used to ensure that the Operations class is initialized only once and is thread-safe
			// LazyInitializer.EnsureInitialized<Operations>(ref operations);
			//LazyInitializer.EnsureInitialized(Operations);

			int numOfParam = typeof(Operations).GetMethod(operation)!.GetParameters().Length;

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

				var result = typeof(Operations).GetMethod(operation)!.Invoke(this.GetType(), new object[] { firstNum });
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

				var result = typeof(Operations).GetMethod(operation)!.Invoke(this.GetType(), new object[] { firstNum, secondNum });

			}
			else
			{
				Console.WriteLine("No valid numbers were provided!");
			}
		




			// two approaches to invoke methods utilizing reflection and type 
			typeof(Operations).InvokeMember(operation, BindingFlags.InvokeMethod | BindingFlags.Instance, null, Activator.CreateInstance(typeof(Operations)), new object[] { firstNum, secondNum });
				
			typeof(Operations).GetMethod(operation)!.Invoke(Activator.CreateInstance(typeof(Operations)), new object[] { firstNum, secondNum });
		

			
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
