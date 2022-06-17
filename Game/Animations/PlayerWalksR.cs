using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFineAnno
{
    class PlayerWalksR : AnimationBase
    {
        Player owner;
        public PlayerWalksR(Player owner) : base("PlayerWalksR", "PlayerWalksR", 4)
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
