using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph02;

namespace ProgFineAnno
{
    enum EnemyType { ShootingEnemy, Last}

    class Enemy : Actor
    {
        protected RandomTimer timer;

        protected StateMachine stateMachine;


        public Dictionary<string, AnimationBase> Animations;
        public AnimationBase CurrentAnimation;
        public AnimationBase CurrentIdleAnimation;
        public EnemyType Type { get; protected set; }

        public int Value { get; protected set; }

        public float SightRadius { get; protected set; }
        public float AttackDistance { get; protected set; }
        public float HalfConeAngle { get; protected set; }
        public bool IsIdle;

        public Player Rival;

        public uint ID;

        public GameObject Target;
        public Node LastNode
        {
            get
            {
                if(Path!=null && Path.Length() > 0)
                {
                    return Path.At(Path.Length() - 1);
                }
                return null;
            }
        }
        public Enemy(World world) : base("enemyIDLED", world, Game.PixelsToUnits(90), Game.PixelsToUnits(90))
        {
            RigidBody.Type = RigidBodyType.Enemy;
            RigidBody.AddCollisionType((uint)RigidBodyType.Player);

            Speed = 3;
            WorldGrid = world;

            cannonOffset = new Vector2(0.8f,0);
            timer = Game.RandomTimer(1, 3);
            SightRadius = 10;
            HalfConeAngle = MathHelper.PiOver6;//30 degrees
            Value = 5;

            Animations = new Dictionary<string, AnimationBase>();
            Animations.Add("WalkR", new EnemyWalkR(this));
            Animations.Add("WalkL", new EnemyWalkL(this));
            Animations.Add("WalkD", new EnemyWalkD(this));
            Animations.Add("WalkU", new EnemyWalkU(this));
            Animations.Add("IdleR", new EnemyIdleR(this));
            Animations.Add("IdleL", new EnemyIdleL(this));
            Animations.Add("IdleD", new EnemyIdleD(this));
            Animations.Add("IdleU", new EnemyIdleU(this));

            CurrentAnimation = Animations["WalkR"];
            CurrentIdleAnimation = Animations["IdleR"];
            IsIdle = true;
            stateMachine = new StateMachine();
            stateMachine.AddState(StateKey.ENEMYIDLE, new EnemyIdleState(this));
            stateMachine.AddState(StateKey.CHASE, new ChaseState(this));
            

            stateMachine.GoTo(StateKey.ENEMYIDLE);

            IsActive = true;
        }
        private void UpdateAnimationToDirection()
        {
            if (Math.Sign(Velocity.X) == Math.Sign(1) && Math.Abs(Velocity.Y) < Math.Abs(Velocity.X))
            {
                CurrentIdleAnimation.Stop();
                CurrentAnimation.IsActive = false;
                CurrentAnimation = Animations["WalkR"];
                CurrentIdleAnimation = Animations["IdleR"];
                CurrentAnimation.Start();
            }
            else if (Math.Sign(Velocity.X) == Math.Sign(-1) && Math.Abs(Velocity.Y) < Math.Abs(Velocity.X))
            {
                CurrentIdleAnimation.Stop();
                CurrentAnimation.IsActive = false;
                CurrentAnimation = Animations["WalkL"];
                CurrentIdleAnimation = Animations["IdleL"];
                CurrentAnimation.Start();
            }
            else if (Math.Sign(Velocity.Y) == Math.Sign(1) && Math.Abs(Velocity.X) < Math.Abs(Velocity.Y))
            {
                CurrentIdleAnimation.Stop();
                CurrentAnimation.IsActive = false;
                CurrentAnimation = Animations["WalkD"];
                CurrentIdleAnimation = Animations["IdleD"];
                CurrentAnimation.Start();
            }
            else if (Math.Sign(Velocity.Y) == Math.Sign(-1) && Math.Abs(Velocity.X) < Math.Abs(Velocity.Y))
            {
                CurrentIdleAnimation.Stop();
                CurrentAnimation.IsActive = false;
                CurrentAnimation = Animations["WalkU"];
                CurrentIdleAnimation = Animations["IdleU"];
                CurrentAnimation.Start();
            }
        }
        public virtual Player GetBestPlayerToFight()
        {
            Player bestPlayer = null;

            List<Player> visiblePlayers = GetVisiblePlayers();

             
            if (visiblePlayers.Count == 1)
            {
                bestPlayer = visiblePlayers[0];
            }
            

            return bestPlayer;
        }
        public virtual List<Player> GetVisiblePlayers()
        {
            //List<Player> players = ((PlayScene)Game.CurrentScene).Players;
            List<Player> players = OutDoorScene.Players;
            List<Player> visiblePlayers = new List<Player>();

            for (int i = 0; i < players.Count; i++)
            {
                if (!players[i].IsAlive)
                {
                    continue;
                }

                Vector2 distToPlayer = players[i].Position - Position;

                if (distToPlayer.Length <= SightRadius)
                {
                    visiblePlayers.Add(players[i]);
                }
            }
            
            return visiblePlayers;
        }
        public override void Update()
        {
            if (IsActive)
            {
                stateMachine.Update();
                UpdateAnimationToDirection();

                base.Update();
            }
        }
        protected override void Shoot()
        {
            timer.Tick();
            if (timer.IsOver())
            {
                timer.Reset();
                Bullet bullet = BulletsMgr.GetBullet(WeaponType);
                if (bullet == null) return;
                Vector2 pos = sprite.position + Forward * cannonOffset.Length;

                bullet.Shoot(pos, Forward, this);
            }
        }

        public override void OnDie()
        {
            //owner.ReturnBack(this);
            IsActive = false;
            //energyBar.IsActive = false;
        }

        public virtual void OnCollide(Collision collisionInfo)
        {
            if (collisionInfo.Collider is Player)
            {
                //Player p = (Player)collisionInfo.Collider;
                //p.AddDamage((int)(Energy*0.025f));
                //OnDie();
            }
            else if (collisionInfo.Collider is Tile)
            {
                OnWallCollide(collisionInfo);
            }
            else if( collisionInfo.Collider is Enemy)
            {
                if(((Enemy)collisionInfo.Collider).ID < ID)
                {
                    OnWallCollide(collisionInfo);
                }
            }
        }
        public virtual void SetXVelocity(float x)
        {
            RigidBody.Velocity = new Vector2(x, RigidBody.Velocity.Y);
        }
        public override void Draw()
        {
            if (IsActive && IsIdle)
            {
                CurrentIdleAnimation.Start();
            }
        }
    }
}
