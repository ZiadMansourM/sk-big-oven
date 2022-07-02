var service = new Backend.Controller.Services.JsonService();
var controller = new Backend.Controller.Controller(service);
var viewService = new Backend.Views.Services.ConsoleService(controller);
var router = new Backend.Views.Router(viewService);

router.Run();