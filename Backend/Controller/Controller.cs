namespace Backend.Controller;

public class Controller
{
    private readonly Services.IService _service;

    public Controller(Services.IService service)
    {
        ArgumentNullException.ThrowIfNull(service);
        _service = service;
    }

    // For Recipe Model
    public List<Models.Recipe> ListRecipes()
    {
        return _service.ListRecipes();
    }

    public Models.Recipe GetRecipe(Guid id)
    {
        return _service.GetRecipe(id);
    }

    public Models.Recipe UpdateRecipe(List<Models.Recipe> recipes, int id)
    {
        return _service.UpdateRecipe(recipes, id);
    }

    public Models.Recipe CreateRecipe(string name, List<string> ingredients, List<string> instructions, List<Guid> categories)
    {
        return _service.CreateRecipe(name, ingredients, instructions, categories);
    }

    public void DeleteRecipe(int id)
    {
        _service.DeleteRecipe(id);
    }

    // For Category Model
    public List<Models.Category> ListCategories()
    {
        return _service.ListCategories();
    }

    public Models.Category GetCategory(int id)
    {
        return _service.GetCategory(id);
    }

    public Models.Category UpdateCategory(int id, string name)
    {
        return _service.UpdateCategory(id, name);
    }

    public Models.Category CreateCategory(string name)
    {
        return _service.CreateCategory(name);
    }

    public void DeleteCategory(int id)
    {
        _service.DeleteCategory(id);
    }

    // Relation
    public void DeleteCascade(int id)
    {
        _service.DeleteCascade(id);
    }
}

