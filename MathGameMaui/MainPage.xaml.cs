
using MathGame;

namespace MathGameMaui
{
	public partial class MainPage : ContentPage
	{
		
		string figletWelcome = """""
			   .        :    :::. :::::::::::: ::   .:   .,-:::::/   :::.     .        :  .,::::::  
			;;,.    ;;;   ;;`;;;;;;;;;;'''',;;   ;;,,;;-'````'    ;;`;;    ;;,.    ;;; ;;;;''''  
			[[[[, ,[[[[, ,[[ '[[,   [[    ,[[[,,,[[[[[[   [[[[[[/,[[ '[[,  [[[[, ,[[[[, [[cccc   
			$$$$$$$$"$$$c$$$cc$$$c  $$    "$$$"""$$$"$$c.    "$$c$$$cc$$$c $$$$$$$$"$$$ $$""""   
			888 Y88" 888o888   888, 88,    888   "88o`Y8bo,,,o88o888   888,888 Y88" 888o888oo,__ 
			MMM  M'  "MMMYMM   ""`  MMM    MMM    YMM  `'YMUP"YMMYMM   ""` MMM  M'  "MMM""""YUMMM
			""""";

		public MainPage()
		{
			InitializeComponent();
	
		}

		//public MainPage(string? userName)
		//{

		//	if (!string.IsNullOrEmpty(userName))
		//	{
		//		UserNameLabel.Text = $"Welcome, {userName}!";
		//	}
		//	else
		//	{
		//		UserNameLabel.Text = "Welcome to the Math Game!";
		//	}
		//}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			CreateGridButtons(); // Call the method to create grid buttons when the page appears
			WelcomeFiglet();
		}

		public void WelcomeFiglet()
		{
			// this method can be used to display a welcome message in a figlet style
			// a library Figlet.Net or similar to create the figlet text
			FigletWelcome.Text = figletWelcome;


		}

		private void CreateGridButtons()
		{
			var MethodName = typeof(MathGame.Operations)
				.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly)
				.Select(m => m.Name)
				.Distinct()
				.Append("Prev - Previous Game History")
				.Append("Q - Exit")
				.ToList();

			int methodCount = MethodName.Count;
			int rowCount = 10;
			int columnCount = (int)Math.Ceiling((double)methodCount / rowCount);

			// Grid grid = new Grid { };

			for (int i = 0; i < rowCount; i++)
			{
				GameGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			}

			for (int i = 0; i < columnCount; i++)
			{
				GameGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
			}

			for (int i = 0; i < methodCount; i++)
			{
				int row = i % rowCount;
				int column = i / rowCount;
				var button = new Button
				{
					Text = MethodName[i],
					//Style = (Style)Application.Current.Resources["Button"]
					
				};
				GameGrid.SetRow(button, row);
				GameGrid.SetColumn(button, column);
				//button.Clicked += (sender, e) =>
				//{
				//	// Handle button click event here
				//	DisplayAlert("Button Clicked", $"You clicked: {MethodName[i]}", "OK");
				//};

				// TOOO: change the name of method to navigate to the corresponding operation
				button.Clicked += OnGameSelectionClicked; // attach the event handler for button clicks
				//GameGrid.Children.Add(button);
				//GameGrid.Children.Add(button, column, row);
				GameGrid.IsVisible = true; // ensure the grid is visible


			}

			// Uncomment and adjust the following lines if needed
			// grid.Children.Add(new Label { Text = "Hello, World!" }, 0, 0);
			// grid.Children.Add(new Button { Text = "Click Me" }, 1, 0);
			// grid.Children.Add(new Entry { Placeholder = "Enter text" }, 0, 1);
			// grid.Children.Add(new Label { Text = "Another label" }, 1, 1);
			// Content = grid;
		}

		private void OnMathGameClicked(object sender, EventArgs e)
		{
			//CreateGridButtons();
			if (sender is Button button)
			{
				Button btn = (Button)sender;
				Navigation.PushAsync(new GameSelection());

				SemanticScreenReader.Announce($"{btn.Text} pressed.");
			}
			else
			{
				new ArgumentException("Argument passed not type button.");
			}
		}

		private void OnGameButtonClicked(object sender, EventArgs e)
		{
			if (sender is Button button)
			{
				string operation = button.Text.Trim().ToLower();
				if (operation == "prev - previous game history")
				{
					DisplayAlert("Game History", "Displaying previous game history...", "OK");
					// logic to display previous game history
				}
				else if (operation == "q" || operation == "exit")
				{
					Application.Current.Quit(); // close the application
				}
				else
				{
					DisplayAlert("Selected Operation", $"You selected: {operation}", "OK");
					// logic to handle the selected operation
				}
				//SemanticScreenReader.Announce();
			}
		}

		private void OnGameSelectionClicked(object sender, EventArgs e)
		{
			if (sender is Button button)
			{
				Button btn = (Button)sender;
				Navigation.PushAsync(new GamePage(btn.Text));

				SemanticScreenReader.Announce($"{btn.Text} pressed.");
			}
			else
			{
				new ArgumentException("Argument passed not type button.");
			}
			
		}

		private void OnGameHistoryClicked(object sender, EventArgs e)
		{
			// logic to display game history
			DisplayAlert("Game History", "Displaying game history...", "OK");
			// this could be replaced with actual logic to navigate to a game history page or display a list of previous games
			if (sender is Button button)
			{
				Button btn = new Button();
				Navigation.PushAsync(new PreviousHistory());

				SemanticScreenReader.Announce($"{btn.Text} pressed");
			}
			else
			{
				new ArgumentException("Argument passed not type button.");
			}
		}

		private void OnExitClicked(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			// logic to exit the application
			Application.Current.Quit(); // close the application
			Environment.Exit(0); // or use to terminate the process
			SemanticScreenReader.Announce(SemanticProperties.GetDescription(button));
		}

		//private void OnCounterClicked(object? sender, EventArgs e)
		//{
		//	count++;

		//	if (count == 1)
		//		CounterBtn.Text = $"Clicked {count} time";
		//	else
		//		CounterBtn.Text = $"Clicked {count} times";

		//	SemanticScreenReader.Announce(CounterBtn.Text);
		//}
	}
}
