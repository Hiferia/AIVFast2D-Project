using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFineAnno
{
    class PlayerWalksU : AnimationBase
    {
        Player owner;
        public PlayerWalksU(Player owner) : base("PlayerWalksU", "PlayerWalksU", 4)
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
