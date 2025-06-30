using Spectre.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization; // may be needed for enums
using System.Text;
using System.Threading;
using System.Threading.Tasks; // may be needed for Operations class
using static MathGame.OperationsEnum;

using Timer = System.Timers.Timer;

namespace MathGame
{
	public class GameSelection
	{
		
		public List<string> methodNames = default(List<string>)!;
		public string? gameSelect = default(string)!;
		public int inputRepeatValidationSignal = default(int);

		public string? GameSelect { get; set; }
		public List<string> MethodNames { get; set; }

		Timer timer = new Timer();

		// the classes may be initialized within this function because of my concern 
		// with creating a invocation loop
		GameIntro gameIntro = new GameIntro();
		QuestionGenerator questionGenerator = new QuestionGenerator();


		public GameSelection() 
		{
			GameSelect = gameSelect ?? string.Empty;
			// get method names from Operations class as game options for user
			MethodNames = typeof(Operations)
				.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly)
				.Select(m => m.Name)
				.Distinct()
				.ToList();

			//attempted to cast list<T> to enum
			//Enum.Parse(typeof(List<string>), methodNames.First());
			//enum EnumMethodName = methodNames.Select(s => Enum.Parse(typeof(Enum), s)).ToList();

			// attempted to dynamically add elements to enum or of type enum at runtime
			// one approach is to keep reinitializing the enum
			//methodNames.Select(s =>  EnumMethodName s = EnumMethodName(0)); 
		}

		/// <summary>
		/// Manage the selection of the game by user
		/// </summary>
		/// <return>void</return>
		public void GameRequestSelectionUser()
		{

			GameLogManager gameLogManager = new GameLogManager();

			if (inputRepeatValidationSignal != 3)
			{
				Console.WriteLine("What game would you like to play today with me?\n");
			}
			else
			{
				Console.WriteLine("""
					Please input a valid game option from the list below.
					Example 'A' or 'Add'

					""");
			}
			for (int i = 0, c = 0; i < MethodNames.Count; i++)
			{
				string? col = default(string);
				if (c == 24) c += 8;
				//TODO: create two columns 05/20/2025
				if (i == Math.Abs(MethodNames.Count / 2)) col = "10";
				Console.WriteLine($"{(char)('A' + c++)} - {MethodNames[i]}{col}");

			}



			Console.WriteLine("Prev - Previous Game History");
			Console.WriteLine("Q - Exit");
			Console.WriteLine("-----------------------------------------------------------------------");

			// ask for the user's game selection using Spectre.Console for better UI experience
			var gameSelection = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title("What game would you like to [green]play [/]?")
					.PageSize(10)
					.MoreChoicesText("[grey](Move up and down to reveal more games)[/]")
					.AddChoices(
						MethodNames
					));


			// render each item in list on separate line
			AnsiConsole.Write(new Columns(MethodNames));

			var table = new Table().Centered();

			// print game options in columns with 10 items per column
			int itemsPerColumn = 10;
			int totalItems = MethodNames.Count;
			int numColumns = (int)Math.Ceiling(totalItems / (double)itemsPerColumn);

			for (int row = 0; row < itemsPerColumn; row++)
			{
				for (int col = 0; col < numColumns; col++)
				{
					// calculate the index for the current item in the column
					// from multi-dimensional array index to single dimensional array index
					int index = col * itemsPerColumn + row;
					if (index < totalItems)
					{
						// char optionChar = (char)('A' + index);
						// correctly retrieve the enum value using Enum.GetName
						string optionChar = Enum.GetName(typeof(EnumOperationsMethodPrefix), index % Enum.GetNames(typeof(EnumOperationsMethodPrefix)).Length)!;  // use modulo to wrap around if needed
						Console.Write($"{optionChar} - {MethodNames[index],-25}");
					}
				}
				Console.WriteLine();
			}

			// create a Spectre.Console table with 10 items per column
			// add columns to the table
			for (int col = 0; col < numColumns; col++)
			{
				table.AddColumn(new TableColumn($"Game Option {col + 1}"));
			}

