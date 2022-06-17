using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFineAnno
{
    class PlayerIdleU : AnimationBase
    {
        Player owner;
        public PlayerIdleU(Player owner) : base("playerIDLEU", "playerIDLEU", 1)
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
