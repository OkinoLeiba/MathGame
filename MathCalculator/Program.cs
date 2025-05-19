// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;
using MathGame; 




internal class Program
{
	private static void Main(string[] args)
	{
		//Enum EnumMethodName;

		Console.WriteLine("Hello, World!");

		Console.WriteLine("What is your name, Chief...\n");
		string name = Console.ReadLine();

		DateTime date = DateTime.UtcNow;

		Console.WriteLine("-----------------------------------------------------------------------");
		Console.WriteLine($"Hello {name}, the date is {date}.\nDo you want to play a game with me?");

		// Get method names from Operations as game options
		var methodNames = typeof(Operations)
			.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly)
			.Select(m => m.Name)
			.Distinct()
			.ToList();

		//attempted to cast list<T> to enum
		//Enum.Parse(typeof(List<string>), methodNames.First());

		//attempted to dynamically add element of enum at runtime
		//methodNames.Select(s =>  EnumMethodName s = EnumMethodName(0)); 

		Console.WriteLine("What game would you like to play today with me?");
		for (int i = 0; i < methodNames.Count; i++)
		{
			if (i == 59) i =+ 12;
			Console.WriteLine($"{(char)('A' + i)} - {methodNames[i]}");
		}
		;

		string gameSelect = Console.ReadLine();

		var charList = Enumerable.Range(0, 50)
	.Select(i => (char)('A' + i))
	.ToList();
	}
}



//enum EMethodName = methodNames.Select(s => Enum.Parse(typeof(Enum), s)).ToList();


