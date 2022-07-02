namespace Backend.Views.Services;

public class ConsoleService : IViewService
{
    private readonly Controller.Controller _controller;
    private readonly Dictionary<string, Action> _modeSelector;

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
        Davinci.Init(_controller);
        Davinci.Setup("BigOven ...");
        Davinci.WriteDivider("Talk to me ^^", true);
        while (true)
            _modeSelector[Davinci.GetMode()]();
    }
}
