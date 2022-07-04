using Spectre.Console;
namespace Backend.Views.Services;

public class Davinci
{
    private static Controller.Controller? _controller;
    private static readonly Dictionary<string, Action> _recipeSelector = new()
    {
        ["List"] = ListRecipes,
        ["Get"] = GetRecipe,
        ["Create"] = CreateRecipe,
        ["Update"] = UpdateRecipe,
        ["Delete"] = DeleteRecipe,
    };
    private static readonly Dictionary<string, Action> _categorySelector = new()
    {
        ["List"] = ListCategories,
        ["Get"] = GetCategory,
        ["Create"] = CreateCategory,
        ["Update"] = UpdateCategory,
        ["Delete"] = DeleteCategory,
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
        AnsiConsole.Markup("[grey](Press Enter to add continue adding or write DONE to exit)[/]\n");
        List<string> flagList = new();
        int i = 1;
        while (true)
        {
            string input = AnsiConsole.Prompt(new TextPrompt<string>($"\n{i} - ")).Trim();
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
            .AddColumns("[grey]id[/]", "[grey]Name[/]")
            .RoundedBorder()
            .BorderColor(Color.Grey);
        int i = 1;
        foreach(Models.Category category in categories)
        {
            table = table.AddRow($"[grey]{i}[/]", category.Name);
            i++;
        }
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
            .AddColumns("[grey]Id[/]", "[grey]Title[/]", "[grey]Ingredients[/]", "[grey]Instructions[/]", "[grey]Categories[/]")
            .RoundedBorder()
            .BorderColor(Color.Grey);
        int i = 1;
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
            // Draw
            table = table.AddRow(
                $"{i}",
                $"[grey]{recipe.Name}[/]",
                String.Join("\n", recipe.Ingredients),
                String.Join("\n", recipe.Instructions),
                String.Join("\n", categoryNames)
            );
            i++;
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

    public static int GetRecipeId()
    {
        int count = _controller.ListRecipes().Count;
        List<string> strings = new();
        for (int i = 1; i <= count; i++)
            strings.Add($"{i}");
        return int.Parse(AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Which one '[green]Pick id[/]' ?")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more Modes)[/]")
            .AddChoices(strings)
        ));
    }

    public static int GetCategoryId()
    {
        int count = _controller.ListCategories().Count;
        List<string> strings = new();
        for (int i = 1; i <= count; i++)
            strings.Add($"{i}");
        return int.Parse(AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Which one '[green]Pick id[/]' ?")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more Modes)[/]")
            .AddChoices(strings)
        ));
    }

    // Recipes
    public static void ListRecipes()
    {
        List<Models.Recipe> recipes = _controller.ListRecipes();
        DrawRecipes(recipes, "Recipes List");
    }

    public static void GetRecipe()
    {
        int id = GetRecipeId();
        List<Models.Recipe> recipes = _controller.ListRecipes();
        DrawRecipes(recipes[id-1].ToList(), "Here you are ^^");
    }

    public static List<Guid> GetGuidsList()
    {
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
        return GuidsList;
    }

    public static void CreateRecipe()
    {
        // [Get Title]
        string name = Ask<string>("What's the [green]recipe name[/]?").Trim();
        // [validate doesn't exist]
        List<Models.Recipe> respies = _controller.ListRecipes();
        foreach (Models.Recipe r in respies)
        {
            if (name == r.Name)
            {
                AnsiConsole.Markup($"Recipe already, [red]exists[/]!\n");
                return;
            }
        }
        // [Get ingredients]
        List<string> ingredients = AskMultiLine("ingredients");
        // [Get instructions]
        List<string> instructions = AskMultiLine("instructions");
        // [get guids of categories]
        List<Guid> guidsList = GetGuidsList();
        // Create recipe
        Models.Recipe recipe = _controller.CreateRecipe(name, ingredients, instructions, guidsList);
        DrawRecipes(recipe.ToList(), "Created Successfully ^^");
    }
    
    public static void UpdateRecipe()
    {
        int id = GetRecipeId();
        List<Models.Recipe> recipes = _controller.ListRecipes();
        string fieldName = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("What do you [green] want to do today ?[/]")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more Modes)[/]")
            .AddChoices(new[] {
                "Name", "Ingredients", "Instructions", "Categories"
            })
        );
        switch (fieldName)
        {
            case "Name":
                string name = Ask<string>("What's the Recipe [green]Name[/]?").Trim();
                List<Models.Recipe> respies = _controller.ListRecipes();
                foreach (Models.Recipe r in respies)
                {
                    if (name == r.Name)
                    {
                        AnsiConsole.Markup($"Recipe already, [red]exists[/]!\n");
                        return;
                    }
                }
                recipes[id - 1].Name = name;
                break;
            case "Ingredients":
                List<string> ingredients = AskMultiLine("ingredients");
                recipes[id - 1].Ingredients = ingredients;
                break;
            case "Instructions":
                List<string> instructions = AskMultiLine("instructions");
                recipes[id - 1].Instructions = instructions;
                break;
            case "Categories":
                List<Guid> guidsList = GetGuidsList();
                recipes[id - 1].CategoriesIds = guidsList;
                break;
        }
        Models.Recipe recipe = _controller.UpdateRecipe(recipes, id);
        DrawRecipes(recipe.ToList(), "Created Successfully ^^");
    }

    public static void DeleteRecipe()
    {
        int id = GetRecipeId();
        _controller.DeleteRecipe(id);
        DrawRecipes(_controller.ListRecipes(), "Done ^^");
    }

    // Categories
    public static void ListCategories()
    {
        List<Models.Category> categories = _controller.ListCategories();
        DrawCategories(categories, "Categories List");
    }

    public static void GetCategory()
    {
        int id = GetCategoryId();
        Models.Category category = _controller.GetCategory(id);
        DrawCategories(category.ToList(), "Here you are ^^");
    }

    public static void CreateCategory()
    {
        string name = Ask<string>("What's the [green]Category name?[/]").Trim();
        List<Models.Category> categories = _controller.ListCategories();
        foreach(Models.Category c in categories)
        {
            if (name == c.Name)
            {
                AnsiConsole.Markup($"Category already, [red]exists[/]!\n");
                return;
            }
        }
        Models.Category category = _controller.CreateCategory(name);
        DrawCategories(category.ToList(), "Created Successfully ^^");
    }

    public static void UpdateCategory()
    {
        int id = GetCategoryId();
        string name = Ask<string>("What's the new [green]Name?[/]").Trim();
        List<Models.Category> categories = _controller.ListCategories();
        foreach (Models.Category c in categories)
        {
            if (name == c.Name)
            {
                AnsiConsole.Markup($"Category already, [red]exists[/]!\n");
                return;
            }
        }
        Models.Category category = _controller.UpdateCategory(id, name);
        DrawCategories(category.ToList(), "Here you are ^^");
    }

    public static void DeleteCategory()
    {
        int id = GetCategoryId();
        // Delete from recipe
        _controller.DeleteCascade(id);
        _controller.DeleteCategory(id);
        DrawCategories(_controller.ListCategories(), "Done ^^");
    }
}