using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFineAnno
{
    class EnemyIdleD : AnimationBase
    {
        Enemy owner;
        public EnemyIdleD(Enemy owner) : base("enemyIDLED", "enemyIDLED", 1)
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
