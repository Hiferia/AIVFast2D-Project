using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFineAnno
{
    class Tile : GameObject
    {
        public Tile(string textureName) : base(textureName, DrawLayer.Foreground)
        {
            RigidBody = new RigidBody(this);
            RigidBody.Collider = ColliderFactory.CreateBoxFor(this);
            RigidBody.Type = RigidBodyType.Tile;

            IsActive = true;
        }
        public override void Draw()
        {
            sprite.DrawTexture(texture); //overridiamo il draw del GameObject in modo tale che le casse siano grandi un' unità
        }
    }
}
