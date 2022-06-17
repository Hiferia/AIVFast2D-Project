using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFineAnno
{
    class EnemyIdleU : AnimationBase
    {
        Enemy owner;
        public EnemyIdleU(Enemy owner) : base("enemyIDLEU", "enemyIDLEU", 1)
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
