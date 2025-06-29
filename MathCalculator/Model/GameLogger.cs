using System;
using System.Collections.Generic;
using System.Net.Http.Headers;



namespace MathGame.Model
{
	public struct GameLogger
	{
		public string? userName = default;
		public DateTime date;
		public int score;
		public int maxScore;
		public int minScore;
		public int totalScore;
		public int averageScore;
		public int correctAnswer;
		public int wrongAnswer;
		public int gameCount;
		public LinkedList<Dictionary<string, int>> gameWon = new LinkedList<Dictionary<string, int>>();
		public LinkedList<Dictionary<string, int>> gameLost = new LinkedList<Dictionary<string, int>>();
		public DateTime update;


		public string? UserName { get; set; }
		public DateTime Date { get; set; } = DateTime.Now;
		public int Score { get; set; }
		public int MaxScore
		{
			get => score;
			set => score = Math.Max(score, value);
		}
		public int MinScore
		{
			get => score;
			set => score = Math.Min(score, value);
		}
		public int TotalScore { 
			get => totalScore; 
			set => Score += value; 
		}
		public int AverageScore
		{
			get => averageScore;
			set
			{
				try
				{
					averageScore = TotalScore / GameCount;
				}
				catch (DivideByZeroException dze)
				{
					Console.WriteLine(dze.Message);
				}
			}
		}
		public int CorrectAnswer { get; set; }
		public int WrongAnswer { get; set; }
		public int GameCount { get; set; }
		public LinkedList<Dictionary<string, int>> GameWon { get; set; } = new LinkedList<Dictionary<string, int>>();
		public LinkedList<Dictionary<string, int>> GameLost { get; set; } = new LinkedList<Dictionary<string, int>>();
		public DateTime Update { get; set; }


		public GameLogger
			(
			string? userName, 
			DateTime date, 
			int score, 
			int maxScore,
			int minScore, 
			int totalScore, 
			int averageScore, 
			int correctAnswer, 
			int wrongAnswer, 
			int gameCount, 
			LinkedList<Dictionary<string, int>> gameWon, 
			LinkedList<Dictionary<string, int>> gameLost, 
			DateTime update
			)
		{
			UserName = userName;
			Date = date;
			Score = score;
			MaxScore = maxScore;
			MinScore = minScore;
			TotalScore = totalScore;
			AverageScore = averageScore;
			CorrectAnswer = correctAnswer;
			WrongAnswer = wrongAnswer;
			GameCount = gameCount;
			GameWon = gameWon;
			GameLost = gameLost;
			Update = update;
			UserName = userName;
			Date = date;
			Score = score;
			MaxScore = maxScore;
			MinScore = minScore;
			TotalScore = totalScore;
			AverageScore = averageScore;
			CorrectAnswer = correctAnswer;
			WrongAnswer = wrongAnswer;
			GameCount = gameCount;
			GameWon = gameWon;
			GameLost = gameLost;
			Update = update;
		}
	}
}
