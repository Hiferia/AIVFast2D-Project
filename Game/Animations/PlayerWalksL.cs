using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFineAnno
{
    class PlayerWalksL : AnimationBase
    {
        Player owner;
        public PlayerWalksL(Player owner) : base("PlayerWalksR", "PlayerWalksR", 4)
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
