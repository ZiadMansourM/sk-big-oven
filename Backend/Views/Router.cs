namespace Backend.Views;

public class Router
{
    private readonly Services.IViewService _service;

    public Router(Services.IViewService service)
    {
        ArgumentNullException.ThrowIfNull(service);
        _service = service;
    }

    public void Run()
    {
        _service.Run();
    }
}

