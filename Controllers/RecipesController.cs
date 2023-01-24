namespace Allspice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly RecipesService _recipesService;
    private readonly Auth0Provider _auth0provider;
    private readonly IngredientsService _ingredientsService;
    private readonly FavoritesService _favoritesService;

    public RecipesController(RecipesService recipesService, Auth0Provider auth0Provider, IngredientsService ingredientsService, FavoritesService favoritesService)
    {
        _recipesService = recipesService;
        _auth0provider = auth0Provider;
        _ingredientsService = ingredientsService;
        _favoritesService = favoritesService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Recipe>>> Get()
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            List<Recipe> recipes = _recipesService.Get(userInfo?.Id);
            return Ok(recipes);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


},
