

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

    internal List<Favorite> GetFavorites(string accountId)
    {
        string sql = @"
        SELECT 
        re.*,
        fa.*,
        ac.*
        FROM favorites fa
        JOIN recipes re ON re.id = fa.recipeId
        JOIN accounts ac ON re.creatorId = ac.id
        WHERE re.accountId = @accountId;
        ";
        List<Favorite> favorites = _db.Query<Favorite, Account, Favorite>(sql, (fa, ac, fa) =>
        {
            re.AccountId = fa.Id;
            re.Creator = ac;
            return re;
        }, new { accountId }).ToList();
        return favorites;
    }

}
