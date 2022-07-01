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

    public Models.Recipe UpdateRecipe(Guid id, string name, List<string> ingredients, List<string> instructions, List<Models.Category> categories)
    {
        return _service.UpdateRecipe(id, name, ingredients, instructions, categories);
    }

    public Models.Recipe CreateRecipe(string name, List<string> ingredients, List<string> instructions, List<Models.Category> categories)
    {
        return _service.CreateRecipe(name, ingredients, instructions, categories);
    }

    public void DeleteRecipe(Guid id)
    {
        _service.DeleteRecipe(id);
    }

    // For Category Model
    public List<Models.Category> ListCategories()
    {
        return _service.ListCategories();
    }

    public Models.Category GetCategory(Guid id)
    {
        return _service.GetCategory(id);
    }

    public Models.Category UpdateCategory(Guid id, string name)
    {
        return _service.UpdateCategory(id, name);
    }

    public Models.Category CreateCategory(string name)
    {
        return _service.CreateCategory(name);
    }

    public void DeleteCategory(Guid id)
    {
        _service.DeleteCategory(id);
    }
}

