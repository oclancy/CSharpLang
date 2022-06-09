using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terminal.Patterns.State;

namespace Terminal.Patterns
{
    internal static partial class StateBuilder
    {
        internal static IState Create<T>() where T : IState, new()
        {
            return new T();
        }

        internal static IState WithContext(this IState target, Context context)
        {
            (target as BaseState).Context = context;
            return target;
        }

    }
}
