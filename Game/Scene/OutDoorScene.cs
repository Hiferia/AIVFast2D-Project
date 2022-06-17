using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Graph02;
using Aiv.Fast2D;
using Aiv.Audio;

namespace ProgFineAnno
{
    class OutDoorScene : Scene
    {
        private List<TileObj> tileObjs;
        private static World worldGrid;
        private TmxReader reader;
        private int collGrid;
        private int rowGrid;
        private static Vector2 playerPosition;

        public static Player Player;
        public static List<Player> Players;
        

        public OutDoorScene()
        {
            tileObjs = new List<TileObj>();
            Players = new List<Player>();
            playerPosition = Vector2.Zero;
            
        }
        public override void Start()
        {
            
            LoadClips();
            LoadPlayerTextures();
            themeClip = AudioMgr.GetClip("grassLands");

            IsPlaying = true;
            
            TileMgr.Init("Assets/MappaEsterna1.tmx");
            worldGrid = new World();
            worldGrid.Init(18, 40);
            
            FontMgr.Init();
            Font stdFont = FontMgr.AddFont("stdFont", "Assets/textSheet.png", 15, 32, 20, 20);
            Font comic = FontMgr.AddFont("comic", "Assets/comics.png", 10, 32, 61, 65);
            if (Players.Count > 0)
            {
                Players.RemoveAt(0);
            }
            if (playerPosition == Vector2.Zero)
            {
                playerPosition = new Vector2(5.5f, 6.5f);
            }
            if(Player == null)
            {
                LoadPlayer();
                if (Players.Count == 0)
                {
                    Players.Add(Player);
                }
            }
            else
            {
                Player.AddAllToAllMgr();
                Player.UpdateWorld(worldGrid);
                if (Players.Count == 0)
                {
                    Players.Add(Player);
                }

            }
            CameraMgr.Init(Player.Position, new Vector2(Game.Win.OrthoWidth * 0.5f, Game.Win.OrthoHeight * 0.5f));
            CameraMgr.Behaviour = FollowBehaviour.FollowTarget;
            CameraMgr.Limits = new CameraLimits(31, 8.87f, 13, 5);
            CameraMgr.Offset = new Vector2(0, 0);
            CameraMgr.Target = Player;
            

        }
        public static void LoadPlayer()
        {
            Player = new Player(playerPosition, worldGrid);
        }
        
        private void LoadPlayerTextures()
        {
            GfxMgr.AddTexture("playerIDLER", "Assets/Player/HEROS8bit_Adventurer Idle R.png");
            GfxMgr.AddTexture("playerIDLEU", "Assets/Player/HEROS8bit_Adventurer Idle U.png");
            GfxMgr.AddTexture("playerIDLED", "Assets/Player/HEROS8bit_Adventurer Idle D.png");
            GfxMgr.AddTexture("PlayerWalksR", "Assets/Player/HEROS8bit_Adventurer Walk R.png");
            GfxMgr.AddTexture("PlayerWalksU", "Assets/Player/HEROS8bit_Adventurer Walk U.png");
            GfxMgr.AddTexture("PlayerWalksD", "Assets/Player/HEROS8bit_Adventurer Walk D.png");
        }
        
        protected override void LoadClips()
        {
            AudioMgr.AddClip("walk", "Assets/Audio/walkStep.ogg");
            AudioMgr.AddClip("grassLands", "Assets/Audio/Grasslands-Theme.ogg");
        }
        public override void Input()
        {
            //player.Input();

        }
        public override void Update()
        {
            source.Stream(themeClip, Game.DeltaTime * 100, true);
            

            UpdateMgr.Update();
            CameraMgr.Update();
            PhysicsMgr.Update();
            PhysicsMgr.CheckCollisions();
        }

        public override void Draw()
        {
            DrawMgr.Draw();
        }
        public override Scene OnExit()
        {
            source.Stop();
            themeClip.Rewind();

            UpdateMgr.ClearAll();
            DrawMgr.ClearAll();
            PhysicsMgr.ClearAll();
            FontMgr.ClearAll();

            return base.OnExit();
        }
    }
}
