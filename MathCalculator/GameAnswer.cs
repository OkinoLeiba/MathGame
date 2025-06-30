

using System;

namespace MathGame;

public class GameAnswer
{
	public int score;
	public double answer;
	public double result;
	public string answerString;

	public string GameSelect { get; set; }


	public GameAnswer(string game)
	{
		this.GameSelect = game;
	}

	QuestionGenerator questionGenerator = new QuestionGenerator();
	GameSelection gameSelectionManager = new GameSelection(GameSelect);


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
		answer = Convert.ToDouble(answerString);

		gameAnswerManager(); // call the gameAnswerManager method to process the answer


	}

	public void gameAnswerManager()
	{
		int numOfParam = questionGenerator.numOfParam; // number of parameters for the operation method
		ref double refAnswer = ref answer; // using ref to avoid unnecessary copying of the result variable 
		int firstNum = questionGenerator.firstNum; // first number for the operation method
		int secondNum = questionGenerator.secondNum; // second number for the operation method

		GameIntro gameIntro = new GameIntro(); // create an instance of the GameIntro class to get the reference answer

		if (numOfParam == 1)
		{
			try
			{
				result = (double)typeof(Operations).GetMethod(char.ToUpper(gameSelection[0]) + gameSelection.Substring(1))?.Invoke(typeof(Operations), new object[] { firstNum })!;
			}
			catch (NullReferenceException nfe) { Console.WriteLine(nfe.Message); return; } // exit if the method is not found 
		}
		else if (numOfParam == 2)
		{
			try
			{
				result = (double)typeof(Operations).GetMethod(char.ToUpper(gameSelection[0]) + gameSelection.Substring(1))?.Invoke(typeof(Operations), new object[] { firstNum, secondNum })!;
			}
			catch (NullReferenceException nfe) { Console.WriteLine(nfe.Message); return; } // exit if the method is not found 
		}

		if (Math.Abs(result - refAnswer) < 0.0001) // using a tolerance for floating point comparison
		{
			Console.WriteLine("Congratulations! You got the correct answer!");
			score++;
		}
		else
		{
			Console.WriteLine($"Sorry, the correct answer is {result}.");
		}
		Console.WriteLine($"Your current score is: {score}");
		Console.WriteLine("-----------------------------------------------------------------------");

		gameSelectionManager.ContinueGameSelectOrEnd(gameSelection, GameIntro.Instances[0]); // call the ContinueGameSelectOrEnd method to continue or end the game
	}
}
