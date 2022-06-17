using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Graph02;
using Aiv.Audio;


namespace ProgFineAnno
{
    class CaveScene : Scene
    {
        private Player player;
        private static World worldGrid;

        public static Enemy Enemy;


        public Key Key;
        public CaveScene()
        {
            IsPlaying = false;
            
        }
        public override void Start()
        {
            LoadClips();
            LoadEnemyTextures();
            IsPlaying = true;
            themeClip = AudioMgr.GetClip("dungeon");
            TileMgr.Init("Assets/mappaCaverna.tmx");
            worldGrid = new World();
            worldGrid.Init(20, 20);

            LoadEnemy();

            player = OutDoorScene.Player;
            player.AddAllToAllMgr();
            player.UpdateWorld(worldGrid);
            player.Position = new Vector2(14.5f, 17.5f);
            Key = new Key(new Vector2(8.5f, 10.5f));


            CameraMgr.Init(player.Position, new Vector2(Game.Win.OrthoWidth * 0.5f, Game.Win.OrthoHeight * 0.5f));
            CameraMgr.Behaviour = FollowBehaviour.FollowTarget;
            CameraMgr.Limits = new CameraLimits(float.MaxValue, float.MinValue, float.MaxValue, float.MinValue);
            CameraMgr.Offset = new Vector2(0, 0);
            CameraMgr.Target = player;
        }
        public static void LoadEnemy()
        {
            Enemy = new Enemy(worldGrid);
            Enemy.Position = new Vector2(6.5f, 10.5f);
        }
        protected override void LoadClips()
        {
            AudioMgr.AddClip("dungeon", "Assets/Audio/Dungeon-Theme.ogg");
            AudioMgr.AddClip("keyClip", "Assets/Audio/Picked-Coin-Echo.ogg");
            AudioMgr.AddClip("bossTheme", "Assets/Audio/Boss-Theme.ogg");
            AudioMgr.AddClip("dogBark", "Assets/Audio/bark.ogg");
        }
        public override void Input()
        {
            player.Input();

        }
        private void LoadEnemyTextures()
        {
            GfxMgr.AddTexture("enemyIDLER", "Assets/EnemyDog/HEROS8bit_Dog Idle R.png");
            GfxMgr.AddTexture("enemyIDLEU", "Assets/EnemyDog/HEROS8bit_Dog Idle U.png");
            GfxMgr.AddTexture("enemyIDLED", "Assets/EnemyDog/HEROS8bit_Dog Idle D.png");
            GfxMgr.AddTexture("enemyWalksR", "Assets/EnemyDog/HEROS8bit_Dog Walk R.png");
            GfxMgr.AddTexture("enemyWalksU", "Assets/EnemyDog/HEROS8bit_Dog Walk U.png");
            GfxMgr.AddTexture("enemyWalksD", "Assets/EnemyDog/HEROS8bit_Dog Walk D.png");
        }
        public override void Update()
        {
            if (player.HasKey)
            {
                DrawMgr.RemoveItem(Key);
                UpdateMgr.RemoveItem(Key);
                Key.Destroy();
            }
            source.Stream(themeClip, Game.DeltaTime * 100, true);   
            UpdateMgr.Update();
            CameraMgr.Update();
            PhysicsMgr.Update();
            PhysicsMgr.CheckCollisions();
        }
        public virtual void ChangeMusic()
        {
            source.Stop();
            themeClip = AudioMgr.GetClip("bossTheme");
        }
        public override void Draw()
        {
            DrawMgr.Draw();
        }
        public override Scene OnExit()
        {
            source.Stop();
            themeClip.Rewind();
            Enemy.Destroy();
            UpdateMgr.ClearAll();
            DrawMgr.ClearAll();
            PhysicsMgr.ClearAll();
            FontMgr.ClearAll();

            return base.OnExit();
        }
        
    }
}
