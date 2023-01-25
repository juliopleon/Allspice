

namespace Allspice.Repositories;

public class FavoritesRepository
{
    private readonly IDbConnection _db;

    public FavoritesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Favorite Create(Favorite myFavoriteData)
    {
        string sql = @"
        INSERT INTO favorites
        (recipeId, accountId)
        VALUES
        (@recipeId, @accountId);
        SELECT LAST_INSERT_ID();
        ";
        int id = _db.ExecuteScalar<int>(sql, myFavoriteData);
        myFavoriteData.Id = id;
        return myFavoriteData;
    }

    internal List<MyRecipe> GetFavorites(string accountId)
    {
        string sql = @"
        SELECT 
        re.*,
        fa.*,
        ac.*
        FROM favorites fa
        JOIN recipes re ON re.id = fa.recipeId
        JOIN accounts ac ON re.creatorId = ac.id
        WHERE fa.accountId = @accountId;
        ";
        List<MyRecipe> favorites = _db.Query<MyRecipe, Favorite, Account, MyRecipe>(sql, (re, fa, ac) =>
        {
            re.FavoriteId = fa.Id;
            re.Creator = ac;
            return re;
        }, new { accountId }).ToList();
        return favorites;
    }

    internal Favorite GetOne(int id)
    {
        string sql = @"
        SELECT
        *
        FROM favorites
        WHERE id = @id;
        ";
        return _db.Query<Favorite>(sql, new { id }).FirstOrDefault();
    }

    internal void Remove(int id)
    {
        string sql = @"
        DELETE FROM favorites
        WHERE id =@id;
        ";
        _db.Execute(sql, new { id });
    }



}
