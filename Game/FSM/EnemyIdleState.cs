using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph02;

namespace ProgFineAnno
{
    class EnemyIdleState : State
    {
        private Enemy owner;

        private float minX;
        private float maxX;

        private RandomTimer CheckForNewPlayers;

        public EnemyIdleState(Enemy anOwner)
        {
            owner = anOwner;

            minX = 1;
            maxX = Game.Win.OrthoWidth - 1;

            CheckForNewPlayers = Game.RandomTimer(0, 2);
        }
        protected virtual void BuildPathToRandomNode()
        {
            Node endNode = owner.WorldGrid.GetRandomFreeNode();
            owner.BuildPath(endNode);
        }

        public override void OnEnter()
        {
            float startSpeedX = RandomGenerator.GetRandomInt(0, 2) == 0 ? owner.Speed : -owner.Speed;
            //owner.Velocity = new Vector2(startSpeedX * 0.3f, 0.01f);
            owner.IsIdle = true;

        }

        public override void Update()
        {
            Player playerToFight = owner.GetBestPlayerToFight();

            if (playerToFight != null && playerToFight.HasKey)
            {//there's visible player
                owner.Rival = playerToFight;

                StateMachine.GoTo(StateKey.CHASE);
                return;
            }
            //PerformPatrolling();
            owner.Position = owner.Position;
        }

        protected virtual void PerformPatrolling()
        {
            float posX = MathHelper.Clamp(owner.Position.X, minX, maxX);

            if (posX == minX || posX == maxX)
            {
                owner.Position = new Vector2(posX, owner.Position.Y);
                owner.SetXVelocity(-owner.Velocity.X);
            }
        }
    }
}
