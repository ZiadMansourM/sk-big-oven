using System.Text.Json;

namespace Backend.Controller.Services;

public class JsonService : IService
{
    private readonly string _fileName;

    public JsonService()
    {
        _fileName = "Categories.json";
        if (!File.Exists(_fileName))
            File.WriteAllText(_fileName, "[]");
    }

    public void OverWrite(List<Models.Category> categories)
    {
        string newString = JsonSerializer.Serialize(categories);
        File.WriteAllText(_fileName, newString);
    }

    public string Read()
    {
        return File.ReadAllText(_fileName);
    }

    public Models.Category Save(Models.Category category)
    {
        string oldString = Read();
        List<Models.Category> categories = JsonSerializer.Deserialize<List<Models.Category>>(oldString)!;
        categories.Add(category);
        OverWrite(categories);
        return category;
    }

    public Models.Category CreateCategory(string name)
    {
        return Save(new Models.Category(name));
    }

    public Models.Recipe CreateRecipe(string name, List<string> ingredients, List<string> instructions, List<Models.Category> categories)
    {
        throw new NotImplementedException();
    }

    public void DeleteCategory(Guid id)
    {
        List<Models.Category> categories = ListCategories();
        Models.Category category = categories.SingleOrDefault(c => c.Id == id)!;
        categories.Remove(category);
        OverWrite(categories);
    }

    public void DeleteRecipe(Guid id)
    {
        throw new NotImplementedException();
    }

    public Models.Category GetCategory(Guid id)
    {
        Models.Category category = ListCategories().FirstOrDefault(c => c.Id == id)!;
        return category;
    }

    public Models.Recipe GetRecipe(Guid id)
    {
        throw new NotImplementedException();
    }

    public List<Models.Category> ListCategories()
    {
        string jsonString = Read();
        List<Models.Category> categories = JsonSerializer.Deserialize<List<Models.Category>>(jsonString)!;
        return categories;
    }

    public List<Models.Recipe> ListRecipes()
    {
        throw new NotImplementedException();
    }

    public Models.Category UpdateCategory(Guid id, string name)
    {
        List<Models.Category> categories = ListCategories();
        Models.Category temp = new("temp");
        foreach (Models.Category category in categories)
        {
            if (category.Id == id)
            {
                category.Name = name;
                temp = category;
            }
        }
        OverWrite(categories);
        return temp;
    }

    public Models.Recipe UpdateRecipe(Guid id, string name, List<string> ingredients, List<string> instructions, List<Models.Category> categories)
    {
        throw new NotImplementedException();
    }
}

