namespace Backend.Controller.Services;

public class JsonService : IService
{
    public Models.Category CreateCategory(string name)
    {
        throw new NotImplementedException();
    }

    public Models.Recipe CreateRecipe(string name, List<string> ingredients, List<string> instructions, List<Models.Category> categories)
    {
        throw new NotImplementedException();
    }

    public void DeleteCategory(Guid id)
    {
        throw new NotImplementedException();
    }

    public void DeleteRecipe(Guid id)
    {
        throw new NotImplementedException();
    }

    public Models.Category GetCategory(Guid id)
    {
        throw new NotImplementedException();
    }

    public Models.Recipe GetRecipe(Guid id)
    {
        throw new NotImplementedException();
    }

    public List<Models.Category> ListCategories()
    {
        throw new NotImplementedException();
    }

    public List<Models.Recipe> ListRecipes()
    {
        throw new NotImplementedException();
    }

    public Models.Category UpdateCategory(Guid id, string name)
    {
        throw new NotImplementedException();
    }

    public Models.Recipe UpdateRecipe(Guid id, string name, List<string> ingredients, List<string> instructions, List<Models.Category> categories)
    {
        throw new NotImplementedException();
    }
}

