using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFineAnno
{
    class EnemyWalkD : AnimationBase
    {
        Enemy owner;
        public EnemyWalkD(Enemy owner) : base("enemyWalksD", "enemyWalksD", 4)
        {
            this.owner = owner;
            sprite.SetMultiplyTint(5,1, 1, 1);
        }
        public override void Update()
        {
            Position = owner.Position;
            
            base.Update();
        }
    }
}
