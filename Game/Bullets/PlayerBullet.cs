using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFineAnno
{
    abstract class PlayerBullet : Bullet
    {
        public PlayerBullet(string texturePath, BulletType type):base(texturePath, type)
        {
            RigidBody.Type = RigidBodyType.PlayerBullet;
            RigidBody.AddCollisionType((uint)RigidBodyType.Enemy);
            RigidBody.AddCollisionType((uint)RigidBodyType.EnemyBullet);

            RigidBody.Velocity = new Vector2(500, 0);
        }

        public override void Shoot(Vector2 startPosition, Actor shotBy)
        {
            base.Shoot(startPosition, shotBy);
            //sprite.position.X += sprite.pivot.X;
        }

        public override void Update()
        {
            if (IsActive)
            {
                
                if (sprite.position.X - sprite.pivot.X >= Game.Win.Width || sprite.position.X + sprite.pivot.X < 0 || Position.Y + sprite.pivot.Y < 0 || Position.Y - sprite.pivot.Y >= Game.Win.Height)
                {
                    BulletsMgr.RestoreBullet(this);
                } 
            }
        }

        public override void OnCollide(Collision collisionInfo)
        {
            if(collisionInfo.Collider is Enemy)
            {
                Enemy e = (Enemy)collisionInfo.Collider;
                if (e.AddDamage(damage))
                {
                    //he's dead
                    //((Player)owner).Points += e.Value;
                }
            }
            else if(collisionInfo.Collider is EnemyBullet)
            {
                BulletsMgr.RestoreBullet((EnemyBullet)collisionInfo.Collider);
            }
            //is Tile!

            BulletsMgr.RestoreBullet(this);
        }
    }
}
