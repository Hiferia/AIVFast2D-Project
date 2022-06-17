using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFineAnno
{
    enum RigidBodyType { Player=1, PlayerBullet=2, Enemy=4, EnemyBullet=8,
        TileObj = 16,
        Tile = 32,
        Key = 64
    }

    class RigidBody
    {
        protected uint collisionsMask;

        public RigidBodyType Type;
        public Vector2 Velocity;

        public GameObject GameObject;
        public bool IsCollisionsAffected;


        public Collider Collider { get; set; }

        public Vector2 Position { get { return GameObject.Position; } set { GameObject.Position = value; } }

        public bool IsActive { get { return GameObject.IsActive; } set { GameObject.IsActive = value; } }

        public RigidBody(GameObject owner)
        {
            GameObject = owner;
            PhysicsMgr.AddItem(this);
            IsCollisionsAffected = true;
        }

        public virtual void Update()
        {
            GameObject.Position += Velocity * Game.DeltaTime;
        }

        public virtual bool Collides(RigidBody other, ref Collision collisionInfo)
        {
            return Collider.Collides(other.Collider, ref collisionInfo);
        }

        //add RigidBodyType to collisions types list
        public void AddCollisionType(uint add)
        {
            collisionsMask |= add;
        }

        //checks if type is in collisions types list
        public bool CollisionTypeMatches(RigidBodyType type)
        {
            return ((uint)type & collisionsMask) != 0;
        }
        public virtual void Destroy()
        {
            GameObject = null;
            if (Collider != null)
            {
                Collider.Destroy();
                Collider = null;
            }
            PhysicsMgr.RemoveItem(this);
        }

    }
}
