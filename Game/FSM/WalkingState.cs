using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph02;
using OpenTK;
using Aiv.Fast2D;
using Aiv.Audio;

namespace ProgFineAnno
{
    class WalkingState : State
    {
        private Player owner;
        private int mouseIndex = 0;
        private AudioSource source;
        private AudioClip walksClip;

        public WalkingState(Player anOwner)
        {
            owner = anOwner;
            source = new AudioSource();
            walksClip = AudioMgr.GetClip("walk");
            
        }
        public override void OnEnter()
        {
            BuildPathToMouse();
            //owner.AnimationWalkR.Start();
            if(owner.Velocity.X!=0 || owner.Velocity.Y != 0)
            {
                owner.CurrentAnimation.Start();
                owner.IsIdle = false;

            }
        }
        public override void OnExit()
        {
            source.Stop();
            base.OnExit();
        }
        public override void Update()
        {
            source.Stream(walksClip, Game.DeltaTime * 100, true);

            if (owner.WorldGrid.GetNodeAtPosition(CameraMgr.MainCamera.position - CameraMgr.MainCamera.pivot + Game.Win.mousePosition) != owner.LastNode)
            {
                if (Game.Win.mouseRight)
                {
                    BuildPathToMouse();
                    return;
                }
            }
            if (owner.FollowPath(owner.Speed))
            {
                BuildPathToMouse();
            }
        }
        protected void BuildPathToMouse()
        {
            mouseIndex++;
            //Node mouseNode = owner.WorldGrid.GetNodeAtPosition(Game.Win.mousePosition);

            Node mouseNode = owner.WorldGrid.GetNodeAtPosition(CameraMgr.MainCamera.position - CameraMgr.MainCamera.pivot + Game.Win.mousePosition); //*2.666f
            //Node mouseNode = owner.WorldGrid.GetNodeAtPosition(Game.Win.mousePosition * 2.6666f);

            if (mouseNode != null && mouseIndex == 1)
            {
                owner.BuildPath(mouseNode);
                owner.FollowPath(owner.Speed);
                
            }
            else
            {
                source.Stop();
                StateMachine.GoTo(StateKey.IDLE);
                mouseIndex = 0;
            }
                
        }
        
    }
}
