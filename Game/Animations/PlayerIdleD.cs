using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFineAnno
{
    class PlayerIdleD : AnimationBase
    {
        Player owner;
        public PlayerIdleD(Player owner) : base("playerIDLED", "playerIDLED", 1)
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
