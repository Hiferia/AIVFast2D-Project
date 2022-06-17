using System;
using System.Collections.Generic;

namespace ProgFineAnno
{
    internal class StateMachine
    {
        private Dictionary<StateKey, State> states;
        private State currentState;

        public StateMachine()
        {
            states = new Dictionary<StateKey, State>();
            currentState = null;
        }

        public void AddState(StateKey key, State state)
        {
            state.SetMachine(this);
            states.Add(key, state);
        }

        public void Update()
        {
            if (currentState != null)
                currentState.Update();
        }

        public void GoTo(StateKey nextState)
        {
            if (currentState != null)
            {
                currentState.OnExit();
            }

            currentState = states[nextState];
            currentState.OnEnter();
        }
    }
}