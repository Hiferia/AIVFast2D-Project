using Aiv.Fast2D;
using OpenTK;

namespace ProgFineAnno
{
    class TileObj: GameObject
    {
        private int xOff;
        private int yOff;
        private int widthPixel;
        private int heightPixel;

        public bool IsInDoorHouse1;
        public bool IsDoorLocked;
        public bool IsOutDoorCave;
        public bool IsInDoorCave;
        public bool IsInDoorHouse2;
        public bool IsOutDoorHouse2;


        public TileObj(string textureName, 
            int tOffX, int tOffY,
            float posX, float posY,
            int width, int height, float scaleX = 1, float scaleY = 1) : base(textureName, DrawLayer.Playground)
        {
            sprite = new Sprite(Game.PixelsToUnits(width * 8), Game.PixelsToUnits(height * 8));
            sprite.position.X = posX * 8;
            sprite.position.Y = posY * 8;
            
            widthPixel = width;
            heightPixel = height;
            

            xOff = tOffX;
            yOff = tOffY;

            IsActive = true;
        }


        public override void Draw()
        {
           if (IsActive)
            {
                sprite.DrawTexture(texture, xOff, yOff, widthPixel, heightPixel);
            }
        }
    }
}
