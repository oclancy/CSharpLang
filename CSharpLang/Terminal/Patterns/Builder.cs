using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terminal.Patterns.State;

namespace Terminal.Patterns
{
    internal class StateBuilder<T> where T : IState, new()
    {
        T _state;

        internal static StateBuilder<T> Create() 
        {
            return new StateBuilder<T>();
        }

        private StateBuilder() 
        {
            _state = new T();
        }

        internal StateBuilder<T> WithContext(Context context)
        {
            (_state as BaseState).Context = context;
            return this;
        }

        internal IState Build()
        {
            return _state;
        }

    }

    internal class ContextBuilder
    {
        Context _context;

        internal static ContextBuilder Create()
        {
            return new ContextBuilder();
        }

        private ContextBuilder()
        {
            _context = new Context();
        }

        internal ContextBuilder WithInitialState<T>() where T : IState, new()
        {
            _context.TryTransitionTo(new T());
            return this;
        }

        internal Context Build()
        {
            return _context;
        }

    }
}
