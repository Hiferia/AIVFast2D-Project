using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFineAnno
{
    class EnemyFireball : EnemyBullet
    {
        public EnemyFireball() : base("fireball", BulletType.EnemyFireball)
        {
            RigidBody.Collider = ColliderFactory.CreateCircleFor(this);
            damage = 5;
            speed = 10;
        }

        public override void Shoot(Vector2 startPosition, Vector2 direction, Actor shotBy)
        {
            Forward = direction;
            base.Shoot(startPosition, direction, shotBy);
        }
    }
}
