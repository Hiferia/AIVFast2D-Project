using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph02;

namespace ProgFineAnno
{
    class Player : Actor
    {
        private bool isFirePressed;

        protected StateMachine stateMachine;
        protected Animation animation;

        protected WalkAnimation walkAnimation;

        public bool IsCollided;


        //ANIMATIONS
        public AnimationBase CurrentAnimation;
        public AnimationBase CurrentIdleAnimation;

        public Dictionary<string, AnimationBase> Animations;

        public bool IsIdle;


        public bool HasKey;

        public Player(Vector2 position, World worldgrid = null) : base("playerIDLER", worldgrid, Game.PixelsToUnits(90), Game.PixelsToUnits(90))
        {
            RigidBody.Type = RigidBodyType.Player;
            sprite.position = position;

            Energy = 100;
            cannonOffset = new Vector2(sprite.pivot.X-5,5);
            Speed = 4;

            Animations = new Dictionary<string, AnimationBase>();
            Animations.Add("WalkR", new PlayerWalksR(this));
            Animations.Add("WalkL", new PlayerWalksL(this));
            Animations.Add("WalkD", new PlayerWalksD(this));
            Animations.Add("WalkU", new PlayerWalksU(this));
            Animations.Add("IdleR", new PlayerIdleR(this));
            Animations.Add("IdleL", new PlayerIdleL(this));
            Animations.Add("IdleD", new PlayerIdleD(this));
            Animations.Add("IdleU", new PlayerIdleU(this));
            CurrentAnimation = Animations["WalkR"];
            CurrentIdleAnimation = Animations["IdleD"];

            IsActive = true;
            IsIdle = true;

            RigidBody.AddCollisionType((uint)RigidBodyType.TileObj | (uint)RigidBodyType.Enemy | (uint)RigidBodyType.Key);

            stateMachine = new StateMachine();
            stateMachine.AddState(StateKey.WALKING, new WalkingState(this));
            stateMachine.AddState(StateKey.IDLE, new IdleState(this));

            stateMachine.GoTo(StateKey.IDLE);
        }

        public void AddAllToAllMgr()
        {
            UpdateMgr.AddItem(this);
            PhysicsMgr.AddItem(this.RigidBody);
            DrawMgr.AddItem(this);
            
            foreach (KeyValuePair<string, AnimationBase> each in Animations)
            {
                UpdateMgr.AddItem(Animations[each.Key]);
                DrawMgr.AddItem(Animations[each.Key]);
            }
        }

        public void Input()
        {
            //horizontal movement
            if (Game.Win.GetKey(KeyCode.D))
            {
                RigidBody.Velocity.X = Speed;
            }
            else if (Game.Win.GetKey(KeyCode.A))
            {
                RigidBody.Velocity.X = -Speed;
            }
            else
            {
                RigidBody.Velocity.X = 0;
            }

            //vertical movement
            if (Game.Win.GetKey(KeyCode.W))
            {
                RigidBody.Velocity.Y = -Speed;
            }
            else if (Game.Win.GetKey(KeyCode.S))
            {
                RigidBody.Velocity.Y = Speed;
            }
            else
            {
                RigidBody.Velocity.Y = 0;
            }

            if(RigidBody.Velocity.X!=0 && RigidBody.Velocity.Y != 0)
            {
                //he's moving diagonal
                Velocity = Velocity.Normalized() * Speed;
            }

            if (Game.Win.GetKey(KeyCode.Space))
            {
                if (!isFirePressed)
                {
                    isFirePressed = true;
                    Shoot();
                }
            }
            else if(isFirePressed)
            {
                isFirePressed = false;
            }

        }

        public override void Update()
        {
            if (IsActive)
            {
                stateMachine.Update();
                UpdateAnimationToDirection();
            }
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
        public override void Draw()
        {
            if (IsActive && IsIdle)
            {
                CurrentIdleAnimation.Start();
            }
        }

        public override void OnDie()
        {
            Game.IsRunning = false;
        }

        public override void OnCollide(Collision collisionInfo)
        {
            if(collisionInfo.Collider is Cup)
            {
                stateMachine.GoTo(StateKey.IDLE);
                Game.CurrentScene.NextScene = Game.WinScene;
                Game.CurrentScene.IsPlaying = false;
            }
            if (collisionInfo.Collider is Enemy)
            {
                Game.CurrentScene.NextScene = Game.GameOverScene;
                Game.CurrentScene.IsPlaying = false;
            }
            if(collisionInfo.Collider is Key)
            {
                HasKey = true;
                Game.keyGui.IsActive = true;
                ((CaveScene)Game.CurrentScene).ChangeMusic();
            }
            if (!IsCollided)
            {
                if (collisionInfo.Collider is TileObj)
                {
                    if (((TileObj)collisionInfo.Collider).IsInDoorHouse1)
                    {
                        stateMachine.GoTo(StateKey.IDLE);
                        Position = new Vector2(5.5f, 6.5f);
                        Game.CurrentScene.IsPlaying = false;
                    }
                    if (((TileObj)collisionInfo.Collider).IsDoorLocked)
                    {
                        if (HasKey)
                        {
                            Game.keyGui.IsActive = false;
                            stateMachine.GoTo(StateKey.IDLE);
                            Game.CurrentScene.NextScene = Game.IndoorScene1;
                            Game.CurrentScene.IsPlaying = false;
                        }
                    }
                    if (((TileObj)collisionInfo.Collider).IsInDoorHouse2)
                    {
                        stateMachine.GoTo(StateKey.IDLE);
                        Game.CurrentScene.NextScene = Game.OutDoorScene;
                        Position = new Vector2(34.5f, 7.5f);
                        Game.CurrentScene.IsPlaying = false;
                    }
                    if (((TileObj)collisionInfo.Collider).IsOutDoorHouse2)
                    {
                        stateMachine.GoTo(StateKey.IDLE);
                        Game.CurrentScene.NextScene = Game.IndoorScene2;
                        Position = new Vector2(5.5f, 6.5f);
                        Game.CurrentScene.IsPlaying = false;
                    }
                    if (((TileObj)collisionInfo.Collider).IsOutDoorCave)
                    {
                        stateMachine.GoTo(StateKey.IDLE);
                        Game.CurrentScene.NextScene = Game.CaveScene;
                        Game.CurrentScene.IsPlaying = false;
                    }
                    if (((TileObj)collisionInfo.Collider).IsInDoorCave)
                    {
                        stateMachine.GoTo(StateKey.IDLE);

                        Game.CurrentScene.NextScene = Game.OutDoorScene;
                        Position = new Vector2(30.5f, 7);

                        Game.CurrentScene.IsPlaying = false;
                    }
                }
                
            }
            else
            {
                IsCollided = false;
            }
        }
        
        protected virtual void OnAnimationEnd()
        {
            Stop();
        }

        public virtual void Start()
        {
            animation.Play();
            animation.IsActive = true;
            IsActive = true;

        }

        public virtual void Stop()
        {
            animation.Stop();
            animation.IsActive = false;
        }
    }
}
