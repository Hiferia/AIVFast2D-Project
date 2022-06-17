using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;

namespace ProgFineAnno
{
    class AnimationBase : GameObject
    {
        Animation animation;
        public AnimationBase(string textureName, string animationName, int numFrames) : base(textureName, DrawLayer.Playground, Game.PixelsToUnits(16), Game.PixelsToUnits(16))
        {
            animation = new Animation(this, texture.Height, texture.Height, numFrames, 10, true, OnAnimationEnd);
            components.Add(animationName, animation);
            sprite.scale = new OpenTK.Vector2(6);
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
