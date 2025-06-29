

using System;

namespace MathGame;

internal class GameAnswer
{
	public int score = 0;
	public double answer = 0;
	public double result = 0;
	public string answerString = default(string);

	public void gameAnswerPrompt()
	{
		
		Console.WriteLine("Please provide an answer");
		answerString = Console.ReadLine();
		while (string.IsNullOrEmpty(answerString) && !Double.TryParse(answerString, out _))
		{
			Console.WriteLine("Please provide an valid answer");
			answerString = Console.ReadLine();
		}
		answer = Convert.ToDouble(answerString);

	}

	public void gameAnswerManager(double result)
	{

	}
}
