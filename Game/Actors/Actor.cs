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
    abstract class Actor : GameObject
    {
        
        protected int energy;
        protected int maxEnergy;
        protected Vector2 textureOffset;
        
        protected Vector2 cannonOffset;

        public float Speed = 2;

        public World WorldGrid;
        public NodePath Path;
        public NodeInfo NextNode;

        public Node LastNode
        {
            get
            {
                if (Path != null && Path.Length() > 0)
                {
                    return Path.At(Path.Length() - 1);
                }
                return null;
            }
        }
        public BulletType WeaponType { get; set; }

        public Actor(string textureName, World world = null, float width = 0, float height = 0) : base(textureName, DrawLayer.Playground, width, height)
        {
            RigidBody = new RigidBody(this);
            RigidBody.Collider = ColliderFactory.CreateBoxFor(this);
            //sprite = new Sprite(Game.PixelsToUnits(64), Game.PixelsToUnits(64));
            
            //textureOffset = new Vector2(Game.PixelsToUnits(16), 0);

            Energy = maxEnergy = 100;
            WorldGrid = world;
        }

        public void UpdateWorld(World worldGrid)
        {
            this.WorldGrid = worldGrid;
        }
        public Vector2 Velocity
        {
            get
            {//read
                return RigidBody.Velocity;
            }
            set
            {//write
                RigidBody.Velocity = value;//the value passed in player.Velocity
                if (RigidBody.Velocity.Length > Speed)
                {//if he's longer than speed, force velocity to speed
                    RigidBody.Velocity = RigidBody.Velocity.Normalized() * Speed;
                }
            }
        }

        public virtual void Reset()
        {
            IsActive = true;
            energy = maxEnergy;
        }

        public int Energy { get { return energy; } set { energy = Math.Min(value, maxEnergy); } }

        public bool IsAlive { get { return Energy > 0; } }

        public abstract void OnDie();

        protected virtual void Shoot()
        {

        }

        public virtual bool AddDamage(int damage)
        {
            Energy -= damage;

            if (energy <= 0)
            {
                OnDie();
                return true;
            }
            return false;
        }
        public virtual bool FollowPath(float speed)
        {
            if(Path!=null && Path.Length() > 0)
            {
                Vector2 nodeDist = NextNode.Position - Position;

                if(nodeDist.Length <= 0.1f)
                {
                    if(NextNode.Index < Path.Length() - 1)
                    {
                        int newIndex = NextNode.Index + 1;
                        NextNode.SetNode(Path.At(newIndex), newIndex);
                        nodeDist = NextNode.Position - Position;
                        Console.WriteLine(NextNode.Node);
                    }
                    else
                    {
                        Velocity = Vector2.Zero;
                        return true;
                    }
                }
                Velocity = nodeDist.Normalized() * speed;
                //if (Math.Abs(Velocity.X) > Math.Abs(Velocity.Y))
                //{
                //    Velocity = new Vector2(Velocity.X, 0);
                //}
                //else
                //{
                //    Velocity = new Vector2(0, Velocity.Y);

                //}

            }
            return false;
        }
        public virtual bool BuildPath(Node endNode)
        {
            Node currentNode = WorldGrid.GetNodeAtPosition(Position);

            Path = GreedyAlgo.AStar_ShortestPath(currentNode, endNode);

            if(Path == null || Path.Length() == 0)
            {
                return false;
            }

            Console.WriteLine($"Go to: {endNode}");
            if (Path.Length() > 1)
            {//go to the second node
                NextNode.SetNode(Path.At(1), 1);

            }
            else
            {//just one node
                NextNode.SetNode(Path.At(0), 0);
            }

            //NextNode.SetNode(Path.At(0), 0);

            return true;
        }
        protected void OnWallCollide(Collision collisionInfo)
        {
            if(collisionInfo.Delta.X < collisionInfo.Delta.Y)
            {
                //horizontal collision
                if(Position.X < collisionInfo.Collider.Position.X)
                {
                    //collision by left
                    collisionInfo.Delta.X = -collisionInfo.Delta.X;
                }

                Position = new Vector2(Position.X + collisionInfo.Delta.X, Position.Y);
                RigidBody.Velocity.X = 0;
            }
            else
            {
                //vertical collision
                //if (Position.Y < collisionInfo.Collider.Position.Y)
                //{
                //    //collision from top
                //    collisionInfo.Delta.Y = -collisionInfo.Delta.Y;
                //    OnGroundTouch();
                //}
                //else
                //{
                //    RigidBody.Velocity.Y = 1;
                //}

                Position = new Vector2(Position.X, Position.Y + collisionInfo.Delta.Y);

            }
        }
        public override void Draw()
        {
            sprite.DrawTexture(texture);
        }

    }
    
}
