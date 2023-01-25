
namespace Allspice.Repositories;

public class IngredientsRepository
{
    private readonly IDbConnection _db;

    public IngredientsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Ingredient Create(Ingredient ingredientData)
    {
        string sql = @"
        INSERT INTO ingredients
        (name, creatorId, quantity, recipeId)
        VALUES
        (@name, @creatorId, @quantity, @recipeId);
        SELECT LAST_INSERT_ID();
        ";
        int id = _db.ExecuteScalar<int>(sql, ingredientData);
        ingredientData.Id = id;
        return ingredientData;
    }


    //NOTE - this is a simple way to get a parent info to a child
    internal List<Ingredient> GetIngredientsByRecipe(int recipeId)
    {
        string sql = @"
        SELECT
        i.*
        FROM ingredients i
        WHERE recipeId = @recipeId;
        ";
        List<Ingredient> ingredients = _db.Query<Ingredient>(sql, new { recipeId }).ToList();

        return ingredients;
    }

    internal Ingredient GetOne(int id)
    {
        string sql = @"
        SELECT
        *
        FROM ingredients
        WHERE id = @id;
        ";
        return _db.Query<Ingredient>(sql, new { id }).FirstOrDefault();
    }

    internal void remove(int id)
    {
        string sql = @"
        DELETE FROM ingredients
        WHERE id = @id;
        ";
        _db.Execute(sql, new { id });
    }

}
