using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

using static MathGame.OperationsEnum;

namespace MathGame;

/// <summary>
/// Manage the questions of the games.
/// </summary>
public class QuestionGenerator
{
	static public int firstNum = 0;
	static public int secondNum = 0;
	static public int numOfParam = 0; // number of parameters for the operation method

	Random random = new Random();

	static public int FirstNum { get; private set; }
	static public int SecondNum { get; private set; }
	static public int NumOfParam { get; private set; }
	
	/// <summary>
	/// Generate math questions for the game
	/// </summary>
	/// <param name="operation">the question generate is based on mathematical operation</param>
	public void MathQuestion(string operation)
	{
		

		try
		{
			// strings may require conversion to title case
			// the name of game will remain consistent with the naming convection of the methods in the 
			// Operations class
			numOfParam = typeof(Operations).GetMethod(operation)!.GetParameters().Length;
		} catch(NullReferenceException nfe) { Console.WriteLine(nfe.Message); }

		// more type safe way to get the number of parameters for the method
		//var methodInfo = typeof(Operations).GetMethod(operation);
		//if (methodInfo != null)
		//{
		//	numOfParam = methodInfo.GetParameters(operation).Length;

		//}
		//else
		//{
		//	Console.WriteLine("Method not found.");
		//}

		if (numOfParam == 1) firstNum = random.Next(1, 99); else firstNum = random.Next(0, 99); secondNum = random.Next(0, 99);

		if (operation.Trim() == "Division" 
			||  operation.Trim() == "Subtraction" 
			|| operation.Trim() == "Power") firstNum = int.Max(firstNum, secondNum); secondNum = int.Min(firstNum, secondNum);

		if (operation.Trim() == "Division" && secondNum == 0) secondNum = random.Next(1,99);

		FirstNum = firstNum;
		SecondNum = secondNum;
		NumOfParam = numOfParam;

		// the name of game will remain consistent with the naming convection of the methods in the 
		// Operations class
		var operationSybmol = typeof(EnumOperationsMethodUnitSymbol)
			.GetTypeInfo()
			.DeclaredMembers
			//.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase)
			.SingleOrDefault(m => m.Name.Trim() == operation.Trim())?
			.GetCustomAttributes<DescriptionAttribute>(false)
			.First()
			.Description
			.ToString();


		// provide user feedback concerning the problem to be solved and take input from user
		if (numOfParam == 1) Console.WriteLine($"\n{char.ToUpper(operation[0]) + operation.Substring(1)}: {operationSybmol} {firstNum}"); else Console.WriteLine($"\n{char.ToUpper(operation[0]) + operation.Substring(1)}: {firstNum} {operationSybmol} {secondNum}");

		GameAnswer gameAnswer = new GameAnswer(operation);
		gameAnswer.gameAnswerPrompt();
	}
}






