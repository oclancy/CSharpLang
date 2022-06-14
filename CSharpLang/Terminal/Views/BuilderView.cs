using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terminal.Gui;

namespace Terminal.Views
{
    internal class BuilderView
    {
        internal const string NAME = "Builder";

        public Window View { get; private set; }

        public BuilderView()
        {
            View = new Window(NAME);

            View.Add(
                new Label(1, 2, NAME)
            );
        }
    }
}
