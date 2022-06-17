using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace ProgFineAnno
{
    class Fireball : PlayerBullet
    {
        public Fireball() : base("fireball", BulletType.Fireball)
        {
            RigidBody.Collider = ColliderFactory.CreateCircleFor(this);
            damage = 30;
            speed = 10;
        }

        public override void Shoot(Vector2 startPosition, Vector2 direction, Actor shotBy)
        {
            Forward = direction;
            base.Shoot(startPosition, direction, shotBy);
        }

    }
}
