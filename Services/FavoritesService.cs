namespace Allspice.Services;

public class FavoritesService
{
    private readonly FavoritesRepository _repo;
    private readonly RecipesService _recipesService;

    public FavoritesService(FavoritesRepository repo, RecipesService recipesService)
    {
        _repo = repo;
        _recipesService = recipesService;
    }

    internal Favorite Create(Favorite myFavoriteData)
    {
        Recipe recipe = _recipesService.GetOne(myFavoriteData.RecipeId, myFavoriteData.AccountId);
        Favorite favorite = _repo.Create(myFavoriteData);
        return favorite;
    }

    internal List<Favorite> GetFavorites(string accountId)
    {
        List<Favorite> favorites = _repo.GetFavorites(accountId);
        return favorites;
    }
}
