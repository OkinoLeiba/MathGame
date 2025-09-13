using System;
using System.Collections.Generic;



namespace MathGame;

internal class HelperTestModel
{

	/// create a list of test game instances
	private List<GameModel> testGames = new List<GameModel>
		{
			new GameModel("Eve", new DateTime(2025, 4, 10), 175, "Division"),
			new GameModel("Tom", new DateTime(2025, 4, 15), 200, "Square Root"),
			new GameModel("Sam", new DateTime(2025, 5, 18), 120, "Cube Root"),
			new GameModel("Linda", new DateTime(2025, 5, 20), 250, "Exponential"),
			new GameModel("Steve", new DateTime(2025, 6, 5), 140, "Logarithm"),
			new GameModel("Sophia", new DateTime(2025, 6, 12), 210, "Factorial"),
			new GameModel("Dan", new DateTime(2025, 7, 1), 190, "Absolute Value"),
			new GameModel("Amy", new DateTime(2025, 7, 7), 230, "Modulus"),
			new GameModel("Chris", new DateTime(2025, 7, 14), 80, "Sine"),
			new GameModel("Emma", new DateTime(2025, 7, 21), 90, "Cosine"),
			new GameModel("Mark", new DateTime(2025, 8, 3), 135, "Tangent"),
			new GameModel("Nina", new DateTime(2025, 8, 9), 125, "Cotangent"),
			new GameModel("Luke", new DateTime(2025, 8, 15), 185, "Secant"),
			new GameModel("Oliver", new DateTime(2025, 8, 22), 95, "Cosecant"),
			new GameModel("Julia", new DateTime(2025, 9, 1), 160, "Exponentiation"),
			new GameModel("Ryan", new DateTime(2025, 9, 8), 145, "Natural Log"),
			new GameModel("Bella", new DateTime(2025, 9, 14), 240, "Square Root Binary"),
			new GameModel("James", new DateTime(2025, 9, 21), 110, "Multiplication"),
			new GameModel("Carla", new DateTime(2025, 10, 2), 155, "Subtraction"),
			new GameModel("Gavin", new DateTime(2025, 10, 7), 205, "Addition"),
			new GameModel("Megan", new DateTime(2025, 10, 13), 175, "Logarithm Base 10"),
			new GameModel("Derek", new DateTime(2025, 10, 18), 220, "Power Scratch")
		};

	// method to display test data
	/// <summary>
	/// Prints the details of each test game instance to the console.
	/// </summary>
	/// <return>void</return>
	public void DisplayTestGames()
	{
		foreach (var game in testGames)
		{
			Console.WriteLine($"Player: {game.Name}, Date: {game.Date}, Score: {game.Score}, Type: {game.GameType}");
		}
	}
}


