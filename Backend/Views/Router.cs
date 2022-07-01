namespace Backend.Views;

public class Router
{
    private readonly Controller.Controller _controller;
    private readonly Services.IViewService _service;

    public Router(Controller.Controller controller, Services.IViewService service)
    {
        ArgumentNullException.ThrowIfNull(controller);
        ArgumentNullException.ThrowIfNull(service);
        _controller = controller;
        _service = service;
    }

    public void Run()
    {
        Console.WriteLine(value: "Good Job ^^");
    }
}

