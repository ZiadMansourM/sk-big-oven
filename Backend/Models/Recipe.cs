namespace Backend.Models;

public class Recipe
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<string> Ingredients { get; set; }
    public List<string> Instructions { get; set; }
    public List<Category> Categories { get; set; }

    public Recipe(string name, List<string> ingredients, List<string> instructions, List<Category> categories)
    {
        Id = Guid.NewGuid();
        Name = name;
        Ingredients = ingredients;
        Instructions = instructions;
        Categories = categories;
    }
}