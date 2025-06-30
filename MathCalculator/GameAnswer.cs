

using System;

namespace MathGame;

public class GameAnswer
{
	public int score;
	public double answer;
	public double result;
	public string answerString;

	public string GameSelect { get; set; }
	GameSelection gameSelectionManager;

	public GameAnswer(string game)
	{
		GameSelect = game;
		gameSelectionManager = new GameSelection();

	}


	public void gameAnswerPrompt()
	{

		Console.WriteLine("-----------------------------------------------------------------------");
		Console.WriteLine("Please provide an answer");
		answerString = Console.ReadLine();

		while (string.IsNullOrEmpty(answerString) && !Double.TryParse(answerString, out _))
		{
			Console.WriteLine("Please provide an valid answer");
			answerString = Console.ReadLine();
		}
		//TODO: create exception handler
		answer = Convert.ToDouble(answerString);

		gameAnswerManager(); // call the gameAnswerManager method to process the answer


	}

	public void gameAnswerManager()
	{
		int numOfParam = QuestionGenerator.NumOfParam; // number of parameters for the operation method
		ref double refAnswer = ref answer; // using ref to avoid unnecessary copying of the result variable 
		int firstNum = QuestionGenerator.FirstNum; // first number for the operation method
		int secondNum = QuestionGenerator.SecondNum; // second number for the operation method

		GameIntro gameIntro = new GameIntro(); // create an instance of the GameIntro class to get the reference answer

		if (numOfParam == 1)
		{
			try
			{
				result = (double)typeof(Operations).GetMethod(string.Concat(char.ToUpper(GameSelect[0]), GameSelect.Substring(1)))?.Invoke(new Operations(), new object[] { firstNum })!;
			}
			catch (NullReferenceException nfe) { Console.WriteLine(nfe.Message); return; } // exit if the method is not found 
		}
		else if (numOfParam == 2)
		{
			try
			{
				result = (double)typeof(Operations).GetMethod(string.Concat(char.ToUpper(GameSelect[0]), GameSelect.Substring(1)))?.Invoke(new Operations(), new object[] { firstNum, secondNum })!;
			}
			catch (NullReferenceException nfe) { Console.WriteLine(nfe.Message); return; } // exit if the method is not found 
		}

		if (Math.Abs(result - refAnswer) < 0.0001) // using a tolerance for floating point comparison
		{
			Console.WriteLine("Congratulations! You got the correct answer!");
			GameIntro.Score++;
			GameIntro.CorrectAnswer++;
		}
		else
		{
			Console.WriteLine($"Sorry, the correct answer is {result}.");
			GameIntro.WrongAnswer++;
		}
		Console.WriteLine($"Your current score is: {GameIntro.Score}");
		Console.WriteLine("-----------------------------------------------------------------------");

		gameSelectionManager.ContinueGameSelectOrEnd(GameSelect); // call the ContinueGameSelectOrEnd method to continue or end the game
	}
}
