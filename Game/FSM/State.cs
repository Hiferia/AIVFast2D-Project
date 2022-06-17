using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFineAnno
{
    abstract class State
    {
        protected StateMachine StateMachine;
        public void SetMachine(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        abstract public void Update();
        public virtual void OnExit() { }
        public virtual void OnEnter() { }
    }
}
