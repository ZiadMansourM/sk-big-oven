using System.Text.Json;

namespace Backend.Controller.Services;

public class JsonService : IService
{
    private readonly string _fileNameCategories;
    private readonly string _fileNameRecipes;

    public JsonService()
    {
        // Recipes
        _fileNameRecipes = "Recipes.json";
        if (!File.Exists(_fileNameRecipes))
            File.WriteAllText(_fileNameRecipes, "[]");
        // Categories
        _fileNameCategories = "Categories.json";
        if (!File.Exists(_fileNameCategories))
            File.WriteAllText(_fileNameCategories, "[]");
    }

    public void OverWriteCategories(List<Models.Category> categories)
    {
        string newString = JsonSerializer.Serialize(categories);
        File.WriteAllText(_fileNameCategories, newString);
    }

    public void OverWriteRecipes(List<Models.Recipe> recipes)
    {
        string newString = JsonSerializer.Serialize(recipes);
        File.WriteAllText(_fileNameRecipes, newString);
    }

    public string ReadCategories()
    {
        return File.ReadAllText(_fileNameCategories);
    }

    public string ReadRecipes()
    {
        return File.ReadAllText(_fileNameRecipes);
    }

    public Models.Category SaveCategories(Models.Category category)
    {
        string oldString = ReadCategories();
        List<Models.Category> categories = JsonSerializer.Deserialize<List<Models.Category>>(oldString)!;
        categories.Add(category);
        OverWriteCategories(categories);
        return category;
    }

    public Models.Recipe SaveRecipes(Models.Recipe recipe)
    {
        string oldString = ReadRecipes();
        List<Models.Recipe> recipes = JsonSerializer.Deserialize<List<Models.Recipe>>(oldString)!;
        recipes.Add(recipe);
        OverWriteRecipes(recipes);
        return recipe;
    }

    public Models.Category CreateCategory(string name)
    {
        return SaveCategories(new Models.Category(name));
    }

    public Models.Recipe CreateRecipe(string name, List<string> ingredients, List<string> instructions, List<Guid> categoriesIds)
    {
        return SaveRecipes(new Models.Recipe(name, ingredients, instructions, categoriesIds));
    }

    public void DeleteCategory(int id)
    {
        List<Models.Category> categories = ListCategories();
        categories.Remove(categories[id-1]);
        OverWriteCategories(categories);
    }

    public void DeleteRecipe(int id)
    {
        List<Models.Recipe> recipes = ListRecipes();
        recipes.Remove(recipes[id - 1]);
        OverWriteRecipes(recipes);
    }

    public Models.Category GetCategory(int id)
    {
        return ListCategories()[id-1];
    }

    public Models.Recipe GetRecipe(Guid id)
    {
        throw new NotImplementedException();
    }

    public List<Models.Category> ListCategories()
    {
        string jsonString = ReadCategories();
        List<Models.Category> categories = JsonSerializer.Deserialize<List<Models.Category>>(jsonString)!;
        return categories;
    }

    public List<Models.Recipe> ListRecipes()
    {
        string jsonString = ReadRecipes();
        List<Models.Recipe> recipes = JsonSerializer.Deserialize<List<Models.Recipe>>(jsonString)!;
        return recipes;
    }

    public Models.Category UpdateCategory(int id, string name)
    {
        List<Models.Category> categories = ListCategories();
        categories[id-1].Name = name;
        OverWriteCategories(categories);
        return categories[id-1];
    }

    public Models.Recipe UpdateRecipe(List<Models.Recipe> recipes, int id)
    {
        OverWriteRecipes(recipes);
        return recipes[id - 1];
    }

    public void DeleteCascade(int id)
    {
        Models.Category category = ListCategories()[id-1];
        string jsonString = ReadRecipes();
        List<Models.Recipe> recipes = JsonSerializer.Deserialize<List<Models.Recipe>>(jsonString)!;
        foreach (Models.Recipe recipe in recipes)
            recipe.CategoriesIds.Remove(category.Id);
        OverWriteRecipes(recipes);
    }
}

