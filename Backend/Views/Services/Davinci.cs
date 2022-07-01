using Spectre.Console;
namespace Backend.Views.Services;

public class Davinci
{
    private static readonly Dictionary<string, Action> _recipeSelector = new()
    {
        ["List"] = Davinci.ListRecipes,
        ["Get"] = Davinci.GetRecipe,
        ["Create"] = Davinci.CreateRecipe,
        ["Update"] = Davinci.UpdateRecipe,
        ["Delete"] = Davinci.DeleteRecipe,
    };
    private static readonly Dictionary<string, Action> _categorySelector = new()
    {
        ["List"] = Davinci.ListCategories,
        ["Get"] = Davinci.GetCategory,
        ["Create"] = Davinci.CreateCategory,
        ["Update"] = Davinci.UpdateCategory,
        ["Delete"] = Davinci.DeleteCategory,
    };

    public static void Setup(string name)
    {
        AnsiConsole.Write(
            new Panel(
                new FigletText(name)
                .Centered()
                .Color(Color.Red)
            ).RoundedBorder()
        );
    }

    public static void WriteDivider(string text)
    {
        AnsiConsole.WriteLine();
        AnsiConsole.Write(
            new Rule($"[yellow]{text}[/]")
            .RuleStyle("grey")
            .LeftAligned()
        );
    }

    public static string GetMode()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("What do you [green] want to do today ?[/]")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more Modes)[/]")
            .AddChoices(new[] {
                "Recipes", "Categories", "Exit"
            })
        );
    }

    public static void RecipesMain()
    {
         string request = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("About [red]recipes[/], [green] pick what want?[/]")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more Options)[/]")
            .AddChoices(new[] {
                "List", "Get", "Create", "Update", "Delete"
            })
        );
        _recipeSelector[request]();
    }

    public static void CategoriesMain()
    {
        string request = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("About categories, [green] pick what want?[/]")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more Options)[/]")
            .AddChoices(new[] {
                "List", "Get", "Create", "Update", "Delete"
            })
        );
        _categorySelector[request]();
    }

    // Recipes
    public static void ListRecipes()
    {
        throw new NotImplementedException();
    }

    public static void GetRecipe()
    {
        throw new NotImplementedException();
    }

    public static void CreateRecipe()
    {
        throw new NotImplementedException();
    }

    public static void UpdateRecipe()
    {
        throw new NotImplementedException();
    }

    public static void DeleteRecipe()
    {
        throw new NotImplementedException();
    }

    // Categories
    public static void ListCategories()
    {
        throw new NotImplementedException();
    }

    public static void GetCategory()
    {
        throw new NotImplementedException();
    }

    public static void CreateCategory()
    {
        throw new NotImplementedException();
    }

    public static void UpdateCategory()
    {
        throw new NotImplementedException();
    }

    public static void DeleteCategory()
    {
        throw new NotImplementedException();
    }
}