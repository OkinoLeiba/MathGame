using MathGameMaui.Data;

namespace MathGameMaui;

public partial class App : Application
{
	public static string AppName { get; } = "Math Game";
	public static string AppVersion { get; } = "1.0.0";
	public static string AppDescription { get; } = "A fun and interactive math game to test your math skills!";
	public static GameRepository GameRepository { get; private set; }

	public App(GameRepository gameRepository)
	{
		InitializeComponent();
		GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository), "GameRepository cannot be null.");
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}