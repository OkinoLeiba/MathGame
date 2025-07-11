

using System;

namespace MathGame;

public struct GameStateManager
{
	public enum GameState
	{
		
		InProgress,
		Completed,
		Failed,
		NotStarted,
		Loading,
		Started,
		Playing,
		Paused,
		MainMenu,
		GameOver
	}


}
