namespace Allspice.Repositories;

public class RecipesRepository
{
    private readonly IDbConnection _db;

    public RecipesRepository(IDbConnection db)
    {
        _db = db;
    }
    internal List<Recipe> Get()
    {
        string sql = @"
        SELECT 
        re.*,
        ac.*
        FROM recipes re
        JOIN accounts ac ON ac.id = re.creatorId;
        ";
        List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
        {
            recipe.Creator = account;
            return recipe;
        }).ToList();

        return recipes;
    }

    internal Recipe GetOne(int id)
    {
        string sql = @"
        re.*,
        ac.*
        FROM recipes re
        JOIN accounts ac ON ac.id = re.creatorId
        WHERE re.id = @id;
        ";
        return _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
        {
            recipe.Creator = account;
            return recipe;
        }, new { id }).FirstOrDefault();
    }
}