			// add rows to the table
			for (int row = 0; row < itemsPerColumn; row++)
			{
				var rowItems = new List<string>();
				for (int col = 0; col < numColumns; col++)
				{
					int index = col * itemsPerColumn + row;
					if (index < totalItems)
					{
						// char optionChar = (char)('A' + index);
						string optionChar = Enum.GetName(typeof(EnumOperationsMethodPrefix), index % Enum.GetNames(typeof(EnumOperationsMethodPrefix)).Length)!; // use modulo to wrap around if needed
						rowItems.Add($"{optionChar} - {MethodNames[index]}");
					}
					else
					{
						rowItems.Add(""); // empty cell if no more items
					}
				}
				table.AddRow(rowItems.ToArray());
			}

			AnsiConsole.Live(table)
				.AutoClear(false)   // do not remove when done
				.Overflow(VerticalOverflow.Ellipsis) // show ellipsis when overflowing
				.Cropping(VerticalOverflowCropping.Top) // crop overflow at top
				.Start(ctx =>
				{
					//table.AddColumn(new TableColumn("Game Options").Centered()); // add a column for game options
					//table.AddColumn(new TableColumn(MethodNames));
					ctx.Refresh();
					Thread.Sleep(1000);
				});

			GameSelect = gameSelection;

			// GameSelect = Console.ReadLine()!;

			// convert unicode characters from enum into symbols that define mathematical operation
			// took different approach to getting this value or sybmol 
			var unicodeTest = Encoding.Unicode.GetString(Encoding.Unicode.GetBytes("\u002B"));

			// validate the game select to make sure the value is not null, not a number, and a member of the list of games
			bool validInputGame = !string.IsNullOrEmpty(GameSelect)
			&& !int.TryParse(GameSelect, out int result)
			&& (Enum.GetNames(typeof(OperationsEnum.EnumOperationsMethod)).Contains(GameSelect)
			|| Enum.GetNames(typeof(OperationsEnum.EnumOperationsMethodPrefix)).Contains(GameSelect));

			// recursively validate the game selection input
			if (!validInputGame) { inputRepeatValidationSignal++; GameRequestSelectionUser(); }

			// manage game selection
			try
			{
				if (GameSelect.Count() == 1 && string.IsNullOrEmpty(GameSelect))
				{

					int selectCount = 0;
					foreach (var selectedGame in Enum.GetValues(typeof(OperationsEnum.EnumOperationsMethodPrefix)))
					{
						string gameName = ((OperationsEnum.EnumOperationsMethod)selectCount++).ToString();
						if (GameSelect?.ToLower() == selectedGame.ToString()!.ToLower())
						{
							// different approach to retrieve string from enum via the cast of int to string literal
							//Enum.GetName(typeof(OperationEnum.EnumOperationMethod), (int) game).ToString();
							
							Console.WriteLine($"The {gameName} game was selected.");
							GameSelect = gameName;
							
							// prevent the execution of the conditional body when function stack unravels 
							// to location where it jumped from -- solution?: move invocation of function
							//GameSelect = null;
							//break;
							//return;
							
						}
						

					}
				}
				else if (GameSelect.Count() > 1)
				{
					foreach (var selectedGame in Enum.GetNames(typeof(OperationsEnum.EnumOperationsMethod)))
					{
						if (GameSelect.ToLower() == selectedGame.ToString().ToLower()) Console.WriteLine($"The {selectedGame} game was selected.");
						
					}
				}
				else if (GameSelect.Trim().ToLower() == "prev" || GameSelect.Trim().ToLower() == "previous" || GameSelect.Trim().ToLower() == "game history")
				{
					foreach (var history in gameLogManager.GameHistory)
					{
						Console.WriteLine(history);
					}
					Console.WriteLine("-----------------------------------------------------------------------");
					Console.WriteLine("Do you want to Exit or Return to Game Selection?");
					string? optionReturnOrExit = Console.ReadLine();

					if (optionReturnOrExit?.Trim() == null) { Console.WriteLine(optionReturnOrExit); }
					else if (optionReturnOrExit.Trim().ToLower() == "exit" || optionReturnOrExit.Trim().ToLower() == "e" || optionReturnOrExit.Trim().ToLower() == "close")
					{
						Console.WriteLine("Bye for Now!");
						timer.Interval = 10000;
						timer.Stop();
						timer.Dispose();
						Environment.Exit(1);
					}
					else if (optionReturnOrExit.Trim().ToLower() == "continue" || optionReturnOrExit.Trim().ToLower() == "return")
					{
						gameIntro.GameIntroMethod();
					}
					else
					{
						Console.WriteLine("Invalid selection, returning to game selection.");
						gameIntro.GameIntroMethod();
					}
				}
				else if (GameSelect.Trim().ToLower() == "q" || GameSelect.Trim().ToLower() == "exit" || GameSelect.Trim().ToLower() == "close")
				{
					Console.WriteLine("Bye for Now!");
					timer.Interval = 10000;
					timer.Stop();
					timer.Dispose();
		
					Environment.Exit(1);
				}
				else if (GameSelect.Trim().ToLower() == "restart" || GameSelect.Trim().ToLower() == "r")
				{
					gameIntro.GameIntroMethod();
				}
				// is this necessary for default behavior if the other conditional statements are not met??
				else if (GameSelect.Trim().ToLower() == "exit")
				{
					Console.WriteLine("Bye for Now!");

					timer.Interval = 10000;
					timer.Stop();
					timer.Dispose();

					Environment.Exit(1);
				}
				else
				{
					Console.WriteLine("No game was selected!\nDo you want to Exit or Restart?");
					string? optionRestartorExit = Console.ReadLine();

					if (optionRestartorExit?.Trim() == null) { Console.WriteLine(optionRestartorExit); }
					else if (optionRestartorExit.Trim().ToLower() == "exit"
						|| optionRestartorExit.Trim().ToLower() == "e"
						|| optionRestartorExit.Trim().ToLower() == "close")
					{
						timer.Interval = 10000;
						timer.Stop();
						timer.Dispose();
						Environment.Exit(1);
					}
					else { gameIntro.GameIntroMethod(); }

				}
			}
			catch (NullReferenceException nfe)
			{
				Console.WriteLine(nfe.Message);
			}

			
			questionGenerator.MathQuestion(GameSelect.Trim().ToLower());

