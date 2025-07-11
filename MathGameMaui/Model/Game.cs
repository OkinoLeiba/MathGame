using static MathGame.OperationsEnum;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using SQLite;

namespace MathGameMaui.Model;

[SQLite.Table("Games")]
public class Game
{
	
	[PrimaryKey, AutoIncrement, SQLite.Column("Id")]
	public int ID { get; set; }
	public string GameSelect { get; set; } = string.Empty;
	public int Score { get; set; }
	public int GameCount { get; set; }
	public int CorrectAnswer  => Score; 
	public int WrongAnswer => GameCount - Score;
	public int AverageScore => GameCount == 0 ? 0 : Score / GameCount;
	public DateTime Date { get; set; } = DateTime.Now;


	//public Game(string gameSelect, int score, int gameCount)
	//{
	//	GameSelect = gameSelect;
	//	Score = score;
	//	GameCount = gameCount;
	//}
}
