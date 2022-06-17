using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFineAnno
{
    class EnemyIdleR : AnimationBase
    {
        Enemy owner;
        public EnemyIdleR(Enemy owner) : base("enemyIDLER", "enemyIDLER", 1)
        {
            this.owner = owner;
        }
        public override void Update()
        {
            Position = owner.Position;
            
            base.Update();
        }
    }
}
