using Graph02;
using OpenTK;
using System;
using Aiv.Audio;

namespace ProgFineAnno
{
    class ChaseState : State
    {
        private Enemy owner;
        private RandomTimer CheckForNewPlayers;
        private RandomTimer RebuildPath;
        private AudioSource source;
        private AudioClip dogBark;

        public ChaseState(Enemy anOwner)
        {
            owner = anOwner;
            CheckForNewPlayers = Game.RandomTimer(0,2);
            RebuildPath = Game.RandomTimer(1,4);
            source = new AudioSource();
            dogBark = AudioMgr.GetClip("dogBark");
        }
        public override void OnEnter()
        {
            owner.IsIdle = false;

            if (owner.Rival != null && owner.Rival.IsActive)
            {
                BuildPathToRival();
            }
        }
        protected void BuildPathToRival()
        {
            Node rivalNode = owner.WorldGrid.GetNodeAtPosition(owner.Rival.Position);
            if (rivalNode != null)
            {
                owner.BuildPath(rivalNode);
                owner.FollowPath(owner.Speed);
            }
        }
        public override void Update()
        {
            source.Stream(dogBark, 100 * Game.DeltaTime, true);
            if(Game.CurrentScene is GameOverScene)
            {
                source.Stop();
            }
            CheckForNewPlayers.Tick();

            if (CheckForNewPlayers.IsOver())
            {

                CheckForNewPlayers.Reset();

                Player playerToFight = owner.GetBestPlayerToFight();

                if (playerToFight != null)
                {
                    owner.Rival = playerToFight;                   
                }
            }

            Vector2 distToPlayer = owner.Rival.Position - owner.Position;

            if (distToPlayer.Length > owner.SightRadius)
            {
                //he's too far
                StateMachine.GoTo(StateKey.ENEMYIDLE);
                return;
            }
            else if (distToPlayer.Length <= owner.AttackDistance)
            {
                //he's near enough to attack
                StateMachine.GoTo(StateKey.ATTACK);
                return;
            }
            else
            {
                //continue chase player
                RebuildPath.Tick();
                if (RebuildPath.IsOver())
                {
                    RebuildPath.Reset();
                   // Vector2 deltaRival = owner.Rival.Position - World.GetNodePosition(owner.LastNode);

                    if (owner.LastNode != owner.WorldGrid.GetNodeAtPosition(owner.Rival.Position))
                    {
                        BuildPathToRival();
                    }
                }
                if (owner.FollowPath(owner.Speed))
                {
                    BuildPathToRival();
                }
                owner.FollowPath(owner.Speed);
            }
           
        }
        public override void OnExit()
        {
            source.Stop();
            base.OnExit();
        }
    }
}