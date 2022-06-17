using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFineAnno
{
    class PlayerWalksD : AnimationBase
    {
        Player owner;
        public PlayerWalksD(Player owner) : base("PlayerWalksD", "PlayerWalksD", 4)
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
