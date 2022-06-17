using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFineAnno
{
    class PlayerIdleL : AnimationBase
    {
        Player owner;
        public PlayerIdleL(Player owner) : base("playerIDLER", "playerIDLER", 1)
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
