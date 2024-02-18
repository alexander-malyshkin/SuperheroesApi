namespace SuperHeroes.Application.Queries.ViewModels;

/// <summary>
/// Represents the view model for a superhero
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="Favourite"></param>
public record SuperheroVm(int Id, string Name, bool Favourite);
