using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static MathGame.GameStateManager;

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
		// assuming GameSelection is a class that handles game selection logic
		// AppManager has no knowledge of the GameSelection class, but it can be used to select a game type
		private readonly GameSelection _gameSelection;

		private GameStateManager.GameState _currentState; // part of FSM implementation 

		/// default constructor initializes the AppManager without any parameters
		public AppManager()
		{
			// initialize the application manager, which can handle various app-level tasks
			// such as managing game states, user sessions, or other global settings.
			Console.WriteLine("AppManager initialized.");
			_gameIntro = new GameIntro() ?? throw new ArgumentNullException(nameof(_gameIntro));
			_gameLogManager = new GameLogManager() ?? throw new ArgumentNullException(nameof(_gameLogManager));
			_gameSelection = new GameSelection() ?? throw new ArgumentNullException(nameof(_gameSelection));
			SwitchState(GameStateManager.GameState.NotStarted);
			StartGame();
		}

		/// constructor initializes the AppManager with GameIntro and GameLogManager instances
		public AppManager(GameIntro gameIntro, GameLogManager gameLogManager)
		{
			_gameIntro = gameIntro ?? throw new ArgumentNullException(nameof(gameIntro));
			_gameLogManager = gameLogManager ?? throw new ArgumentNullException(nameof(gameLogManager));
			_gameSelection = new GameSelection() ?? throw new ArgumentNullException(nameof(_gameSelection));
			SwitchState(GameStateManager.GameState.NotStarted);
			StartGame();
		}
		private void StartGame()
		{
			_gameIntro.GameIntroMethod();
			_gameLogManager.UpdateGameHistory("Addition"); // example of updating game history

			if (_gameIntro.GetType().IsInstanceOfType(typeof(GameIntro)) && _gameLogManager.GetType().IsInstanceOfType(typeof(GameLogManager)))
			{
				SwitchState(GameStateManager.GameState.Completed);
			}
			else
			{

				SwitchState(GameStateManager.GameState.Failed);
			}

			SwitchState(GameStateManager.GameState.Started);
			Console.WriteLine("Game has started successfully!");
		}

		/// Implementing Finite State Machine (FSM) or State Pattern for managing game states ///
		/// refer: https://www.aleksandrhovhannisyan.com/blog/implementing-a-finite-state-machine-in-cpp/


		// single abstraction to manage the entire process of changing the game state 
		public void SwitchState(GameStateManager.GameState newState)
		{
			// logic to change the state of the game or application
			Console.WriteLine($"Changing state to: {newState}");
			// this could involve updating UI, game logic, etc...
			// SwitchState is absolved of that responsibility and has only the one responsibility 
			ExitCurrentState(_currentState);
			_currentState = newState;
			EnterNewState(_currentState);
		}

		private void ExitCurrentState(GameStateManager.GameState state)
		{
			switch (state)
			{
				case GameState.InProgress:
					Console.WriteLine("Action in progress...");
					break;
				case GameState.Completed:
					Console.WriteLine("Action completed...");
					break;
				case GameState.Failed:
					Console.WriteLine("Action failed...");
					break;
				case GameState.NotStarted:
					Console.WriteLine("Game not started...");
					break;
				case GameState.Loading:
					Console.WriteLine("Game loading...");
					break;
				case GameState.Started:
					Console.WriteLine("Game started...");
					break;
				case GameState.Playing:
					Console.WriteLine("Resuming game...");
					break;
				case GameState.Paused:
					Console.WriteLine("Game paused...");
					break;
				case GameState.MainMenu:
					Console.WriteLine("Main Menu...");
					break;
				case GameState.GameOver:
					Console.WriteLine("Game over...");
					break;
				// logic to restart game in case of exceptions or other issues
				default:
					_currentState = GameState.MainMenu;
					break;
			}
			// needs to execute upon change in state
			UpdateGameState();
		}

		private void EnterNewState(GameStateManager.GameState state)
		{
			switch (state)
			{
				case GameState.InProgress:
					Console.WriteLine("Action in progress...");
					break;
				case GameState.Completed:
					Console.WriteLine("Action completed...");
					break;
				case GameState.Failed:
					Console.WriteLine("Action failed...");
					break;
				case GameState.NotStarted:
					Console.WriteLine("Game not started...");
					break;
				case GameState.Loading:
					Console.WriteLine("Game loading...");
					break;
				case GameState.Started:
					Console.WriteLine("Game started...");
					break;
				case GameState.Playing:
					Console.WriteLine("Resuming game...");
					break;
				case GameState.Paused:
					Console.WriteLine("Game paused...");
					break;
				case GameState.MainMenu:
					Console.WriteLine("Main Menu...");
					break;
				case GameState.GameOver:
					Console.WriteLine("Game over...");
					break;
				// logic to restart game in case of exceptions or other issues
				default:
					_currentState = GameState.MainMenu;
					break;
			}
		}


		// change behavior of game based on state or control the flow of the game based on state
		private void UpdateGameState()
		{
			
			// method will used to update the game state based on the game name or other parameters.
			// used to manage the game flow, such as starting, pausing, or ending the game.

			// trigger the save of game history
			// what game states should  be used to trigger this???
			// should I create an entire method to manage??
			//_gameLogManager.UpdateGameHistory(gameName);

			// call this every frame
			// this manages the sequence of executions managing the game flow
			// all based on the state of the game 
			switch (_currentState)
			{
				case GameState.InProgress:
					Console.WriteLine("Action in progress...");
					break;
				case GameState.Completed:
					Console.WriteLine("Action completed...");
					break;
				case GameState.Failed:
					HandleGameIntro();
					break;
				case GameState.NotStarted:
					HandleGameStart();
					break;
				case GameState.Loading:
					Console.WriteLine("Game loading...");
					break;
				case GameState.Started:
					HandleGameStart();
					break;
				case GameState.Playing:
					Console.WriteLine("Resuming game...");
					break;
				case GameState.Paused:
					HandleGameplay();
					break;
				case GameState.MainMenu:
					HandleMainMenu();
					break;
				case GameState.GameOver:
					HandleGameOver();
					break;
				// logic to restart game in case of exceptions or other issues
				default:
					_currentState = GameState.MainMenu;
					break;
			}

			Console.WriteLine($"Game state updated to: {_currentState}");
		}


		private void HandleGameStart()
		{
			_gameSelection.GameRequestSelectionUser();
			SwitchState(GameStateManager.GameState.Started);
		}

		private void HandleGameIntro()
		{
			if (GameStateManager.GameState.Failed.ToString() == "Failed") Console.WriteLine("Game failed to load, restarting game.");
			_gameIntro.GameIntroMethod();
			SwitchState(GameStateManager.GameState.Started);
		}
		private void HandleMainMenu()
		{
			Console.WriteLine("Main Menu logic...");

		}
		private void HandleGameplay() => Console.WriteLine("Gameplay logic...");


		// both this method and gameAnswerManager method can call the ContinueGameSelectOrEnd method
		// ideally only this method should manage the game-over state
		// or the game over behavior should be a single action and not the two of continue or end 
		private void HandleGameOver()
		{
			_gameSelection.ContinueGameSelectOrEnd(_gameSelection.GameSelect);
			// TODO: include logic to save game history
		}


		/// IDisposable to clean up resources if needed
	}
}
