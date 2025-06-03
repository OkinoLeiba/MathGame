// See https://aka.ms/new-console-template for more information
using MathGame;
using System;


// Program will initialize the GameIntro class and GameLogManager class
// and call the GameIntroMethod to start the game intro process.
// This create a method chain where the GameIntro class will handle or start.
// Consider whether to refactor the code to separate concerns!!!
// Consider which or if methods will be static or instance methods!!!
// Consider whether to use dependency injection or not!!!
// Consider whether to use interfaces or not!!!
// Consider whether to use a factory pattern and initialize all needed classes on the onset or start of program!!!
// Pass a reference to the classes and invoke methods in the classes as needed???
// Manage all instances of the classes in a single namespace or class???
// Consider whether to use of a singleton pattern for the GameLogManager class!!!
// Consider which or if certain methods will need to be async or not!!!
// The methods are not async at the moment, but may need to be in the future if they involve I/O operations or long-running tasks.
// The methods are single responsibility methods, meaning they handle a single task - no long-running tasks at the moment.

// The GameIntro class will handle the game introduction and user input for the game name.
// The GameLogManager will manage the game history and log the game type selected by the user.
// The GameLogManager will also update the game history with the game type.


internal class Program
{
	private static void Main(string[] args)
	{
		//TODO: may change after refactoring 
		//int score = 0;
		
		GameIntro gameIntro = new GameIntro();
		//GameSelection gameSelection = new GameSelection();
		GameLogManager gameLogManager = new GameLogManager();
		gameLogManager.UpdateGameHistory("Addition");
		gameIntro.GameIntroMethod();


	

		
	}
}




	





