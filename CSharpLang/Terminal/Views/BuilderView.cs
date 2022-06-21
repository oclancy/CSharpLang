using NStack;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terminal.Gui;
using Terminal.Patterns;
using Terminal.Patterns.State;

namespace Terminal.Views
{
    internal class BuilderView : IView
    {
        Context _context;

        internal const string NAME = "Builder";

        public View View { get; private set; }

        public string Id => NAME;

        private RadioGroup Status { get; init; }

        public BuilderView()
        {
            View = new Window(NAME);

            var create = new Button("_Create")
            {
                X = 1,
                Y = 2,
            };
            create.Clicked += Create_Clicked;

            var update = new Button("_Update")
            {
                X = 1,
                Y = Pos.Bottom(create),
            };
            update.Clicked += Update_Clicked;

            var @do = new Button("_Do")
            {
                X = 1,
                Y = Pos.Bottom(update),

            };
            @do.Clicked += Do_Clicked;

            Status = new RadioGroup(1, 5, new ustring[] { "_Start", "_Pause", "Stop", "_Test" }, 0) { DisplayMode = DisplayModeLayout.Horizontal, };

            View.Add(
                create,
                update,
                @do,
                Status
            );
        }

        private void Update_Clicked()
        {
            IState status = Status.SelectedItem switch {
                0 => new PlayState(),
                1 => new PauseState(),
                _ => new StopState()
            };

            _context.TryTransitionTo(status);
        }

        private void Create_Clicked()
        {
            _context = ContextBuilder.Create()
                                 .WithInitialState<PauseState>()
                                 .Build();
        }

        private void Do_Clicked()
        {
            _context.Do();
        }
    }
}
