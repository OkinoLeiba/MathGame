using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGame
{
	internal class GameSelection
	{
		public string gameSelect = default(string)!;
		public List<string> methodNames = default(List<string>)!;
		//Enum EnumMethodName;
		public int inputRepeatValidationSignal = default(int);

		public string GameSelect { get; set; }
		public List<string> MethodNames { get; set; }

		public GameIntro gameIntro = new GameIntro();

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
			//enum EMethodName = methodNames.Select(s => Enum.Parse(typeof(Enum), s)).ToList();

			//attempted to dynamically add element of enum at runtime
			//methodNames.Select(s =>  EnumMethodName s = EnumMethodName(0)); 
		}
		/// <summary>
		/// Manage the selection of the game by user
		/// </summary>
		/// <return>void</return>
		public void GameRequestSelectionUser()
		{
			Console.Clear();
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

			Console.WriteLine("-----------------------------------------------------------------------");

			GameSelect = Console.ReadLine()!;

			// convert unicode characters from enum into symbols that define mathematical operation
			var unicodeTest = Encoding.Unicode.GetString(Encoding.Unicode.GetBytes("\u002B"));

			// validate the game select to make the value is not null, not a number, and a member of the list of games
			bool validInputGame = !string.IsNullOrEmpty(GameSelect)
			&& !int.TryParse(GameSelect, out int result)
			&& (Enum.GetNames(typeof(OperationsEnum.EnumOperationsMethod)).Contains(GameSelect)
			|| Enum.GetNames(typeof(OperationsEnum.EnumOperationsMethodPrefix)).Contains(GameSelect));

			// recursively validate the game selection input
			if (!validInputGame) { inputRepeatValidationSignal++; GameRequestSelectionUser(); }

			// manage game selection
			try
			{
				if (GameSelect.Count() == 1)
				{
					int selectCount = 0;
					foreach (var selectedGame in Enum.GetValues(typeof(OperationsEnum.EnumOperationsMethodPrefix)))
					{
						// different approach to retrieve string from enum via the cast of int to string literal
						//Enum.GetName(typeof(OperationEnum.EnumOperationMethod), (int) game).ToString();
						string gameName = ((OperationsEnum.EnumOperationsMethod)selectCount++).ToString();
						if (GameSelect.ToLower() == selectedGame.ToString()!.ToLower())
						{
							Console.WriteLine($"The {gameName} game was selected.");
							//typeof(Operations).GetMethod(selectedGame.ToString()).Invoke();
							gameIntro.GameInputManager(gameName);
						}

					}
				}
				else if (GameSelect.Count() > 1)
				{
					foreach (var selectedGame in Enum.GetNames(typeof(OperationsEnum.EnumOperationsMethod)))
					{
						if (GameSelect.ToLower() == selectedGame.ToString().ToLower()) Console.WriteLine($"The {selectedGame} game was selected.");
						gameIntro.GameInputManager(selectedGame);
					}
				}
				else if (GameSelect.Trim().ToLower() == "exit")
				{
					Console.WriteLine("Bye for Now!");
					Environment.Exit(1);
				}
				else
				{
					Console.WriteLine("No game was selected!\nDo you want to Exit or Restart?");
					string? optionRestartorExit = Console.ReadLine();
					if (optionRestartorExit?.Trim() == null) { Console.WriteLine(optionRestartorExit); }
					else if (optionRestartorExit.Trim().ToLower() == "exit"
						|| optionRestartorExit.Trim().ToLower() == "e"
						|| optionRestartorExit.Trim().ToLower() == "close") { Environment.Exit(1); }
					else { gameIntro.GameIntroMethod(); }

				}
			}
			catch (NullReferenceException nfe)
			{
				Console.WriteLine(nfe.Message);
			}

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
	}
}
