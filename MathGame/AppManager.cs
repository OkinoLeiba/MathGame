using Spectre.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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
		//// it can also be used to manage the game history, user sessions' data, and other global settings or global data/state.

		//// instances of the classes will be added to the read-only list and will be immutable or ?mutable? to add new instances?
		//// whoever uses the instance of the class will have to use and be responsible for the methods provided by that class
		//// and any additional methods that the instance method of that instance of the class may call and data
		//// and will be responsible for managing the game flow and user interactions
		//// and/or managing the state and the data of that class and not the AppManager class

		//TODO: consider designing AppManager to have access to state and data of all the classes in the application and within the entire namespace
		//TODO: consider whether to use a singleton pattern for the AppManager class or not
		//TODO: consider using the observer pattern to notify other classes of changes in the game state or user interactions; meaning global accessibility for the AppManager class

		// tight coupling between most of the classes in the namespace may not be ideal for maintainability and scalability, but it is acceptable for a small application like this
		private readonly GameIntro _gameIntro;
		private readonly GameLogManager _gameLogManager;
		private readonly GameAnswer _gameAnswer; 
		// AppManager's Perspective: assuming GameSelection is a class that handles game selection logic
		// AppManager has no knowledge of the GameSelection class, but it can be used to select a game type
		// only the behavior should be of concern to AppManager and any other knowledge of the GameSelection
		// class should be kept to a minimum - !to an extent GameSelection is a crossroad to other classes!
		// most other classes are dependent on the data, single data point, it produces
		private readonly GameSelection _gameSelection;

		private GameStateManager.GameState _currentState; // part of FSM implementation 

		private List<object> _instances = new List<object>(); // list of instances of the AppManager class

		public List<object> Instances { get; } // read-only property to access list of instances of the AppManager class

		/// default constructor initializes the AppManager without any parameters
		public AppManager()
		{
			// initialize the application manager, which can handle various app-level tasks
			// such as managing game states, user sessions, or other global settings.
			Console.WriteLine("AppManager initialized.");
			_gameIntro = new GameIntro() ?? throw new ArgumentNullException(nameof(_gameIntro));
			_gameLogManager = new GameLogManager() ?? throw new ArgumentNullException(nameof(_gameLogManager));
			_gameSelection = new GameSelection() ?? throw new ArgumentNullException(nameof(_gameSelection));
			// add all instances of classes to the list of instances
			_instances.Add(_gameIntro); 
			_instances.Add(_gameLogManager);
			_instances.Add(_gameSelection);

			SwitchState(GameStateManager.GameState.NotStarted);
			StartGame();
		}

		/// constructor initializes the AppManager with GameIntro and GameLogManager instances
		public AppManager(GameIntro gameIntro, GameLogManager gameLogManager)
		{
			_gameIntro = gameIntro ?? throw new ArgumentNullException(nameof(gameIntro));
			_gameLogManager = gameLogManager ?? throw new ArgumentNullException(nameof(gameLogManager));
			_gameSelection = new GameSelection() ?? throw new ArgumentNullException(nameof(_gameSelection));
			// add all instances of classes to the list of instances
			_instances.Add(_gameIntro); 
			_instances.Add(_gameLogManager);
			_instances.Add(_gameSelection);

			SwitchState(GameStateManager.GameState.NotStarted);
			StartGame();
		}
		/// <summary>
		/// Initial entry point into the program that starts the game and logger
		/// </summary>
		/// <return>void</return>
		private void StartGame()
		{
			_gameIntro.GameIntroMethod();
			_gameLogManager.UpdateGameHistory("Addition"); // updating game history

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
		/// Using a Switch statement to change the state may not be the best approach, but I think
		/// there is value in knowing what to do as much as knowing what not to do [using an antipattern]
		/// all options are available even if it defies convention.
		/// refer: https://www.aleksandrhovhannisyan.com/blog/implementing-a-finite-state-machine-in-cpp/


		// single abstraction to manage the entire process of changing the game state 
		/// <summary>
		/// Manage the exit and enter state within a single method
		/// </summary>
		/// <param name="newState">transitioning state effecting subsequent behavior</param>
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

		// ExitCurrentState and EnterNewState methods are used to handle the logic for exiting the current state and entering a new state
		// using a while loop to handle the game state changes and actions based on the current state
		// may be the best approach to handle the game state changes and actions based on the current state
		// based on the scale of the application and the complexity of the game logic
		/// <summary>
		/// Handles the necessary actions to exit the specified game state.
		/// </summary>
		/// <remarks>This method performs cleanup or transition logic specific to the provided game state. It ensures
		/// that the game state is properly exited before transitioning to a new state.</remarks>
		/// <param name="state">The current game state to exit. Must be a valid <see cref="GameStateManager.GameState"/> value.</param>
		private void ExitCurrentState(GameStateManager.GameState state)
		{

			#region GameState
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
			#endregion
			// needs to execute upon change in state
			UpdateGameState();
		}

		/// <summary>
		/// Handles the necessary actions to enter the specified game state.
		/// </summary>
		/// <remarks>This method performs cleanup or transition logic specific to the provided game state. It ensures
		/// that the game state is properly entered before transitioning to a new state.</remarks>
		/// <param name="state">The current game state to exit. Must be a valid <see cref="GameStateManager.GameState"/> value.</param>
		private void EnterNewState(GameStateManager.GameState state)
		{
			#region GameState
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
			#endregion
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

			#region GameStateFlow
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
					HandleGamePaused();
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
			#endregion
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
			_gameSelection.GameRequestSelectionUser();

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

		private void HandleActions()
		{
			Task taskManager = new Task(() =>
			{
				if (GameStateManager.GameState.InProgress == _currentState)
				{
					Task.Delay(250).Wait();
					if (Task.CompletedTask.IsCompletedSuccessfully) SwitchState(GameStateManager.GameState.Completed);
				}
				else if (GameStateManager.GameState.Loading == _currentState) 
				{
					AnsiConsole.Progress()
						.AutoRefresh(false) // turn off auto refresh
						.AutoClear(false)   // do not remove the task list when done
						.HideCompleted(false)   // hide tasks as they are completed
						.Columns(new ProgressColumn[]
						{
						new TaskDescriptionColumn(),    // Task description
						new ProgressBarColumn(),        // Progress bar
						new PercentageColumn(),         // Percentage
						new RemainingTimeColumn(),      // Remaining time
						new SpinnerColumn(),            // Spinner
						new DownloadedColumn(),         // Downloaded
						new TransferSpeedColumn(),      // Transfer speed
						})
						.Start(ctx =>
						{
							// define tasks
							var task1 = ctx.AddTask("[green]Loading in Progress...[/]");


							while (!ctx.IsFinished)
							{
								// simulate some work
								Task.Delay(250);

								// increment
								task1.Increment(0.5);

							}
						});
				}
				else if (GameStateManager.GameState.Completed == _currentState)
				{
					Console.WriteLine("Action Completed.");
				}
				else
				{

				}
			});
		}

		private void HandleGamePaused()
		{
			// logic to handle game paused actions, such as saving game state, showing pause menu, etc...
			Console.WriteLine("Game is paused. Showing pause menu...");
			// this could involve updating UI or game logic
			SwitchState(GameStateManager.GameState.Paused);
			do
			{
				Console.WriteLine("Press any key to unpause the game.");
				var input = Console.ReadKey().Key.GetHashCode().ToString();
				//HandleGamePaused?.invoke(input);
				if (string.IsNullOrEmpty(input)) { SwitchState(GameStateManager.GameState.Playing); return; }

			} while (true);

			//static void Lock()
			//{
			//	object lockObj = new object();
			//	lock (lockObj)
			//	{
			//		new Thread(GetInput).Start(lockObj);
			//		Monitor.Wait(lockObj, 10000);
			//	}
			//	Console.WriteLine("Main exiting");
			//}
			//static void GetInput(object state)
			//{
			//	Console.WriteLine("press return...");
			//	string s = Console.ReadLine();
			//	lock (state)
			//	{
			//		Monitor.Pulse(state);
			//	}
			//	Console.WriteLine("GetInput exiting");
			//}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
		{
			PropertyChangedEventHandler propertyChange = this.PropertyChanged;

			if (propertyChange != null)
			{
				propertyChange(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		private void HandleGameProgress()
		{ 
			// logic to handle game progress actions, such as updating score, showing progress bar, etc...
			Console.WriteLine("Game is in progress. Updating score...");
			// this could involve updating UI or game logic

			//INotifyPropertyChanged notifyPropertyChanged;
			OnPropertyChanged(_currentState.ToString());
			SwitchState(GameStateManager.GameState.InProgress);
		}


		private void HandleGameStateChange(GameStateManager.GameState newState)
		{
			// logic to handle game state changes, such as updating UI or game logic
			Console.WriteLine($"Game state changed to: {newState}");
			SwitchState(newState);
		}

		private void HandleGameLogManager()
		{
			// logic to handle game log manager actions
			// such as saving game history, updating game state, etc...
			_gameLogManager.UpdateGameHistory("Addition"); // example of updating game history
			Console.WriteLine("Game log manager actions handled.");
		}
		/// IDisposable to clean up resources if needed
	}
}
