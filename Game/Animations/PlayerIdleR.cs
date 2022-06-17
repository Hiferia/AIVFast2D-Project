using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFineAnno
{
    class PlayerIdleR : AnimationBase
    {
        Player owner;
        public PlayerIdleR(Player owner) : base("playerIDLER", "playerIDLER", 1)
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
