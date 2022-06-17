using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace ProgFineAnno
{
    abstract class EnemyBullet : Bullet
    {
        public EnemyBullet(string texturePath, BulletType type) : base(texturePath, type)
        {
            RigidBody.Type = RigidBodyType.EnemyBullet;
            RigidBody.AddCollisionType((uint)RigidBodyType.Player);
        }

        public override void Shoot(Vector2 startPosition, Actor shotBy)
        {
            base.Shoot(startPosition, shotBy);
            sprite.position.X -= sprite.pivot.X;
        }

        public override void Update()
        {
            if (IsActive)
            {
                //left side check
                if (sprite.position.X + sprite.pivot.X <= 0)
                {
                    BulletsMgr.RestoreBullet(this);
                }
            }
        }

        public override void OnCollide(Collision collisionInfo)
        {
            if (collisionInfo.Collider is Player)
            {
                ((Player)collisionInfo.Collider).AddDamage(damage);
            }
            //else is Tile
            BulletsMgr.RestoreBullet(this);
        }
    }
}
