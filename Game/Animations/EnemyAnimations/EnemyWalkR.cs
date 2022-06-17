using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFineAnno
{
    class EnemyWalkR : AnimationBase
    {
        Enemy owner;
        public EnemyWalkR(Enemy owner) : base("enemyWalksR", "enemyWalksR", 4)
        {
            this.owner = owner;
            sprite.SetMultiplyTint(5, 1, 1, 1);

        }
        public override void Update()
        {
            Position = owner.Position;
            base.Update();
        }
    }
}
