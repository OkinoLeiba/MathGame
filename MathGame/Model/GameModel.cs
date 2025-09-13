using System;
using System.ComponentModel;


namespace MathGame;

internal partial class GameModel : Component		
{
	// a static data model to hold all game instances
	// it is a simple implementation for now, used print to console
	// this can be extended to use a database or file storage in the future

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


	private void InitializeComponent()
	{
		// add initialization logic here if needed.
		// this method is required to resolve the CS0103 error.
		// ensures that the code compiles and can be extended later.
	}
}




	
	



