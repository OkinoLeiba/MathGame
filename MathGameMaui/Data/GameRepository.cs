using SQLite;

namespace MathGameMaui.Data;

public  class GameRepository
{
	private readonly SQLiteAsyncConnection _databaseSync;
	private readonly SQLiteConnection _database; // for synchronous operations if needed


	public GameRepository(string dbPath)
	{
		_databaseSync = new SQLiteAsyncConnection(dbPath);
		_database = new SQLiteConnection(dbPath); // for synchronous operations if needed
		_databaseSync.CreateTableAsync<Model.Game>().Wait(); // ensure the table is created
		_database.CreateTable<Model.Game>(); // ensure the table is created synchronously
	}

	public async Task<List<Model.Game>> GetGamesAsync()
	{
		// return await _database.FindAsync<Model.Game>(id);
		return await _databaseSync.Table<Model.Game>().ToListAsync();
	}

	public List<Model.Game> GetGames()
	{
		// return await _database.Find<Model.Game>(id);
		return _database.Table<Model.Game>().ToList();
	}

	public async Task<List<Model.Game>> GetGamesByTypeAsync(string gameType)
	{
		return await _databaseSync.Table<Model.Game>().Where(g => g.GameSelect == gameType).ToListAsync();
	}

	public List<Model.Game> GetGamesByType(string gameType)
	{
		return _database.Table<Model.Game>().Where(g => g.GameSelect == gameType).ToList();
	}

	public async Task<Model.Game> GetGameByIdAsync(int id)
	{
		return await _databaseSync.FindAsync<Model.Game>(id);
	}

	public Model.Game GetGameById(int id)
	{
		return _database.Find<Model.Game>(id);
	}

	public async Task<List<Model.Game>> GetGamesByDateAsync(DateTime date)
	{
		return await _databaseSync.Table<Model.Game>().Where(g => g.Date.Date == date.Date).ToListAsync();
	}

	public List<Model.Game> GetGamesByDate(DateTime date)
	{
		return _database.Table<Model.Game>().Where(g => g.Date.Date == date.Date).ToList();
	}

	// SaveGameAsync method will insert a new game or update an existing one based on the ID
	public async Task<int> SaveGameAsync(Model.Game game)
	{
		if (game.ID != 0)
		{
			return await _databaseSync.UpdateAsync(game);
		}
		else
		{
			return await _databaseSync.InsertAsync(game);
		}
	}

	public int SaveGame(Model.Game game)
	{
		if (game.ID != 0)
		{
			return _database.Update(game);
		}
		else
		{
			return _database.Insert(game);
		}
	}

	public async Task<int> DeleteGameIDAsync(Model.Game game)
	{
		return await _databaseSync.DeleteAsync(game);
	}

	public void DeleteGameID(Model.Game game)
	{
		_database.Delete(game);
	}

	public async Task<int> DeleteGameIDAsync(int id)
	{
		return await _databaseSync.DeleteAsync(GetGameById(id));
	}

	public void DeleteGameID(int id)
	{
		_database.Delete(GetGameById(id));
	}

}
