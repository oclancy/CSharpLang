using NStack;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terminal.Gui;

namespace Terminal.Views
{
    internal class ConsoleView
    {
        internal const string NAME = "Console";
        internal View View => textView;
        
        private StringBuilder StringBuilder { get; init; }

        private TextView textView;

        public ConsoleView(StringBuilder stringBuilder)
        {
            StringBuilder = stringBuilder;

            textView = new TextView();
            StringBuilder = stringBuilder;

            textView.Text = StringBuilder.ToString();
        }
    }
}
