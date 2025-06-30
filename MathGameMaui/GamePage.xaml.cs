using MathGame;

namespace MathGameMaui;

public partial class GamePage : ContentPage
{
	public string GameSelect {get; set;}

	public GamePage(string gameSelect)
	{
		InitializeComponent();
		GameSelect = gameSelect;
		BindingContext = this;
	}
}