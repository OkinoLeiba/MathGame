using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGame
{
	internal class AppManager
	{
		//// this class manages the application-level tasks and interactions between different components of the game.
		//// it initializes the game intro and log manager, starts the game, and handles any app-level logic.
		//// it can also manage user sessions, game states, and other global settings.
		//// it can be used to manage the flow of the game, such as starting, pausing, or ending the game.
		//// it can also be used to manage the game history, user sessions, and other global settings.

		//// instances will be read-only and whatever is added to the list will be immutable
		//// whoever uses the instance of the class will have to use the methods provided by this class
		//// and will be responsible for managing the game flow and user interactions
		//// an/or managing the state and the data of that class and not the AppManager class

		//TODO: consider designing AppManager to have access to state and data of all the classes in the application and within the entire namespace
		//TODO: consider whether to use a singleton pattern for the AppManager class or not
		//TODO: consider using the observer pattern to notify other classes of changes in the game state or user interactions; meaning global accessibility for the AppManager class

		// tight coupling between most of the classes in the namespace may not be ideal for maintainability and scalability, but it is acceptable for a small application like this
		private readonly GameIntro _gameIntro;
		private readonly GameLogManager _gameLogManager;

		/// default constructor initializes the AppManager without any parameters
		public AppManager()
		{
			// initialize the application manager, which can handle various app-level tasks
			// such as managing game states, user sessions, or other global settings.
			Console.WriteLine("AppManager initialized.");
			_gameIntro = new GameIntro() ?? throw new ArgumentNullException(nameof(_gameIntro));
			_gameLogManager = new GameLogManager() ?? throw new ArgumentNullException(nameof(_gameLogManager));
			StartGame();
		}

		/// constructor initializes the AppManager with GameIntro and GameLogManager instances
		public AppManager(GameIntro gameIntro, GameLogManager gameLogManager)
		{
			_gameIntro = gameIntro ?? throw new ArgumentNullException(nameof(gameIntro));
			_gameLogManager = gameLogManager ?? throw new ArgumentNullException(nameof(gameLogManager));
			StartGame();
		}
		private void StartGame()
		{
			_gameIntro.GameIntroMethod();
			_gameLogManager.UpdateGameHistory("Addition"); // Example of updating game history
			Console.WriteLine("Game has started successfully!");
		}

		
	}
}
