using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace MathGame;

internal partial class GameModel : Component		
{
	private string _name = default(string);
	private DateTime _date = default(DateTime);
	private int _score = 0;
	private string _gameType = default(string);

	internal string Name { get; set; }
	internal DateTime Date { get; set; } = DateTime.Now;
	internal int Score { get; set; }
	internal string GameType { get; set; } 

	internal GameModel
		(
			string name, 
			DateTime date,
			int score, 
			string gameType
		)
	{
		InitializeComponent();
		_name = name;
		_date = date;
		_score = score;
		_gameType = gameType;

	}

	internal GameModel(IContainer container)
	{
		container.Add(this);

		InitializeComponent();
	}
}
