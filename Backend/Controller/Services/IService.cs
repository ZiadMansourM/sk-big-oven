namespace Backend.Controller.Services;

public interface IService
{
    // For Recipe Model
    public List<Models.Recipe> ListRecipes();
    public Models.Recipe GetRecipe(Guid id);
    public Models.Recipe UpdateRecipe(Guid id, string name, List<string> ingredients, List<string> instructions, List<Guid> categories);
    public Models.Recipe CreateRecipe(string name, List<string> ingredients, List<string> instructions, List<Guid> categories);
    public void DeleteRecipe(Guid id);
    // For Category Model
    public List<Models.Category> ListCategories();
    public Models.Category GetCategory(Guid id);
    public Models.Category UpdateCategory(Guid id, string name);
    public Models.Category CreateCategory(string name);
    public void DeleteCategory(Guid id);
}

