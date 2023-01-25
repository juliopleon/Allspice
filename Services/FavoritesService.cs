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

    internal List<MyRecipe> GetFavorites(string accountId)
    {
        List<MyRecipe> favorites = _repo.GetFavorites(accountId);
        return favorites;
    }

    internal string Remove(int id, string userId)
    {
        Favorite favorite = _repo.GetOne(id);
        if (favorite == null)
        {
            throw new Exception("favorite has been already removed");
        }
        _repo.Remove(id);
        return $"Favorite at {id} was removed";
    }
}
