using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace ProgFineAnno
{
    class IdleState : State
    {
        private Player owner;
        private bool isMousePressed;

        public IdleState(Player anOwner)
        {
            owner = anOwner;

        }
        public override void OnEnter()
        {
            owner.Velocity = Vector2.Zero;
            if (owner.CurrentAnimation != owner.Animations["IdleR"] && owner.CurrentAnimation != null)
            {
                owner.CurrentAnimation.Stop();
            }
            owner.IsIdle = true;
        }
        public override void Update()
        {
            if (Game.Win.mouseRight && !isMousePressed)
            {
                isMousePressed = true;
                StateMachine.GoTo(StateKey.WALKING);
                return;
            }
            else
            {
                isMousePressed = false;
            }
        }
        
    }
}
