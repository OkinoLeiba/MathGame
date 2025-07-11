
using MathGame;
using MathGameMaui.Data;
using MathGameMaui.Model;
using System.ComponentModel;
using System.Reflection;

using static MathGame.OperationsEnum;


namespace MathGameMaui;

public partial class GamePage : ContentPage
{
	public string GameSelect {get; set;}
	private int numParams;
	private int firstNum = 0;
	private int secondNum = 0;
	static private int score = 0;
	const int totalQuestion = 2;
	private int gameCount = totalQuestion;

	Random random = new Random();

	public int FirstNum
	{
		get => firstNum;
		set
		{
			firstNum = value;
			OnPropertyChanged(nameof(FirstNum));
		}
	}

	public int SecondNum
	{
		get => secondNum;
		set
		{
			secondNum = value;
			OnPropertyChanged(nameof(SecondNum));
		}
	}

	public GamePage(string gameSelect)
	{
		InitializeComponent();
		GameSelect = gameSelect;
		BindingContext = this;

		try
		{
			numParams = typeof(MathGame.Operations).GetMethod(string.Concat(char.ToUpper(GameSelect[0]), GameSelect.Substring(1)))!.GetParameters().Length;
		}
		catch (NullReferenceException nfe) { Console.WriteLine(nfe.Message); }

		NeutralGameState();
		GenerateQuestion();
	}

	private void NeutralGameState()
	{

		//ScoreLabel.IsVisible = false;
		ResultLabel.IsVisible = false;
		AnswerEntry.Text = string.Empty;
	}

	private void GenerateQuestion()
	{
		firstNum = random.Next(0, 99);
		secondNum = random.Next(0, 99);

		if (GameSelect.Trim().ToLower() == "division"
			|| GameSelect.Trim().ToLower() == "subtraction"
			|| GameSelect.Trim().ToLower() == "power")
		{
			if (GameSelect.Trim().ToLower() == "division" && (FirstNum == 0 || SecondNum == 0))
			{
				FirstNum = random.Next(1, 99); 
				SecondNum = random.Next(1, 99);

				while (FirstNum < SecondNum || FirstNum % SecondNum == 0)
				{

					FirstNum = random.Next(1, 99);
					SecondNum = random.Next(1, 99);
				}
			}
			FirstNum = int.Max(FirstNum, SecondNum); SecondNum = int.Min(FirstNum, SecondNum);
		}
		

		var operationSybmol = typeof(EnumOperationsMethodUnitSymbol)
			.GetTypeInfo()
			.DeclaredMembers
			.SingleOrDefault(m => m.Name.Trim().ToLower() == GameSelect.Trim().ToLower())?
			.GetCustomAttributes<DescriptionAttribute>(false)
			.First()
			.Description
			.ToString();

		QuestionLabel.Text = numParams == 1 ?
			$"{operationSybmol} {FirstNum}" : 
			$"{FirstNum} {operationSybmol} {SecondNum}";

			//$"{string.Concat(char.ToUpper(GameSelect[0]), GameSelect.Substring(1))}\n{operationSybmol} {FirstNum}" : 
			//$"{string.Concat(char.ToUpper(GameSelect[0]), GameSelect.Substring(1))}\n{FirstNum} {operationSybmol} {SecondNum}";

		if (ScoreLabel.IsVisible) NeutralGameState();
	}

	private void GenerateResult()
	{
		var result = numParams == 1 ?
			(int)(double)typeof(Operations).GetMethod(string.Concat(char.ToUpper(GameSelect[0]), GameSelect.Substring(1)))?.Invoke(Activator.CreateInstance(typeof(Operations)), new object[] { FirstNum }) :
			(int)(double)typeof(Operations).GetMethod(string.Concat(char.ToUpper(GameSelect[0]), GameSelect.Substring(1)))?.Invoke(Activator.CreateInstance(typeof(Operations)), new object[] { FirstNum, SecondNum });

		if (result is int intResult) // ensure the result is cast to an integer
		{
			ProcessAnswer(intResult);
		}
		else
		{
			throw new InvalidCastException("The result could not be cast to an integer.");
		}
	
	}

	private void ProcessAnswer(int result)
	{
		int answer = Int32.Parse(AnswerEntry.Text);

		if ((int)answer == (int)result) // convert int to account for different numerical datatype
		{
			score++;
			ResultLabel.IsVisible = true;
			ResultLabel.Text = "Congratulations! You got the correct answer!";
		}
		else
		{
			ResultLabel.IsVisible = true;
			ResultLabel.Text = $"Sorry, the correct answer is {result}.";
		}

		ScoreLabel.IsVisible = true;
		ScoreLabel.Text = $"Your current score is: {score}";

		gameCount--;

		GameManager(gameCount);
		
	}

