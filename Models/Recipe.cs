namespace Allspice.Models;
public class Recipe
{
    public int Id { get; set; }

    public string Title { get; set; }
    public string Instructions { get; set; }
    public string Img { get; set; } = "https://ca-times.brightspotcdn.com/dims4/default/78ccdc9/2147483647/strip/true/crop/1920x1080+0+0/resize/1200x675!/quality/80/?url=https%3A%2F%2Fcalifornia-times-brightspot.s3.amazonaws.com%2Fb1%2Ffa%2F991da28e44019f3f83d083d1a966%2Ffood-1920w-0000000.jpg";
    public string Category { get; set; }
    public bool Archived { get; set; }
    public string CreatorId { get; set; }

    public Account Creator { get; set; }
}

public class MyRecipe : Recipe
{
    public int FavoriteId { get; set; }
}
