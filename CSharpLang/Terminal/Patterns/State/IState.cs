using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal.Patterns.State
{
    public interface IState
    {
        public void Do();
        public IState TryTransitionTo<T>(T newState) where T : IState;
        public Context Context { get; }
    }
}
