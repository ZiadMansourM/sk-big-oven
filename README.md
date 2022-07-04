# 🫡 Welcome to BigOven Using .NET 🤖
This is my first project using C# and .NET, So this README.md will record all the learning steps I have been through.

- [X] Read the C# Coding Conventions, [link](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions).
- [X] Read all the discussions and announcements, then did some further research to understand some.
- [X] Read exercise one, outlined what I know, and wrote my questions.
- [X] Read about .NET 6 new top-level statements 🤯.
- [X] How to structure a C# app, what is a solution and project. 
- [X] Searched for a design pattern that can help me minimize the coupling in my code.
- [X] Drew some drafts/diagrams trying to structure the project without over-complicating everything.
- [X] Finalized the design, then implemented a [boilerplate](https://github.com/ZiadMansourM/sk-big-oven/tree/be67f8941a37df143fdb9efe7ced0d947b4e7df7) to test it.
- [X] Read Spectre Console [docs](https://spectreconsole.net/) and [examples](https://github.com/spectreconsole/spectre.console/tree/main/examples).
- [X] Read System.Text.Json [docs](https://docs.microsoft.com/en-us/dotnet/api/system.text.json?view=net-6.0).
- [X] Implemented "List-Get-Update-Delete" for The Category Model.
- [ ] Implement "List-Get-Update-Delete" for The Recipe Model.
- [ ] Validation layer.
- [ ] Add .env file "Still hard coding filename" although it should have been the first thing in the environment config step.
- [ ] Unit testing.
- [ ] Exercise two.

--------

# Goal 🤔
We are expecting in the near future to change from Json to a Database, and from Console to a RestAPI. For sure, we don't want to go through the whole codebase changing / reimplementing every line. So, the solution is to use:

```C#
// Dependency inversion from SOLID
var service = new Backend.Controller.Services.JsonService();
var controller = new Backend.Controller.Controller(service);
var viewService = new Backend.Views.Services.ConsoleService(controller);
var router = new Backend.Views.Router(viewService);

router.Run();
```

As you can see above, we can easily change JsonService() with DatabaseService() or ConsoleService(controller) with RestService(controller). It is called [Dependency inversion](https://youtu.be/S9awxA1wNNY). Also, I highly recommend this [anti-coupling-playlist](https://youtube.com/playlist?list=PLC0nd42SBTaNuP4iB4L6SJlMaHE71FG6N).


# Structure of BigOven:
```Console
ziadh@Ziads-MacBook-Air BigOven % tree -I obj -I bin
.
├── Backend
│   ├── Backend.csproj
│   ├── Categories.json
│   ├── Controller
│   │   ├── Controller.cs
│   │   └── Services
│   │       ├── IService.cs
│   │       └── JsonService.cs
│   ├── Exceptions
│   │   ├── CategoryNotFoundException.cs
│   │   └── RecipeNotFoundException.cs
│   ├── Models
│   │   ├── Category.cs
│   │   └── Recipe.cs
│   ├── Program.cs
│   └── Views
│       ├── Router.cs
│       └── Services
│           ├── ConsoleService.cs
│           ├── Davinci.cs
│           └── IViewService.cs
├── BigOven.sln
└── README.md

7 directories, 16 files
```

# Example

![Home Page Interactive](./images/First.png)

![Second Page Interactive](./images/Second.png)

![Third Page Interactive](./images/Third.png)
