using Spectre.Console;
namespace Backend.Views.Services;

public class Davinci
{
    private static Controller.Controller? _controller;
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

    public static void Init(Controller.Controller controller)
    {
        _controller = controller;
    }

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

    public static void WriteDivider(string text, bool center=false)
    {
        AnsiConsole.WriteLine();
        var rule = new Rule($"[yellow]{text}[/]")
            .RuleStyle("grey");
        if(center)
            rule = rule.Centered();
        else
            rule = rule.LeftAligned();
        AnsiConsole.Write(rule);
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

    public static T Ask<T>(string question)
    {
        return AnsiConsole.Prompt<T>(
            new TextPrompt<T>(question)
            .PromptStyle("red")
        );
    }

    public static List<string> AskMultiLine(string flag)
    {
        AnsiConsole.Markup($"What's the [green]recipe {flag}[/]?\n");
        AnsiConsole.Markup("[grey](Press Enter to add continue adding or write DONE to exit)[/]");
        List<string> flagList = new();
        int i = 1;
        while (true)
        {
            string input = AnsiConsole.Prompt(new TextPrompt<string>($"\n{i} - "));
            if (input.ToUpper() == "DONE")
                break;
            flagList.Add(input);
            i++;
        }
        return flagList;
    }

    public static void DrawCategories(List<Models.Category> categories, string message)
    {
        AnsiConsole.Write(
            new Rule($"[yellow]{message}[/]")
            .RuleStyle("grey")
            .LeftAligned()
        );
        var table = new Table()
            .AddColumns("[grey]Guid[/]", "[grey]Name[/]")
            .RoundedBorder()
            .BorderColor(Color.Grey);
        foreach(Models.Category category in categories)
            table = table.AddRow($"[grey]{category.Id}[/]", category.Name);
        AnsiConsole.Write(table);
    }

    public static void DrawRecipes(List<Models.Recipe> recipes, string message)
    {
        AnsiConsole.Write(
            new Rule($"[yellow]{message}[/]")
            .RuleStyle("grey")
            .LeftAligned()
        );
        var table = new Table()
            .AddColumns("[grey]Title[/]", "[grey]Ingredients[/]", "[grey]Instructions[/]", "[grey]Categories[/]")
            .RoundedBorder()
            .BorderColor(Color.Grey);
        foreach (Models.Recipe recipe in recipes)
        {
            // Prepare data
            List<Models.Category> categories = _controller.ListCategories();
            Dictionary<Guid, string> categoriesDict = new();
            foreach (Models.Category category in categories)
                categoriesDict.Add(category.Id, category.Name);
            List<string> categoryNames = new();
            foreach (Guid guidId in recipe.CategoriesIds)
                categoryNames.Add(categoriesDict[guidId]);
            table = table.AddRow(
                $"[grey]{recipe.Name}[/]",
                String.Join("\n", recipe.Ingredients),
                String.Join("\n", recipe.Instructions),
                String.Join("\n", categoryNames)
            );
        }
        AnsiConsole.Write(table);
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
        List<Models.Recipe> recipes = _controller.ListRecipes();
        DrawRecipes(recipes, "Recipes List");
    }

    public static void GetRecipe()
    {
        throw new NotImplementedException();
    }

    public static void CreateRecipe()
    {
        // [Get Title]
        string name = Ask<string>("What's the [green]recipe name[/]?");
        // [Get ingredients]
        List<string> ingredients = AskMultiLine("ingredients");
        // [Get instructions]
        List<string> instructions = AskMultiLine("instructions");
        // [get guids of categories]
        List<Models.Category> categories = _controller.ListCategories();
        DrawCategories(categories, "Categories List");
        Dictionary<string, Guid> categoriesNames = new();
        foreach (Models.Category category in categories)
            categoriesNames.Add(category.Name, category.Id);
        var Names = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
            .Title("choose [green]categories[/]?")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more categories)[/]")
            .InstructionsText(
                "[grey](Press [blue]<space>[/] to toggle a category, " +
                "[green]<enter>[/] to accept)[/]")
            .AddChoices(categoriesNames.Keys.ToList())
        );
        List<Guid> GuidsList = new();
        foreach (string n in Names)
            GuidsList.Add(categoriesNames[n]);
        // Create recipe
        Models.Recipe recipe = _controller.CreateRecipe(name, ingredients, instructions, GuidsList);
        DrawRecipes(recipe.ToList(), "Created Successfully ^^");
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
        List<Models.Category> categories = _controller.ListCategories();
        DrawCategories(categories, "Categories List");
    }

    public static void GetCategory()
    {
        Guid id = Ask<Guid>("What's the Category [green]Guid?[/]");
        Models.Category category = _controller.GetCategory(id);
        DrawCategories(category.ToList(), "Here you are ^^");
    }

    public static void CreateCategory()
    {
        string name = Ask<string>("What's the [green]Category name?[/]");
        Models.Category category = _controller.CreateCategory(name);
        DrawCategories(category.ToList(), "Created Successfully ^^");
    }

    public static void UpdateCategory()
    {
        Guid id = Ask<Guid>("What's the Category [green]Guid?[/]");
        string name = Ask<string>("What's the new [green]Name?[/]");
        Models.Category category = _controller.UpdateCategory(id, name);
        DrawCategories(category.ToList(), "Here you are ^^");
    }

    public static void DeleteCategory()
    {
        Guid id = Ask<Guid>("What's the Category [green]Guid?[/]");
        _controller.DeleteCategory(id);
        DrawCategories(_controller.ListCategories(), "Done ^^");
    }
}