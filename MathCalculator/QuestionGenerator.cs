using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

using static MathGame.OperationsEnum;

namespace MathGame;

internal class QuestionGenerator
{
	public int firstNum = 0;
	public int secondNum = 0;

	Random random = new Random();

	public void MathQuestion(string operation)
	{
		int numOfParam = typeof(Operations).GetMethod(operation)!.GetParameters().Length;

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
		if (numOfParam == 1) Console.WriteLine($"{operation}: {firstNum} {operationSybmol} {secondNum}"); else Console.WriteLine($"{operation}: {operationSybmol} {firstNum}");

		GameAnswer gameAnswer = new GameAnswer();
		gameAnswer.gameAnswerPrompt();
	}
}
