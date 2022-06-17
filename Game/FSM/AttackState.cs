using OpenTK;
using System;

namespace ProgFineAnno
{
    class AttackState : State
    {
        private Actor owner;
        private RandomTimer CheckForNewPlayers;
        public AttackState(Actor anOwner)
        {
            owner = anOwner;
            CheckForNewPlayers = Game.RandomTimer(0, 2);
        }

        public override void OnEnter()
        {
            owner.Velocity = new Vector2(0, 0);
        }
        public override void OnExit()
        {
        }

        public override void Update()
        {

            //    Player currentRival = owner.Rival;

            //    CheckForNewPlayers.Tick();

            //    if (CheckForNewPlayers.IsOver())
            //    {
            //        CheckForNewPlayers.Reset();

            //        Player playerToFight = owner.GetBestPlayerToFight();

            //        if (playerToFight != null && playerToFight != owner.Rival)
            //        {
            //             owner.Rival = playerToFight;
            //             StateMachine.GoTo(StateKey.CHASE);
            //             return;
            //        }
            //        if(owner.Energy / owner.MaxEnergy < 0.5f)
            //        {
            //            //choice between ATTACK and RECHARGE
            //            PowerUp p = owner.GetVisiblePowerUp();

            //            if(p != null)
            //            {
            //                //recharge
            //                Vector2 powerUpDist = p.Position - owner.Position;
            //                float goToRecharge = 1 - powerUpDist.Length / owner.SightRadius; //è solo una variabile, senza quindi fare una somma dopo

            //                //attack
            //                //same energy => 1
            //                //Enemy MAX => 100
            //                //Player MAX => 0.01
            //                float continueAttacking = Math.Min( owner.Energy / owner.Rival.Energy, 1); //tra 0 e 1

            //                if(goToRecharge >= continueAttacking)
            //                {
            //                    owner.Target = p;
            //                    StateMachine.GoTo(StateKey.RECHARGE);
            //                    return;
            //                }
            //            }
            //        }
            //    }

            //    Vector2 distToPlayer = owner.Rival.Position - owner.Position;

            //    if(distToPlayer.Length > owner.AttackDistance*1.25f)
            //    {
            //        StateMachine.GoTo(StateKey.CHASE);
            //        return;
            //    }

            //    Vector2 newForward = Vector2.Lerp(owner.Forward, distToPlayer.Normalized(), Game.Win.deltaTime * 8);

            //    owner.Forward = newForward;

            //    owner.Shoot();
            //}
        }
    }
}