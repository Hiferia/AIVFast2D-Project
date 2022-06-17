using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFineAnno
{
    abstract class Bullet : GameObject
    {
        public BulletType Type { get; protected set; }

        protected int damage;

        protected float speed;

        protected Actor owner;


        public Bullet(string textureName, BulletType type):base(textureName, DrawLayer.Foreground)
        {
            Type = type;
            damage = 25;

            RigidBody = new RigidBody(this);
            RigidBody.AddCollisionType((uint)RigidBodyType.Tile);
            //RigidBody.Collider = ColliderFactory.CreateCircleFor(this);
        }

        public virtual void Shoot(Vector2 startPosition, Actor shotBy)
        {
            IsActive = true;
            sprite.position = startPosition;
            owner = shotBy;
        }

        public virtual void Shoot(Vector2 startPosition, Vector2 direction, Actor shotBy)
        {
            RigidBody.Velocity = direction.Normalized() * speed;
            Shoot(startPosition, shotBy);
        }
    }
}