			// generate a list of characters 
			// exclude ascii characters after 5A
			//var charList = Enumerable.Range(0, 60)
			//	.Where(i => i < 20 || i > 20)
			//	.Select(i => (char)('A' + i))
			//	.ToList();


		}


		/// <summary>
		/// Manage the selection of the game by user using simple if and switch statements
		/// </summary>
		/// <return>void</return>
		public void SimpleGameRequestSelectionUser()
		{
			if (inputRepeatValidationSignal != 3)
			{
				Console.WriteLine("What game would you like to play today with me?\n");
			}
			else
			{
				Console.WriteLine("""
					Please input a valid game option from the list below.
					Example 'A' or 'Add'

					""");
			}


			// series of if statement to print out message to user concerning the selection of game
			if (GameSelect.Trim().ToLower() == "addition" || GameSelect.Trim().ToLower() == "a")
			{
				Console.WriteLine("The addition game was selected.");
			}
			else if (GameSelect.Trim().ToLower() == "subtract" || GameSelect.Trim().ToLower() == "b")
			{
				Console.WriteLine("The subtraction game was selected.");
			}
			else if (GameSelect.Trim().ToLower() == "multiply" || GameSelect.Trim().ToLower() == "c")
			{
				Console.WriteLine("The multiplication game was selected.");
			}
			else if (GameSelect.Trim().ToLower() == "divide" || GameSelect.Trim().ToLower() == "d")
			{
				Console.WriteLine("The division game was selected.");
			}
			else if (GameSelect.Trim().ToLower() == "power" || GameSelect.Trim().ToLower() == "e")
			{
				Console.WriteLine("The exponentiation game was selected.");
			}
			else if (GameSelect.Trim().ToLower() == "powerscratch" || GameSelect.Trim().ToLower() == "f")
			{
				Console.WriteLine("The power (function implemented from scratch) game was selected.");
			}
			else if (GameSelect.Trim().ToLower() == "squareroot" || GameSelect.Trim().ToLower() == "g")
			{
				Console.WriteLine("The square root game was selected.");
			}
			else if (GameSelect.Trim().ToLower() == "squarerootscratch" || GameSelect.Trim().ToLower() == "h")
			{
				Console.WriteLine("The square root (function implemented from scratch) game was selected.");
			}
			else if (GameSelect.Trim().ToLower() == "squarerootbinaryscratch" || GameSelect.Trim().ToLower() == "i")
			{
				Console.WriteLine("The square root binary (function implemented from scratch) game was selected.");
			}
			else if (GameSelect.Trim().ToLower() == "cuberoot" || GameSelect.Trim().ToLower() == "j")
			{
				Console.WriteLine("The cube root game was selected.");
			}
			else if (GameSelect.Trim().ToLower() == "cuberootscratch" || GameSelect.Trim().ToLower() == "k")
			{
				Console.WriteLine("The cube root (function implemented from scratch) game was selected.");
			}
			else if (GameSelect.Trim().ToLower() == "exponential" || GameSelect.Trim().ToLower() == "l")
			{
				Console.WriteLine("The exponential game was selected.");
			}
			else if (GameSelect.Trim().ToLower() == "exponentialscratch" || GameSelect.Trim().ToLower() == "m")
			{
				Console.WriteLine("The exponential (function implemented from scratch) game was selected.");
			}
			else if (GameSelect.Trim().ToLower() == "logarithmbase10" || GameSelect.Trim().ToLower() == "n")
			{
				Console.WriteLine("The base-10 logarithm game was selected.");
			}
			else if (GameSelect.Trim().ToLower() == "logarithmbase10scratch" || GameSelect.Trim().ToLower() == "o")
			{
				Console.WriteLine("The base-10 logarithm (function implemented from scratch) game was selected.");
			}
			else if (GameSelect.Trim().ToLower() == "factorial" || GameSelect.Trim().ToLower() == "p")
			{
				Console.WriteLine("The factorial game was selected.");
			}
			else if (GameSelect.Trim().ToLower() == "factorialscratch" || GameSelect.Trim().ToLower() == "q")
			{
				Console.WriteLine("The factorial (function implemented from scratch) game was selected.");
			}
			else if (GameSelect.Trim().ToLower() == "logarithm" || GameSelect.Trim().ToLower() == "r")
			{
				Console.WriteLine("The logarithm game was selected.");
			}
			else if (GameSelect.Trim().ToLower() == "exit")
			{
				Environment.Exit(1);
			}
			else
			{
				Console.WriteLine("Invalid selection.");
			}

			// same series conditional statements as switch statements
			switch (GameSelect.Trim().ToLower())
			{
				case "addition":
				case "a":
					Console.WriteLine("The addition game was selected.");
					break;

				case "subtract":
				case "b":
					Console.WriteLine("The subtraction game was selected.");
					break;

				case "multiply":
				case "c":
					Console.WriteLine("The multiplication game was selected.");
					break;

				case "divide":
				case "d":
					Console.WriteLine("The division game was selected.");
					break;

				case "power":
				case "e":
					Console.WriteLine("The exponentiation game was selected.");
					break;

				case "powerscratch":
				case "f":
					Console.WriteLine("The power (function implemented from scratch) game was selected.");
					break;

				case "squareroot":
				case "g":
					Console.WriteLine("The square root game was selected.");
					break;

				case "squarerootscratch":
				case "h":
					Console.WriteLine("The square root (function implemented from scratch) game was selected.");
					break;

				case "squarerootbinaryscratch":
				case "i":
					Console.WriteLine("The square root binary (function implemented from scratch) game was selected.");
					break;

				case "cuberoot":
				case "j":
					Console.WriteLine("The cube root game was selected.");
					break;

				case "cuberootscratch":
				case "k":
					Console.WriteLine("The cube root (function implemented from scratch) game was selected.");
					break;

				case "exponential":
				case "l":
					Console.WriteLine("The exponential game was selected.");
					break;

				case "exponentialscratch":
				case "m":
					Console.WriteLine("The exponential (function implemented from scratch) game was selected.");
					break;

				case "logarithmbase10":
				case "n":
					Console.WriteLine("The base-10 logarithm game was selected.");
					break;

				case "logarithmbase10scratch":
				case "o":
					Console.WriteLine("The base-10 logarithm (function implemented from scratch) game was selected.");
					break;

				case "factorial":
				case "p":
					Console.WriteLine("The factorial game was selected.");
					break;

				case "factorialscratch":
				case "q":
					Console.WriteLine("The factorial (function implemented from scratch) game was selected.");
					break;

				case "logarithm":
				case "r":
					Console.WriteLine("The logarithm game was selected.");
					break;
				case "exit":
					Environment.Exit(1);
					break;
				default:
					Console.WriteLine("Invalid selection.");
					break;
			}

			Console.WriteLine(GameSelect.Trim().ToLower() switch
			{
				"addition" or "a" => "The addition game was selected.",
				"subtract" or "b" => "The subtraction game was selected.",
				"multiply" or "c" => "The multiplication game was selected.",
				"divide" or "d" => "The division game was selected.",
				"power" or "e" => "The exponentiation game was selected.",
				"powerscratch" or "f" => "The power (function implemented from scratch) game was selected.",
				"squareroot" or "g" => "The square root game was selected.",
				"squarerootscratch" or "h" => "The square root (function implemented from scratch) game was selected.",
				"squarerootbinaryscratch" or "i" => "The square root binary (function implemented from scratch) game was selected.",
				"cuberoot" or "j" => "The cube root game was selected.",
				"cuberootscratch" or "k" => "The cube root (function implemented from scratch) game was selected.",
				"exponential" or "l" => "The exponential game was selected.",
				"exponentialscratch" or "m" => "The exponential (function implemented from scratch) game was selected.",
				"logarithmbase10" or "n" => "The base-10 logarithm game was selected.",
				"logarithmbase10scratch" or "o" => "The base-10 logarithm (function implemented from scratch) game was selected.",
				"factorial" or "p" => "The factorial game was selected.",
				"factorialscratch" or "q" => "The factorial (function implemented from scratch) game was selected.",
				"logarithm" or "r" => "The logarithm game was selected.",
				"exit" => new Action(() => Environment.Exit(1)),
				_ => "Invalid selection."
			});



			Console.WriteLine("-----------------------------------------------------------------------");
		}

		//TODO: In Development - generate random selection of game
		public string RandomGameSelect()
		{
			Random random = new Random();
			CollectionsMarshal.AsSpan(MethodNames);

			// default value provide to user if null 
			// log if default value or behavior occurs within a threshold
			//TODO: create corrective behavior to manage occurrence of anomalous behavior
			return random.GetItems((ReadOnlySpan<string>) CollectionsMarshal.AsSpan<string>(MethodNames), MethodNames.Count).First()!;
			
			
		}

		public void ContinueGameSelectOrEnd(string gameName)
		{
			// TODO: refactor the code to use a switch expression or a switch statement
			// TODO: refactor the code to use a ternary operator or a conditional operator
			// TODO: refactor the code to use a lambda expression or a delegate
			// TODO: change GAMECOUNT to a constant value of 10
			GameSelect = gameName;
			const int GAMECOUNT = 10;
			int gameTotal = GameIntro.CorrectAnswer + GameIntro.WrongAnswer;

			string gameFinalFeedback = ((double) GameIntro.Score / (double) gameTotal) switch
			{
				1.0 => $"\nGame over, your Great!!! You are super-awesome. Your final score is {GameIntro.Score}.\n",
				>= 0.8 => $"\nGame over, your Great!!! You did well. Your final score is {GameIntro.Score}.\n",
				>= 0.5 => $"\nGame over, your Great!!! You did alright. Your final score is {GameIntro.Score}.\n",
				>= 0.0 => $"\nGame over, your Great!!! You did it, keep trying. Your final score is {GameIntro.Score}.\n",
				_ => "\nKeep trying, you get it!\n"
			};


			if (gameTotal >= GAMECOUNT) Console.WriteLine(gameFinalFeedback);
			else questionGenerator.MathQuestion(GameSelect);

			
			if (gameTotal >= GAMECOUNT)
			{
				Console.WriteLine("Do you want to continue playing or exit? (Type 'continue' or 'exit')");
				string? optionContinueOrExit = Console.ReadLine()?.Trim().ToLower();
				if (optionContinueOrExit == "continue" || optionContinueOrExit == "c")
				{
					gameIntro.GameIntroMethod();
				}
				else if (optionContinueOrExit == "exit" || optionContinueOrExit == "e")
				{
					Console.WriteLine("Bye for Now!");
					timer.Interval = 10000;
					timer.Stop();
					timer.Dispose();
					Environment.Exit(1);
				}
				else
				{
					Console.WriteLine("Invalid selection, returning to game selection.");
					gameIntro.GameIntroMethod();
				}
			}


		}
	}
}

