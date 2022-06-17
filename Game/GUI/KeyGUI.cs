using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Aiv.Fast2D;

namespace ProgFineAnno
{
    class KeyGUI : GameObject
    {
        public KeyGUI() : base("key", DrawLayer.GUI, Game.PixelsToUnits(120), Game.PixelsToUnits(120))
        {
            IsActive = false;
            sprite.position = new Vector2(1);
            sprite.Camera = CameraMgr.GetCamera("GUI");

        }
        public override void Update()
        {
            base.Update();
            if (Game.CurrentScene is GameOverScene)
            {
                IsActive = false;
            }
        }
    }
}
