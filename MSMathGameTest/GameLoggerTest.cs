using System;
using MathGame;
//using Microsoft.VisualStudio.TestPlatform;
using VSQualityTools = Microsoft.VisualStudio.QualityTools.UnitTestFramework;
using VSTestPlatform = Microsoft.VisualStudio.TestPlatform;




namespace MSMathGameTest;

[TestClass]
public class GameLoggerTest
{
		
	[TestMethod]
	public void PassTest_ValidateCorrectData()
	{
		// arrange
		GameLogger gameLog = new GameLogger
		(
			userName: "Player1",
			date: DateTime.Now,
			score: 150,
			maxScore: 200,
			minScore: 50,
			totalScore: 1000,
			averageScore: 100,
			correctAnswer: 15,
			wrongAnswer: 5,
			gameCount: 10,
			gameWon: new LinkedList<Dictionary<string, int>>(),
			gameLost: new LinkedList<Dictionary<string, int>>(),
			update: DateTime.Now 
		);

		//GameWon = new LinkedList<Dictionary<string, int>>();
		//GameLost = new LinkedList<Dictionary<string, int>>();

		// adding some dummy values to game results
		gameLog.GameWon.AddLast(new Dictionary<string, int> { { "Game1", 100 } });
		gameLog.GameWon.AddLast(new Dictionary<string, int> { { "Game2", 150 } });

		gameLog.GameLost.AddLast(new Dictionary<string, int> { { "Game3", 50 } });
		gameLog.GameLost.AddLast(new Dictionary<string, int> { { "Game4", 75 } });

		// act
		bool isValid = ValidateGameStats(gameLog);

		// assert
		Assert.IsTrue(isValid, "The game stats should be valid.");
	
	}

	[TestMethod]
	public void FailTest_InvalidData()
	{
		// rrange
		GameLogger gameLogger= new GameLogger
		(
			userName: null,  // invalid username
			score: -10,      // invalid score
			date: DateTime.Now,
			maxScore: 200,
			minScore: 50,
			totalScore: 1000,
			averageScore: 100,
			correctAnswer: 15,
			wrongAnswer: 5,
			gameCount: 10,
			gameWon: new LinkedList<Dictionary<string, int>>(),
			gameLost: new LinkedList<Dictionary<string, int>>(),
			update: DateTime.Now
		);

		// Act
		bool isValid = ValidateGameStats(gameLogger);

		// Assert
		Assert.IsFalse(isValid, "The game stats should be invalid due to incorrect values.");
	}

	private bool ValidateGameStats(GameLogger gameLogger)
	{
		if (string.IsNullOrWhiteSpace(gameLogger.userName)) return false;
		if (gameLogger.score < 0) return false;
		if (gameLogger.correctAnswer < 0 || gameLogger.wrongAnswer < 0) return false;

		return true;
	}


}