/// <summary>
/// Manage the selection of the game by user
/// </summary>
/// <return>void</return>
//public void GameRequestSelectionUser()
//{

//	if (inputRepeatValidationSignal != 3)
//	{
//		Console.WriteLine("What game would you like to play today with me?\n");
//	}
//	else
//	{
//		Console.WriteLine("""
//					Please input a valid game option from the list below.
//					Example 'A' or 'Add'

//					""");
//	}
//	for (int i = 0, c = 0; i < MethodNames.Count; i++)
//	{
//		string col = default(string);
//		if (c == 24) c += 8;
//		//TODO: create two columns 05/20/2025
//		if (i == Math.Abs(MethodNames.Count / 2)) col = "10";
//		Console.WriteLine($"{(char)('A' + c++)} - {MethodNames[i]}{col}");

//	}

//	Console.WriteLine("Exit");
//	Console.WriteLine("-----------------------------------------------------------------------");

//	// convert unicode characters from enum into symbols that define mathematical operation
//	var unicodeTest = Encoding.Unicode.GetString(Encoding.Unicode.GetBytes("\u002B"));


//	GameSelect = Console.ReadLine()!;


//	// validate the game select to make the value is not null, not a number, and a member of the list of games
//	bool validInputGame = !string.IsNullOrEmpty(GameSelect)
//	&& !int.TryParse(GameSelect, out int result)
//	&& (Enum.GetNames(typeof(OperationsEnum.EnumOperationsMethod)).Contains(GameSelect)
//	|| Enum.GetNames(typeof(OperationsEnum.EnumOperationsMethodPrefix)).Contains(GameSelect));
//  // try
//  // }
//	//	  var _ = (double) typeof(Operations).GetMethod(char.ToUpper(operation[0]) + operation.Substring(1))!.Invoke(this.GetType(), new object[] { firstNum });
//  // } catch(NullReferenceException nfe) { Console.WriteLine(nfe.Message); return; } // exit if the method is not found
//	//}
//	//else if (numOfParam == 2) 
//	//{
//	//	try
//	//	{
//	//		do
//	//		{
//	//			Console.WriteLine("Enter first number");
//	//			firstStringNum = Console.ReadLine();
//	//		} while (firstStringNum is null && !Double.TryParse(firstStringNum, out firstNum));
//	//		firstNum = Convert.ToDouble(firstStringNum);

