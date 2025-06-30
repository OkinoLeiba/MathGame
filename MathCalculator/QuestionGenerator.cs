using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

using static MathGame.OperationsEnum;

namespace MathGame;

public class QuestionGenerator
{
	public int firstNum = 0;
	public int secondNum = 0;
	public int numOfParam = 0; // number of parameters for the operation method

	Random random = new Random();

	public int FirstNum { get; private set; }
	public int SecondNum { get; private set; }
	public int NumOfParam { get; private set; }

	public QuestionGenerator()
	{
		FirstNum = firstNum;
		SecondNum = secondNum;
		NumOfParam = numOfParam;
	}

	public void MathQuestion(string operation)
	{

		try
		{
			// strings requires conversion to title case
			int numOfParam = typeof(Operations).GetMethod(char.ToUpper(operation[0]) + operation.Substring(1))!.GetParameters().Length;
		} catch(NullReferenceException nfe) { Console.WriteLine(nfe.Message); }
		//var methodInfo = typeof(Operations).GetMethod(operation);
		//if (methodInfo != null)
		//{
		//	numOfParam = methodInfo.GetParameters().Length;
			
		//}
		//else
		//{
		//	Console.WriteLine("Method not found.");
		//}

		if (numOfParam == 1) firstNum = random.Next(1, 99); else firstNum = random.Next(0, 99); secondNum = random.Next(0, 99);

		if (operation.Trim().ToLower() == "division" ||  operation.Trim().ToLower() == "subtraction") firstNum = int.Max(firstNum, secondNum); secondNum = int.Min(firstNum, secondNum);

		if (operation.Trim().ToLower() == "division" && secondNum == 0) secondNum = 1;

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
		if (numOfParam == 1) Console.WriteLine($"\n{char.ToUpper(operation[0]) + operation.Substring(1)}: {operationSybmol} {firstNum}"); else Console.WriteLine($"\n{char.ToUpper(operation[0]) + operation.Substring(1)}: {firstNum} {operationSybmol} {secondNum}");

		GameAnswer gameAnswer = new GameAnswer(operation);
		gameAnswer.gameAnswerPrompt();
	}
}






