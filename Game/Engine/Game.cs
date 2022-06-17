using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFineAnno
{
    static class Game
    {
        public static KeyGUI keyGui;

        public static Scene TitleScene;
        public static Scene OutDoorScene;
        public static Scene GameOverScene;
        public static Scene WinScene;
        public static Scene IndoorScene1;
        public static Scene IndoorScene2;
        public static Scene CaveScene;


        public static Window Win;
        public static bool IsRunning;
        public static float PixelSize { get; private set; }
        public static Scene CurrentScene { get; private set; }

        public static float UnitSize { get; private set; }
        public static float OptimalUnitSize { get; private set; }
        public static float OptimalScreenHeight { get; private set; }

        public static float DeltaTime { get { return Win.deltaTime; } }
        public static RandomTimer RandomTimer(int timeMin, int timeMax)
        {
            return new RandomTimer(timeMin, timeMax);
        }
        

        public static void Init()
        {
            Win = new Window(1280, 720, "RPG Spiller");
            Win.Position = new Vector2(150, 50);
            Win.SetVSync(false);
            Win.SetDefaultOrthographicSize(10);
            OptimalScreenHeight = 1280;

            UnitSize = Win.Height / Win.OrthoHeight;
            OptimalUnitSize = OptimalScreenHeight / Win.OrthoHeight;
            
            PixelSize = PixelsToUnits(1);
            TitleScene = new TitleScene("Assets/aivBG.png");
            OutDoorScene = new OutDoorScene();
            GameOverScene = new GameOverScene();
            WinScene = new WinScene();
            CaveScene = new CaveScene();
            IndoorScene1 = new IndoorScene1();
            IndoorScene2 = new IndoorScene2();

            TitleScene.NextScene = OutDoorScene;
            OutDoorScene.NextScene = IndoorScene1;
            GameOverScene.NextScene = OutDoorScene;
            WinScene.NextScene = OutDoorScene;
            IndoorScene1.NextScene = OutDoorScene;
            CurrentScene = TitleScene;

            GfxMgr.AddTexture("key", "Assets/Items/item8BIT_key.png");

        }
        public static float PixelsToUnits(float pixelsSize)
        {
            return pixelsSize / OptimalUnitSize;
        }
        public static void Play()
        {

            IsRunning = true;
            //scene.Start();
            CurrentScene.Start();
            CameraMgr.AddCamera("GUI", new Camera());

            keyGui = new KeyGUI();

            while (Win.IsOpened && IsRunning)
            {
                //input
                if (Win.GetKey(KeyCode.Esc))
                {
                    break;
                }
                CurrentScene.Input();
               


                if (!CurrentScene.IsPlaying)
                {
                    //change scene
                    Scene newScene = CurrentScene.OnExit();

                    GC.Collect();

                    if (newScene != null)
                    {
                        CurrentScene = newScene;
                        CurrentScene.Start();
                    }
                    else return;
                }

                //UPDATE
                CurrentScene.Update();
                keyGui.Update();

                //DRAW
                CurrentScene.Draw();
                keyGui.Draw();


                Win.Update();
            }
        }

    }
}
