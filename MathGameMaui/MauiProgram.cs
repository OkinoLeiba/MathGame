using MathGameMaui.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace MathGameMaui;

/// <summary>
/// Class MauiProgram contains the entry point for the Maui application.
/// </summary>
/// <return>void</return>
public static class MauiProgram
{
	/// <summary>
	/// CreateMauiApp method initializes the Maui application.
	/// </summary>
	/// <return>void</return>
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("CaveatBrush-Regular.ttf", "CaveatBrushRegular");
			});


#if DEBUG
		builder.Logging.AddDebug();
#endif
		string dbPath = Path.Combine(FileSystem.AppDataDirectory, "mathgame.db");
		//builder.Services.AddSingleton<MauiApp>((Func<System.IServiceProvider, Microsoft.Maui.Hosting.MauiApp>)CreateMauiApp);
		//builder.Services.AddSingleton<Data.GameRepository>(new Data.GameRepository(dbPath));
		builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<GameRepository>(s, dbPath));
		// register services and other dependencies here
		return builder.Build();
	}
}
