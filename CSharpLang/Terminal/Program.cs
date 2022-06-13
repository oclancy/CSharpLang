using Terminal.Gui;
using NStack;
using System.Text;
using Terminal.Views;

Application.Init();
var top = Application.Top;




var sb = new StringBuilder();
TextWriter tw = new StringWriter(sb);
Console.SetOut(tw);


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

FrameView frameLeft = new FrameView("Program")
{
	Y = 1,
	Width = Dim.Percent(50),
	Height = Dim.Fill()
};

FrameView frameRight = new FrameView("Operation")
{
	Y = 1,
	X = Pos.Right(frameLeft),
	Width = Dim.Percent(50),
	Height = Dim.Fill()
};



var win = new Main();
win.SelectedItemChanged += Win_SelectedItemChanged;
win.SetOptions(new[] { "State" });

void Win_SelectedItemChanged(object? sender, ListViewItemEventArgs e)
{
	if (e?.Value == "State")
	{
		var right = new StateView();
		frameRight.Add(right.View);
	}
}
// Add some controls, 
frameLeft.Add(win.View);
top.Add(menu);
top.Add(frameLeft, frameRight);

static bool Quit()
{
	var n = MessageBox.Query(50, 7, "Quit Demo", "Are you sure you want to quit this demo?", "Yes", "No");
	return n == 0;
}


//var consoleText = new TextView()
//{
//	X = 1,
//	Y = 10,
//	Width = Dim.Fill(),
//	Height = Dim.Fill(),
//	Text = "TextView here"
//};


//var changeRight = new Button("_Change Right")
//{
//	X = Pos.Right(@do),
//	Y = Pos.Top(@do),
//};

//changeRight.Clicked += ChangeRight_Clicked;

//void ChangeRight_Clicked()
//{
//	if (win2.Visible)
//	{
//		//Application.Top.Remove(win2);
//		//Application.Top.Add(win3);
//		win2.Enabled = false;
//		win2.Visible = false;
//		win3.Enabled = true;
//		win3.Visible = true;
//	}
//	else if (win3.Visible)
//	{
//		//Application.Top.Remove(win3);
//		//Application.Top.Add(win2);
//		win3.Enabled = false;
//		win3.Visible = false;
//		win2.Enabled = true;
//		win2.Visible = true;
//	}
//}







Application.Run();
Application.Shutdown();