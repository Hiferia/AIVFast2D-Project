using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace ProgFineAnno
{
    class GameOverScene : TitleScene
    {
        //private Texture texture;
        //private Sprite sprite;
        //private string texturePath;
        public GameOverScene() : base("Assets/gameOverBg.png", KeyCode.Y)
        {
        }

        public override void Start()
        {
            base.Start();
            LoadClips();
            themeClip = AudioMgr.GetClip("outro");
            texture = new Texture(texturePath);
            sprite = new Sprite(Game.Win.OrthoWidth, Game.Win.OrthoHeight);
            CameraMgr.Init(sprite.position, sprite.position);
            OutDoorScene.Player = null;
            CaveScene.Enemy = null;
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
            AudioMgr.AddClip("outro", "Assets/Audio/Sadness.ogg");

        }
        public override void Input()
        {
            if (Game.Win.GetKey(KeyCode.Y)) OnExit();
            //if (Game.Win.GetKey(KeyCode.N))
            //{
            //    NextScene = null;
            //    Game.IsRunning = false;
            //}
            base.Input();
            if (IsPlaying && Game.Win.GetKey(KeyCode.N) || Game.Win.JoystickStart(0))
            {
                NextScene = null;
                Game.IsRunning = false;
            }
        }
    }
}
