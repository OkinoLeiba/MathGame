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
	/// Beginning of program after main entry point and iniatilized
	/// </summary>
	internal class GameIntro
	{
		public string? name = default(string);
		public string gameSelect = default(string)!;
		public List<string> methodNames = default(List<string>)!;
		//Enum EnumMethodName;
		int inputRepeatValidationSignal = default(int);

		public string? Name { get; set; }
		public string GameSelect { get; set; }
		public List<string> MethodNames { get; set; }

		Operations operations = new Operations();

		 
		public GameIntro() {
			Name = name ?? string.Empty;
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
			GameRequestSelectionUser();
		}

		/// <summary>
		/// Manage the selection of the game by user
		/// </summary>
		/// <return>void</return>
		public void GameRequestSelectionUser()
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
				for (int i = 0, c = 0; i < MethodNames.Count; i++)
				{
					string col = default(string);
					if (c == 24) c += 8;
					//TODO: create two columns 05/20/2025
					if (i == Math.Abs(MethodNames.Count / 2)) col = "10";
					Console.WriteLine($"{(char)('A' + c++)} - {MethodNames[i]}{col}");
					
				}

				Console.WriteLine("Exit");
				Console.WriteLine("-----------------------------------------------------------------------");

			// convert unicode characters from enum into symbols that define mathematical operation
			var unicodeTest = Encoding.Unicode.GetString(Encoding.Unicode.GetBytes("\u002B"));
				

			GameSelect = Console.ReadLine()!;

		
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
							GameInputManager(gameName);
						}
						
					}
				}
				else if (GameSelect.Count() > 1)
				{
					foreach (var selectedGame in Enum.GetNames(typeof(OperationsEnum.EnumOperationsMethod)))
					{
						if (GameSelect.ToLower() == selectedGame.ToString().ToLower()) Console.WriteLine($"The {selectedGame} game was selected.");
						GameInputManager(selectedGame);
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
					string optionRestartorExit = Console.ReadLine();
					if (optionRestartorExit?.Trim() == null) { Console.WriteLine(optionRestartorExit); }
					else if (optionRestartorExit.Trim().ToLower() == "exit"
						|| optionRestartorExit.Trim().ToLower() == "e"
						|| optionRestartorExit.Trim().ToLower() == "close") { Environment.Exit(1); }
					else { GameIntroMethod(); }

				}
			}
			catch(NullReferenceException nfe)
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

		public void GameInputManager(string operation)
		{
			int firstNum = default(int);
			string? firstStringNum = default(string);
			int secondNum = default(int);
			string? secondStringNum = default(string);

	
			//LazyInitializer.EnsureInitialized(Operations);

			// initial approach to find the method signature, specifically the count of parameters
			//MethodInfo.GetCurrentMethod();
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

				typeof(Operations).GetMethod(operation)!.Invoke(this.GetType(), new object[] { firstNum });
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
					} while (secondStringNum is null && !int.TryParse(secondStringNum, out secondNum));
					secondNum = Convert.ToInt16(secondStringNum);
				}
				catch (NullReferenceException nfe)
				{
					Console.WriteLine(nfe.Message);
				}

				var operationSybmol = typeof(EnumOperationsMethodUnitSymbol)
					.GetTypeInfo()
					.DeclaredMembers
					//.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase)
					.SingleOrDefault(m => m.Name.Trim().ToLower() == operation.Trim().ToLower())?
					.GetCustomAttributes<DescriptionAttribute>(false)
					.First()
					.Description
					.ToString();

				

				Console.WriteLine($"{operation}: {firstNum} {operationSybmol} {secondNum}");


				// two approaches to invoke methods utilizing reflection and type 
				typeof(Operations).InvokeMember(operation, BindingFlags.InvokeMethod | BindingFlags.Instance, null, Activator.CreateInstance(typeof(Operations)), new object[] { firstNum, secondNum });
				
				typeof(Operations).GetMethod(operation)!.Invoke(Activator.CreateInstance(typeof(Operations)), new object[] { firstNum, secondNum });
			}
			else
			{
				Console.WriteLine("No valid numbers were provided!");
			}

			
		}

		//public string? ToEnumMember<T>(this T value) where T : Enum
		//{
		//	return typeof(T)
		//		.GetTypeInfo()
		//		.DeclaredMembers
		//		.SingleOrDefault(x => x.Name == value.ToString())?
		//		.GetCustomAttribute<EnumMemberAttribute>(false)?
		//		.Value;
		//}
	}
}
