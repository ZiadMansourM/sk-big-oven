using Spectre.Console;
namespace Backend.Views.Services;

public class ConsoleService : IViewService
{
    private readonly Controller.Controller _controller;

    public ConsoleService(Controller.Controller controller)
    {
        _controller = controller;
    }

    public void Run()
    {
        AnsiConsole.Markup("[underline red]Hello[/] World!");
    }
}

