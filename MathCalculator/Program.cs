// See https://aka.ms/new-console-template for more information
using MathGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Linq;




internal class Program
{
	private static void Main(string[] args)
	{
		//TODO: may change after refactoring 
		int score = 0;
		
		GameIntro gameIntro = new GameIntro();
		gameIntro.GameIntroMethod();


		const int gameCount = 10;
		for (int i = 0; i < gameCount; i++) 
		{
			gameIntro.GameRequestSelectionUser();

			if (i == 10) Console.WriteLine($"Game over, your Great!!!. Your final score is {score}");
		}

		
	}
}




	





