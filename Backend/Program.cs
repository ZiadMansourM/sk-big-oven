using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var service = new Backend.Controller.Services.JsonService(
    config["RecipesPath"],
    config["CategoriesPath"]
);
var controller = new Backend.Controller.Controller(service);
var viewService = new Backend.Views.Services.ConsoleService(controller);
var router = new Backend.Views.Router(viewService);

router.Run();