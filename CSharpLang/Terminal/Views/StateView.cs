using NStack;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terminal.Gui;

namespace Terminal.Views
{
    internal class StateView :IView
    {
        internal const string NAME = "State";
        internal Window View { get; private set; }

        public string Id => NAME;

        View IView.View => View;

        public StateView()
        {
            View = new Window(NAME);

            var apply = new Button(1, 4, "_Apply");
            apply.Clicked += Apply_Clicked;

            void Apply_Clicked()
            {
                Console.WriteLine("Clicked");
            }


            var @do = new Button("_Do")
            {
                X = Pos.Right(apply),
                Y = Pos.Top(apply),
            };

            View.Add(
                new Label(1, 2, "State"),
                new RadioGroup(1, 3, new ustring[] { "_Start", "_Pause", "_Stop" }, 0) { DisplayMode = DisplayModeLayout.Horizontal, },
                apply,
                @do
            );
        }
    }
}
