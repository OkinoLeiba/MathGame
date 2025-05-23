// See https://aka.ms/new-console-template for more information
using MathGame;
using System;





internal class Program
{
	private static void Main(string[] args)
	{
		//TODO: may change after refactoring 
		//int score = 0;
		
		GameIntro gameIntro = new GameIntro();
		gameIntro.GameIntroMethod();


		const int gameCount = 10;
		for (int i = 0; i < gameCount; i++)
		{
			gameIntro.GameRequestSelectionUser();

			if (i == 10) {Console.WriteLine($"Game over, your Great!!!. Your final score is {gameIntro.Score}"); Console.ReadLine();}
		
		}

		
	}
}




	





