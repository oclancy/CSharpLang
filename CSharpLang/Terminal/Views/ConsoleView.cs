using NStack;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terminal.Gui;

namespace Terminal.Views
{
    internal class ConsoleView : IDisposable, IView
    {
        internal const string NAME = "Console";
        public View View => textView;
        
        private StringBuilder StringBuilder { get; init; }

        public string Id => NAME;


        private TextView textView;

        private CancellationTokenSource tokenSource = new CancellationTokenSource();

        public ConsoleView()
        {

            StringBuilder = new StringBuilder();
            TextWriter tw = new StringWriter(StringBuilder);
            Console.SetOut(tw);

            textView = new TextView() { 
                Width=Dim.Fill(),
                Height=Dim.Fill(),
                ReadOnly=true,
            };

            var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
            Task.Run( async () =>
            {
                while (!tokenSource.Token.IsCancellationRequested)
                {
                    textView.Text = StringBuilder.ToString();
                    await timer.WaitForNextTickAsync();
                }
            });
        }

        public void Dispose()
        {
            tokenSource.Cancel();
        }
    }
}
