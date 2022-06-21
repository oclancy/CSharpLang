using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terminal.Patterns.State;

namespace Terminal.Patterns
{
    public class Context
    {
        public IState State { get; private set; }
        public int Counter { get; internal set; }

        public IState TryTransitionTo(IState newState)
        {
            newState.Context = this;

            try
            {
                if (State is null)
                    SetState(newState);
                else
                    State.TryTransitionTo(newState);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return State;
        }

        internal IState SetState(IState state)
        {
            State= state;
            return State;
        }

        public override string ToString()
        {
            return $"Current State is {State}, Counter is {Counter}";
        }

        internal void Do()
        {
            State.Do();
            Console.WriteLine(this);
        }
    }
}
