using Terminal.Gui;
using NStack;
using System.Text;
using Terminal.Views;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Reflection;

Application.Init();
var top = Application.Top;

var configBuilder = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);


if (Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") == "development")
{
    configBuilder.AddUserSecrets(Assembly.GetExecutingAssembly());
}

var config = configBuilder.Build();

//Mediator mediator = new Mediator((t) =>
//{
//	Console.WriteLine($"Asked for type {t}");
//	return new();
//});

IDictionary<string, View> viewDictionary = new Dictionary<string, View>();




// Creates a menubar, the item "New" has a help menu.
var menu = new MenuBar(new MenuBarItem[] {
            new MenuBarItem ("_File", new MenuItem [] {
				//new MenuItem ("_New", "Creates new file", null),
				//new MenuItem ("_Close", "",null),
				new MenuItem ("_Quit", "", () => { if (Quit ()) top.Running = false; })
            }),
			//new MenuBarItem ("_Edit", new MenuItem [] {
			//	new MenuItem ("_Copy", "", null),
			//	new MenuItem ("C_ut", "", null),
			//	new MenuItem ("_Paste", "", null)
			//})
		});

var win = new Main();

View frameRight = new View("Operation")
{
    Y = 1,
    X = Pos.Right(win.View),
    Width = Dim.Percent(50),
    Height = Dim.Fill()
};

win.SelectedItemChanged += (object? sender, ListViewItemEventArgs e) =>
{
    if (!viewDictionary.TryGetValue(e.Value.ToString(), out var view))
    {
        if (e?.Value == StateView.NAME)
        {
            var right = new StateView();
            viewDictionary.Add(StateView.NAME, right.View);
            frameRight.Add(right.View);
        }
        else if (e?.Value == ConsoleView.NAME)
        {
            var right = new ConsoleView();
            viewDictionary.Add(ConsoleView.NAME, right.View);
            frameRight.Add(right.View);
        }
        else if (e?.Value == BuilderView.NAME)
        {
            var right = new BuilderView();
            viewDictionary.Add(BuilderView.NAME, right.View);
            frameRight.Add(right.View);
        }
        else if (e?.Value == AdventureWorksView.NAME)
        {
            var right = new AdventureWorksView();
            viewDictionary.Add(AdventureWorksView.NAME, right.View);
            frameRight.Add(right.View);
        }

    }
    else
    {
        frameRight.BringSubviewToFront(view);
    }
};

// Add some controls, 
top.Add(menu);

top.Add(win.View);

top.Add(frameRight);

win.SetOptions(new[] { StateView.NAME, BuilderView.NAME, ConsoleView.NAME, AdventureWorksView.NAME });

win.View.SetFocus();

static bool Quit()
{
    var n = MessageBox.Query(50, 7, "Quit Demo", "Are you sure you want to quit this demo?", "Yes", "No");
    return n == 0;
}

Application.Run();
Application.Shutdown();