using MathGameMaui.Data;

namespace MathGameMaui;

public partial class PreviousHistory : ContentPage
{
	public PreviousHistory()
	{
		InitializeComponent();
		App.GameRepository.GetGamesAsync(); // load previous games from the repository
		//gameList.ItemsSource = App.GameRepository.GetGames(); // bind the game list to the UI
		//App.Current?.On<MathGameMaui.App>().GameRepository.GetGamesAsync(); // load previous games from the repository
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		gameList.ItemsSource = App.GameRepository.GetGames(); // refresh the game list when the page appears
	}

	protected override void OnDisappearing() {
		base.OnDisappearing();
		// optionally clear the game list or perform any cleanup here
		gameList.ItemsSource = null; // clear the list when the page disappears
	}

	private void OnDeleteClicked(object sender, EventArgs e)
	{

		//Button button = (Button)sender;
		//App.GameRepository.DeleteGameID((int)button.BindingContext); // delete all games from the repository
		//gameList.ItemsSource = App.GameRepository.GetGames();

		if (gameList.SelectedItem is Model.Game selectedGame)
		{
			App.GameRepository.DeleteGameIDAsync(selectedGame.ID).ContinueWith(task =>
			{
				if (task.IsCompletedSuccessfully)
				{
					DisplayAlert("Success", "Game history deleted successfully.", "OK");
					gameList.ItemsSource = App.GameRepository.GetGamesAsync().Result; // refresh the list
				}
				else
				{
					DisplayAlert("Error", $"Failed to delete game history: {task.Exception?.Message}", "OK");
				}
			});
		}
		else
		{
			DisplayAlert("No Selection", "Please select a game to delete.", "OK");
		}

		
	}

	private void OnGameSelected(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.Count > 0 && e.CurrentSelection[0] is Model.Game selectedGame)
		{
			// Navigate to the game details page or perform any action with the selected game
			DisplayAlert("Game Selected", $"You selected: {selectedGame.GameSelect} with score {selectedGame.Score} out of {selectedGame.GameCount}", "OK");
		}
		else
		{
			DisplayAlert("No Selection", "Please select a game from the list.", "OK");
		}
	}
}