//	// manage game selection
//	try
//	{
//		if (GameSelect.Count() == 1)
//		{
//			int selectCount = 0;
//			foreach (var selectedGame in Enum.GetValues(typeof(OperationsEnum.EnumOperationsMethodPrefix)))
//			{
//				// different approach to retrieve string from enum via the cast of int to string literal
//				//Enum.GetName(typeof(OperationEnum.EnumOperationMethod), (int) game).ToString();
//				string gameName = ((OperationsEnum.EnumOperationsMethod)selectCount++).ToString();
//				if (GameSelect.ToLower() == selectedGame.ToString()!.ToLower())
//				{
//					Console.WriteLine($"The {gameName} game was selected.");
//					//typeof(Operations).GetMethod(selectedGame.ToString()).Invoke();
//					GameInputManager(gameName);
//				}

//			}
//		}
//		else if (GameSelect.Count() > 1)
//		{
//			foreach (var selectedGame in Enum.GetNames(typeof(OperationsEnum.EnumOperationsMethod)))
//			{
//				if (GameSelect.ToLower() == selectedGame.ToString().ToLower()) Console.WriteLine($"The {selectedGame} game was selected.");
//				GameInputManager(selectedGame);
//			}
//		}
//		else if (GameSelect.Trim().ToLower() == "exit")
//		{
//			Console.WriteLine("Bye for Now!");
//			Environment.Exit(1);
//		}
//		else
//		{
//			Console.WriteLine("No game was selected!\nDo you want to Exit or Restart?");
//			string optionRestartorExit = Console.ReadLine();
//			if (optionRestartorExit?.Trim() == null) { Console.WriteLine(optionRestartorExit); }
//			else if (optionRestartorExit.Trim().ToLower() == "exit"
//				|| optionRestartorExit.Trim().ToLower() == "e"
//				|| optionRestartorExit.Trim().ToLower() == "close") { Environment.Exit(1); }
//			else { GameIntroMethod(); }

//		}
//	}
//	catch (NullReferenceException nfe)
//	{
//		Console.WriteLine(nfe.Message);
//	}

//	// provide user feedback concerning the problem to be solved and take input from user
//	//	Console.WriteLine($"{operation}: {firstNum} {operationSybmol} {secondNum}");
//	//	Console.WriteLine("Please provide an answer");
//	//	answerString = Console.ReadLine();
//	//	while (answerString is null && !Double.TryParse(answerString, out answer))
//	//	{
//	//		Console.WriteLine("Please provide an valid answer");
//	//		answerString = Console.ReadLine();
//	//	}
//	//	answer = Convert.ToDouble(answerString);
//}