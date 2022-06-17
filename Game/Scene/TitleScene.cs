using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace ProgFineAnno
{
    class TitleScene : Scene
    {
        protected Texture texture;
        protected Sprite sprite;
        protected string texturePath;
        protected KeyCode exitkey;
        public TitleScene(string texturePath, KeyCode exitkey = KeyCode.Return)
        {
            this.texturePath = texturePath;
            this.exitkey = exitkey;
        }
        public override void Start()
        {
            base.Start();
            LoadClips();
            themeClip = AudioMgr.GetClip("intro");
            texture = new Texture(texturePath);
            sprite = new Sprite(Game.Win.OrthoWidth, Game.Win.OrthoHeight);
            CameraMgr.Init(sprite.position, sprite.position);
            
        }
        public override void Draw()
        {
            sprite.DrawTexture(texture);
        }
        public override void Update()
        {
            base.Update();
            source.Stream(themeClip, Game.DeltaTime * 100, true);

        }
        protected override void LoadClips()
        {
            AudioMgr.AddClip("intro", "Assets/Audio/Intro-Theme.ogg");

        }
        public override void Input()
        {
            if (Game.Win.GetKey(exitkey))
            {
                IsPlaying = false;
            }
        }
        public override Scene OnExit()
        {
            source.Stop();
            themeClip.Rewind();
            sprite = null;
            texture = null;
            return base.OnExit();
        }
    }
}
