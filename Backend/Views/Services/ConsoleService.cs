namespace Backend.Views.Services;

public class ConsoleService : IViewService
{
    private readonly Controller.Controller _controller;
    private Dictionary<string, Action> _modeSelector;

    public ConsoleService(Controller.Controller controller)
    {
        _controller = controller;
        _modeSelector = new Dictionary<string, Action>
        {
            ["Recipes"] = Davinci.RecipesMain,
            ["Categories"] = Davinci.CategoriesMain,
            ["Exit"] = () => Environment.Exit(0),
        };
    }

    public void Run()
    {
        Davinci.Setup("BigOven ...");
        Davinci.WriteDivider("Talk to me ^^");
        while (true)
            _modeSelector[Davinci.GetMode()]();
    }
}
