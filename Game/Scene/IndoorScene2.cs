using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Graph02;


namespace ProgFineAnno
{
    class IndoorScene2 : Scene
    {
        private Player player;

        public List<Player> Players;
        private World worldGrid;

        public IndoorScene2()
        {
            Players = new List<Player>();
            IsPlaying = false;
        }
        public override void Start()
        {
            LoadClips();
            IsPlaying = true;
            themeClip = AudioMgr.GetClip("house");
            TileMgr.Init("Assets/MappaCasa2.tmx");
            worldGrid = new World();
            worldGrid.Init(10, 16);

            player = OutDoorScene.Player;
            player.AddAllToAllMgr();
            player.UpdateWorld(worldGrid);
            player.Position = new Vector2(8.5f, 8);

            CameraMgr.Init(player.Position, new Vector2(Game.Win.OrthoWidth * 0.5f, Game.Win.OrthoHeight * 0.5f));
            CameraMgr.Behaviour = FollowBehaviour.FollowTarget;
            CameraMgr.Limits = new CameraLimits(float.MaxValue, float.MinValue, float.MaxValue, float.MinValue); 
            CameraMgr.Offset = new Vector2(0, 0);
            CameraMgr.Target = player;

        }
        protected override void LoadClips()
        {
            AudioMgr.AddClip("house", "Assets/Audio/House-Theme.ogg");
        }
        public override void Input()
        {
            player.Input();

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
