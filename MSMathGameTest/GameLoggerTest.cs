using System;
using MathGame.Model;
using Microsoft.VisualStudio.TestPlatform;
using UnitTest = Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute;

//using VSQualityTools = Microsoft.VisualStudio.QualityTools.UnitTestFramework;
//using VSTestPlatform = Microsoft.VisualStudio.TestPlatform;




namespace MSMathGameTest;

[UnitTest.TestClass]
public class GameLoggerTest
{
		
	[UnitTest.TestMethod]
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
		UnitTest.Assert.IsTrue(isValid, "The game stats should be valid.");
	
	}

	[UnitTest.TestMethod]
	public void FailTest_InvalidData()
	{
		// arrange
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

		// act
		bool isValid = ValidateGameStats(gameLogger);

		// assert
		UnitTest.Assert.IsFalse(isValid, "The game stats should be invalid due to incorrect values.");
	}

	private bool ValidateGameStats(GameLogger gameLogger)
	{
		if (string.IsNullOrWhiteSpace(gameLogger.userName)) return false;
		if (gameLogger.score < 0) return false;
		if (gameLogger.correctAnswer < 0 || gameLogger.wrongAnswer < 0) return false;

		return true;
	}


}
