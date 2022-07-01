Console.WriteLine(value: "Hello, World!");

var service = new Backend.Controller.Services.JsonService();
var controller = new Backend.Controller.Controller(service);
var viewService = new Backend.Views.Services.ConsoleService();
var router = new Backend.Views.Router(controller, viewService);

router.Run();