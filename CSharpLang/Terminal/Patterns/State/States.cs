using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal.Patterns.State
{
    internal abstract class BaseState : IState
    {
        public Context Context { get; set; }


        internal virtual void Do()
        {
            Console.WriteLine($"{GetType().Name}::{nameof(Do)}");
        }

        internal abstract IState TryTransitionTo<T>(T newState) where T : IState;

        void IState.Do()
        {
            Do();
        }

        IState IState.TryTransitionTo<T>(T newState)
        {
            return TryTransitionTo(newState);
        }

        //internal BaseState(Context context)
        //{
        //    Context = context;
        //}
    }

    internal class PlayState : BaseState
    {
        //internal PlayState(Context context)
        //    : base(context) { }

        internal override void Do()
        {
            Context.Counter++;
            Console.WriteLine($"increase counter to {Context.Counter}");
        }

        internal override IState TryTransitionTo<T>(T newState) => newState switch
        {
            PlayState => this,
            StopState => Context.SetState(newState),
            PauseState => Context.SetState(newState),
            _ => throw new NotImplementedException()
        };
    }

    internal class PauseState : BaseState
    {
        //internal PauseState(Context context)
        //    : base(context) { }

        internal override IState TryTransitionTo<T>(T newState) => newState switch
        {
            PauseState => this,
            PlayState => Context.SetState(newState),
            StopState => Context.SetState(newState),
            _ => throw new NotImplementedException()
        };


    }

    internal class StopState : BaseState
    {
        //internal StopState(Context context)
        //    : base(context) { }

        internal override IState TryTransitionTo<T>(T newState) => throw new NotImplementedException();

    }
}
