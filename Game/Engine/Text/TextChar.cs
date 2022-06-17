using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace ProgFineAnno
{
    class TextChar : GameObject
    {
        protected char character;
        protected Vector2 textureOffset;
        protected Font font;
      
        public char Character { get { return character; } set { character = value; ComputeOffset(); } }

        public TextChar(Vector2 spritePosition, char character, Font font) : base(font.TextureName, DrawLayer.GUI, Game.PixelsToUnits(font.CharacterWidth), Game.PixelsToUnits(font.CharacterHeight))
        {
            this.sprite.position = spritePosition;
            this.font = font;
            this.character = character;
            sprite.pivot = Vector2.Zero;
            sprite.scale *= new Vector2(2.5f);
            //sprite.Camera = CameraMgr.GetCamera("GUI");
            ComputeOffset();
        }

        protected void ComputeOffset()
        {
            textureOffset = font.GetOffset(this.character);
        }

        public override void Draw()
        {
            if(IsActive)
                sprite.DrawTexture(texture, (int)textureOffset.X, (int)textureOffset.Y, font.CharacterWidth, font.CharacterHeight);
        }
    }
}