	private void GameManager(int count)
	{
		if (count > 0)
		{
			
			GenerateQuestion();
		}
		else
		{
			GameOver();
		}
		//NeutralGameState();
	}

	private void GameOver()
	{
		GameOverLabel.IsVisible = true;
		GameOverLabel.Text = $"Game Over! Congratulations you got {score} out of {totalQuestion} right!";

		// different approaches to save the game history to the repository
		// used the app instance, the current instance to access the GameRepository and save the game history
		//App.GameRepository.UpdateGameHistory(new Model.Game { GameCount = totalQuestion, Score = score, GameSelect = GameSelect });

		//Application.Current?.Resources["GameRepository"] = new Model.Game { GameCount = totalQuestion, Score = score, GameSelect = GameSelect };

		//App.Current?.GameRespository?.UpdateGameHistory();

		//Application.Current?.GameRespository.UpdateGameHistory(new Model.Game { GameCount = totalQuestion, Score = score, GameSelect = GameSelect });

		//= App.GameRepository.SaveGameAsync(new Model.Game { GameCount = totalQuestion, Score = score, GameSelect = GameSelect }).ContinueWith(task =>
		//{
		//	if (task.IsCompletedSuccessfully)
		//	{
		//		Console.WriteLine("Game history saved successfully.");
		//	}
		//	else
		//	{
		//		Console.WriteLine($"Error saving game history: {task.Exception?.Message}");
		//	}
		//});
		// ?? new GameRepository(App.Current.GameRepository.DatabasePath); // ensure the GameRepository is initialized

		//App.Current?.GetType().GetProperty("GameRepository").GetValue(App.Current); // get the GameRepository instance from the app

		//GameRepository gameRepository = new GameRepository(App.Current.GameRepository.DatabasePath); // assuming you have a database path set up in your app
		//gameRepository.SaveGameAsync(new Model.Game { GameCount = totalQuestion, Score = score, GameSelect = GameSelect }).ContinueWith(task =>
		//{
		//	if (task.IsCompletedSuccessfully)
		//	{
		//		Console.WriteLine("Game history saved successfully.");
		//	}
		//	else
		//	{
		//		Console.WriteLine($"Error saving game history: {task.Exception?.Message}");
		//	}
		//});

		//App.Current?.FindByName<GameRepository>("GameRepository").SaveGameAsync(new Model.Game { GameCount = totalQuestion, Score = score, GameSelect = GameSelect }).ContinueWith(task =>
		//{
		//	if (task.IsCompletedSuccessfully)
		//	{
		//		Console.WriteLine("Game history saved successfully.");
		//	}
		//	else
		//	{
		//		Console.WriteLine($"Error saving game history: {task.Exception?.Message}");
		//	}
		//});

		App.GameRepository.SaveGameAsync(new Model.Game { GameCount = totalQuestion, Score = score, GameSelect = GameSelect }).ContinueWith(task =>
		{
			if (task.IsCompletedSuccessfully)
			{
				Console.WriteLine("Game history saved successfully.");
			}
			else
			{
				Console.WriteLine($"Error saving game history: {task.Exception?.Message}");
			}
		});

	


		// logic to exit the application
		// Application.Current.Quit(); // close the application
		// Environment.Exit(0); // or use to terminate the process
	}

	private void OnAnswerSubmit(object sender, EventArgs e)
	{	
		GenerateResult();
	}

	private void OnBackMenuClicked(object sender, EventArgs e)
	{
		if (sender is Button button)
		{
			Button btn = (Button)sender;
			Navigation.PopAsync(); // navigate back to the previous page
			SemanticScreenReader.Announce($"{btn.Text} pressed.");
		}
		else
		{
			DisplayAlert("Error", "Please enter a valid answer before going back.", "OK");
		}
	}

	private void OnBackToMainClicked(object sender, EventArgs e)
	{
		if (sender is Button button)
		{
			score = 0; // reset score
			gameCount = totalQuestion; // reset game count
			// NeutralGameState(); // reset the game state
			// AnswerEntry.Text = string.Empty; // clear the answer entry field
			// GameOverLabel.IsVisible = false; // hide the game over label

			Button btn = (Button)sender;
			Navigation.PushAsync(new MainPage()); // navigate back to the main page
			// Navigation.PopToRootAsync() to go back to the root page
			SemanticScreenReader.Announce($"{btn.Text} pressed.");
		}
		else
		{
			DisplayAlert("Error", "Please enter a valid answer before going back.", "OK");
		}
	}
}