using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFineAnno
{
    class EnemyIdleL : AnimationBase
    {
        Enemy owner;
        public EnemyIdleL(Enemy owner) : base("enemyIDLER", "enemyIDLER", 1)
        {
            this.owner = owner;
            sprite.FlipX = true;

        }
        public override void Update()
        {
            Position = owner.Position;
            
            base.Update();
        }
    }
}
