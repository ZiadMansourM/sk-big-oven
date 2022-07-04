namespace Backend.Controller.Services;

public interface IService
{
    // For Recipe Model
    public List<Models.Recipe> ListRecipes();
    public Models.Recipe GetRecipe(Guid id);
    public Models.Recipe UpdateRecipe(List<Models.Recipe> recipes, int id);
    public Models.Recipe CreateRecipe(string name, List<string> ingredients, List<string> instructions, List<Guid> categories);
    public void DeleteRecipe(int id);
    // For Category Model
    public List<Models.Category> ListCategories();
    public Models.Category GetCategory(int id);
    public Models.Category UpdateCategory(int id, string name);
    public Models.Category CreateCategory(string name);
    public void DeleteCategory(int id);
    // Relation
    public void DeleteCascade(int id);
}

