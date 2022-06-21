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
        }
    }
}
