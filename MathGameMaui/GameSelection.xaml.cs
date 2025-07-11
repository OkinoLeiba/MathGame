
namespace MathGameMaui;

public partial class GameSelection : ContentPage
{
	public GameSelection()
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		CreateGridButtons(); // call the method to create grid buttons when the page appears
			
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

		// var buttonStyle = (Style)Application.Current.Resources["GameButton"];

		// took different approach, defined each property individually 
		// define the button style in the resources
		var buttonStyle = new Style(typeof(Button))
		{
			Setters =
		   	{
		   		new Setter { Property = Button.BackgroundColorProperty, Value = Colors.Chocolate },
		   		new Setter { Property = Button.FontFamilyProperty, Value = "Helvetica" },
		   		new Setter { Property = Button.FontSizeProperty, Value = 28 },
		   		new Setter { Property = Button.VerticalOptionsProperty, Value = LayoutOptions.Center },
		   		new Setter { Property = Button.HorizontalOptionsProperty, Value = LayoutOptions.Center },
		   		new Setter { Property = Button.WidthRequestProperty, Value = 30 },
		   		new Setter { Property = Button.ScaleProperty, Value = 1.0 },
		   		new Setter { Property = Button.PaddingProperty, Value = new Thickness(8) }
		   	}
		};
	
		  //var buttonStyle = new { "BackgroundColor" : "Chocolate",
		  //					"FontFamily" : "Helvetica",
		  //					"FontSize" : 28,
		  //					"VerticalOptions" : "Center",
		  //					"HorizontalOptions" : "Center",
		  //					"WidthRequest" : 30,
		  //					"Scale" : 1.0,
		  //					"Padding" : 8};

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

			GameGrid.HorizontalOptions = LayoutOptions.Center;
			GameGrid.VerticalOptions = LayoutOptions.Center;
			GameGrid.BackgroundColor = Colors.Chocolate;
			GameGrid.WidthRequest = 1200; // set the maximum width of the grid
			//button.Clicked += (sender, e) =>
			//{
			//	// Handle button click event here
			//	DisplayAlert("Button Clicked", $"You clicked: {MethodName[i]}", "OK");
			//};

			
			button.Clicked += OnGameSelectionClicked; // attach the event handler for button clicks
			// GameGrid.Style = buttonStyle; // apply the button style from resources
			button.BackgroundColor = Colors.Chocolate;
			button.FontFamily = "Helvetica";
			button.FontSize = 24;
			button.VerticalOptions = LayoutOptions.Center;
			button.HorizontalOptions = LayoutOptions.Center;
			button.WidthRequest = 300;
			button.Scale = 1.0;
			button.Padding = new Thickness(8);
			// update the line causing the error
			GameGrid.Children.Add(button); // Add the button to the grid

			// Set the row and column for the button using the appropriate methods
			//Grid.SetRow(button, row);
			//Grid.SetColumn(button, column);

		}

		GameGrid.IsVisible = true; // ensure the grid is visible

		// uncomment and adjust the following lines if needed
		// grid.Children.Add(new Label { Text = "Hello, World!" }, 0, 0);
		// grid.Children.Add(new Button { Text = "Click Me" }, 1, 0);
		// grid.Children.Add(new Entry { Placeholder = "Enter text" }, 0, 1);
		// grid.Children.Add(new Label { Text = "Another label" }, 1, 1);
		// Content = grid;
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

}
