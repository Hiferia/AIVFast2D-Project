using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace ProgFineAnno
{
    class WalkAnimation : GameObject
    {
        Animation animation;
        Actor owner;
        public Dictionary<string, Texture> WalkTextures;
        public WalkAnimation(Actor owner, string walkR, string walkU, string walkD, int numFrames) : base(walkR, DrawLayer.Playground, Game.PixelsToUnits(GfxMgr.GetTexture(walkR).Height), Game.PixelsToUnits(GfxMgr.GetTexture(walkR).Height))
        {
            WalkTextures = new Dictionary<string, Texture>();
            WalkTextures.Add("walkR", GfxMgr.GetTexture(walkR));
            WalkTextures.Add("walkL", GfxMgr.GetTexture(walkR));
            WalkTextures.Add("walkU", GfxMgr.GetTexture(walkU));
            WalkTextures.Add("walkD", GfxMgr.GetTexture(walkD));

            sprite = new Sprite(Game.PixelsToUnits(GfxMgr.GetTexture(walkR).Height), Game.PixelsToUnits(GfxMgr.GetTexture(walkR).Height));
            sprite.scale *= new Vector2(3, 3);
            this.owner = owner;

            animation = new Animation(this, texture.Height, texture.Height, numFrames, 5, true, OnAnimationEnd);
        }

        public override void Draw()
        {
            if (IsActive)
            {
                sprite.DrawTexture(texture, TextureOffsetX, TextureOffsetY, (int)(sprite.Width * Game.OptimalUnitSize), (int)(sprite.Height * Game.OptimalUnitSize));
            }
        }

        protected virtual void OnAnimationEnd()
        {
            Stop();
        }

        public virtual void Start()
        {
            animation.Play();

            animation.IsActive = true;
            IsActive = true;

        }

        public virtual void Stop()
        {
            animation.Stop();
            IsActive = false;
            animation.IsActive = false;
        }

    }
}
