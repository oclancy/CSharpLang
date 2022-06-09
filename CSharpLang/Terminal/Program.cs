using Terminal.Gui;
using NStack;
using System.Text;

Application.Init();
var top = Application.Top;

var sb = new StringBuilder();
TextWriter tw = new StringWriter(sb);
Console.SetOut(tw);

// Creates the top-level window to show
var win = new Window("MyApp")
{
	X = 0,
	Y = 1, // Leave one row for the toplevel menu

	// By using Dim.Fill(), it will automatically resize without manual intervention
	Width = Dim.Fill(),
	Height = Dim.Fill()
};

top.Add(win);

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
top.Add(menu);

static bool Quit()
{
	var n = MessageBox.Query(50, 7, "Quit Demo", "Are you sure you want to quit this demo?", "Yes", "No");
	return n == 0;
}

//var login = new Label("Login: ") { X = 3, Y = 2 };
//var password = new Label("Password: ")
//{
//	X = Pos.Left(login),
//	Y = Pos.Top(login) + 1
//};
//var loginText = new TextField("")
//{
//	X = Pos.Right(password),
//	Y = Pos.Top(login),
//	Width = 40
//};

var consoleText = new TextView()
{
	X = 1,
	Y = 10,
	Width = Dim.Fill(),
	Height = Dim.Fill(),
	Text = "TextView here"
};

var apply = new Button(1, 4, "_Apply");
apply.Clicked += Apply_Clicked;

void Apply_Clicked()
{
    Console.WriteLine("Clicked");
	consoleText.Text = sb.ToString();
}


var @do = new Button("_Do") {
	X = Pos.Right(apply),
	Y = Pos.Top(apply),
};

// Add some controls, 
win.Add(
		// The ones laid out like an australopithecus, with Absolute positions:
	new Label(1, 2, "State"),
	new RadioGroup(1, 3, new ustring[] { "_Start", "_Pause", "_Stop" }, 0) { DisplayMode = DisplayModeLayout.Horizontal,  },
	apply,
	@do,
	consoleText
);





Application.Run();
Application.Shutdown();