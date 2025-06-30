using MathGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MathGame
{
	internal class GameLogManager
	{
		public List<string> gameHistory = new List<string>();

		public List<string> GameHistory {  get { return gameHistory; } }

		public GameLogManager() 
		{ 
			gameHistory = new List<string>();
		}

		GameSelection gameSelection = new GameSelection();
		GameIntro gameIntro = new GameIntro();


		GameLogger gameLog = new GameLogger
			(
				userName: "",
				date: DateTime.Now,
				score: 0,
				maxScore: 0,
				minScore: 0,
				totalScore: 0,
				averageScore: 0,
				correctAnswer: 0,
				wrongAnswer: 0,
				gameCount: 0,
				gameWon: new LinkedList<Dictionary<string, int>>(),
				gameLost: new LinkedList<Dictionary<string, int>>(),
				update: DateTime.Now
			);

		//public bool CreateGameLog()
		//{



		//	gameLog.UserName = gameIntro.Name.ToString();
		//	gameLog.Date = DateTime.Now;
		//	gameLog.Score = gameIntro.Score;
		//	gameLog.MaxScore = gameIntro.Score;
		//	gameLog.MinScore = gameIntro.Score;
		//	gameLog.TotalScore = gameIntro.Score;
		//	gameLog.CorrectAnswer =	gameIntro.CorrectAnswer;
		//	gameLog.WrongAnswer = gameIntro.WrongAnswer;
		//	gameLog.GameCount = gameIntro.CorrectAnswer + gameIntro.WrongAnswer;
		//	gameLog.GameWon.AddLast(new Dictionary<string, int>() { { gameSelection.GameSelect, 1 } });
		//	gameLog.GameLost.AddLast(new Dictionary<string, int>() { { gameSelection.GameSelect, 1 } });
		//	gameLog.Update = DateTime.Now;


		//	return false;
		//}

		public GameLogger CreateUpdateGameLog(
			string userName,
			DateTime date,
			int score, 
			int correctAnswer, 
			int wrongAnswer, 
			int gameCount)
		{
			gameLog.UserName = userName;
			gameLog.Date = date;
			gameLog.Score = score;
			gameLog.MaxScore = score;
			gameLog.MinScore = score;
			gameLog.TotalScore = score;
			gameLog.AverageScore = score;
			gameLog.CorrectAnswer = correctAnswer;
			gameLog.WrongAnswer = wrongAnswer;
			gameLog.GameCount = gameCount;
			gameLog.Update = DateTime.Now;
			return gameLog;
		}

		public void UpdateGameWonAndLoss(string gameName, GameLogger gameLogger)
		{
			// the local dictionary will probably need to add to and return LinkedList of gamelog
			//Dictionary<string, int> localGameDictionary = new Dictionary<string, int>();


			// get method names from Operations class as game options for user
			//var MethodNames = typeof(Operations)
			//	.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly)
			//	.Select(m => m.Name)
			//	.Distinct()
			//	.ToList();

			//foreach (var method in MethodNames) 
			//{
			//	localGameDictionary.Add(method, 0);
			//}

			// two approaches to find dictionary item in LinkedList by game name and update score
			// both assign a reference to the GameWon and GameLost properties so changes will be reflected in the instance 
			// of the class and will not require return the value and replacing it in the property 
			Dictionary<string, int> gameWin = (Dictionary<string, int>) gameLogger.GameWon.Where(n => n.ContainsKey(gameName));
			gameWin[gameName] += 1;

			foreach (var game in gameLogger.GameWon)
			{
				if (game.ContainsKey(gameName)) game[gameName] += 1; 
			}

			Dictionary<string, int> gameLose = (Dictionary<string, int>)gameLogger.GameWon.Where(n => n.ContainsKey(gameName));
			gameLose[gameName] += 1;

			foreach (var game in gameLogger.GameLost)
			{
				if (game.ContainsKey(gameName)) game[gameName] += 1;
			}

		}

		public void UpdateGameScore(int gameScore, GameLogger gameLogger)
		{
			gameLogger.Score = gameScore;
		}

		//public virtual string ToString()
		//{
		//	return $"{gameIntro.Date} - {gameSelection.GameSelect}: {gameIntro.Score}";
		//}
		public override string ToString()
		{
			string localRefGame = $"{gameIntro.Date} - {gameSelection.GameSelect}: {GameIntro.Score}";
			return localRefGame;
			//return base.ToString(localRefGame);
		}

		public void UpdateGameHistory(string gameName)
		{
			string localRefGame = $"{gameIntro.Date} - {gameSelection.GameSelect}: {GameIntro.Score}";
			//gameHistory.Add("31/05/2025 16:38:27 - Addition : 0");
			//bool test = localRefGame.Contains(gameName);
			//bool tst = GameHistory.Any(h => h.Contains("gameName"));
			
			//TODO: consider how to replace string...use gameLog properties or pass value as argument to be replaced
			GameHistory.Remove(GameHistory.Find(h => h.Contains(gameName)));
			if (GameHistory.Any(h => h.Contains(gameName)) == true)
			{ 
				GameHistory.Remove(GameHistory.Find(h => h.Contains(gameName)));
				GameHistory.Add($"{gameIntro.Date} - {gameName}: {GameIntro.Score}");
		
			}
			else GameHistory.Add($"{gameIntro.Date} - {gameSelection.GameSelect}: {GameIntro.Score}");
			
		}
	}
}
