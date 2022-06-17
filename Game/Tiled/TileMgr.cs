using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace ProgFineAnno
{
    static class TileMgr
    {
        private static List<TileObj> tileObjs;
        static public List<Vector2> TilePositions;
        static public List<Vector2> DoorPositions;

        public static void Init(string texturename)
        {
            TmxReader reader = new TmxReader(texturename);
            TileSet ts = reader.TileSet;
            List<Layer> layers = reader.Layers;

            GfxMgr.AddTexture(ts.ImgPath, ts.ImgPath);
            tileObjs = new List<TileObj>();
            TilePositions = new List<Vector2>();
            DoorPositions = new List<Vector2>();
            foreach (Layer each in layers)
            {
                AddTilesFor(each, tileObjs);
            }


        }

        private static void AddTilesFor(Layer layer, List<TileObj> tileObjs)
        {
            //string type = layer.Name;
            //DrawLayer engineLayer = (DrawLayer)Enum.Parse(typeof(DrawLayer), type);

            DrawLayer engineLayer = DrawLayer.Playground;
            if (layer.Props.Has("drawLayer"))
            {
                string drawLayer = layer.Props.GetString("drawLayer");
                engineLayer = (DrawLayer)Enum.Parse(typeof(DrawLayer), drawLayer);
            }

            for (int i = 0; i < layer.Grid.Size(); i++)
            {
                TileInstance inst = layer.Grid.At(i);
                if (inst == null) continue;
                string texture = inst.Type.ImagePath;
                int tOffX = inst.Type.OffX;
                int tOffY = inst.Type.OffY;
                int width = inst.Type.Width;
                int height = inst.Type.Height;
                float posX = Game.PixelsToUnits(inst.PosX);
                float posY = Game.PixelsToUnits(inst.PosY);
                TileObj obj = new TileObj(texture, tOffX, tOffY, posX, posY, width, height);
                tileObjs.Add(obj);

                //bool obj = (bool)inst.Type.Get("collidable");
                // bool found = props.TryBool("collidable", outValue)
                if (inst.Type.Props.Has("collidable") && inst.Type.Props.GetBool("collidable"))
                {
                    obj.RigidBody = new RigidBody(obj);
                    obj.RigidBody.Collider = ColliderFactory.CreateBoxFor(obj);
                    obj.RigidBody.Type = RigidBodyType.TileObj;
                    TilePositions.Add(obj.Position);
                }

                if (inst.Type.Props.Has("door") && inst.Type.Props.GetBool("door"))
                {
                    obj.IsInDoorHouse1 = true;
                    obj.RigidBody = new RigidBody(obj);
                    obj.RigidBody.Collider = ColliderFactory.CreateBoxFor(obj);
                    obj.RigidBody.Type = RigidBodyType.TileObj;
                }
                if (inst.Type.Props.Has("outDoorHouse2") && inst.Type.Props.GetBool("outDoorHouse2"))
                {
                    obj.IsOutDoorHouse2 = true;
                    obj.RigidBody = new RigidBody(obj);
                    obj.RigidBody.Collider = ColliderFactory.CreateBoxFor(obj);
                    obj.RigidBody.Type = RigidBodyType.TileObj;
                }
                if (inst.Type.Props.Has("doorCasa2") && inst.Type.Props.GetBool("doorCasa2"))
                {
                    obj.IsInDoorHouse2 = true;
                    obj.RigidBody = new RigidBody(obj);
                    obj.RigidBody.Collider = ColliderFactory.CreateBoxFor(obj);
                    obj.RigidBody.Type = RigidBodyType.TileObj;
                }
                if (inst.Type.Props.Has("doorLocked") && inst.Type.Props.GetBool("doorLocked"))
                {
                    obj.IsDoorLocked = true;
                    obj.RigidBody = new RigidBody(obj);
                    obj.RigidBody.Collider = ColliderFactory.CreateBoxFor(obj);
                    obj.RigidBody.Type = RigidBodyType.TileObj;
                }
                if (inst.Type.Props.Has("outDoorCave") && inst.Type.Props.GetBool("outDoorCave"))
                {
                    obj.IsOutDoorCave = true;
                    obj.RigidBody = new RigidBody(obj);
                    obj.RigidBody.Collider = ColliderFactory.CreateBoxFor(obj);
                    obj.RigidBody.Type = RigidBodyType.TileObj;
                }
                if (inst.Type.Props.Has("inDoorCave") && inst.Type.Props.GetBool("inDoorCave"))
                {
                    obj.IsInDoorCave = true;
                    obj.RigidBody = new RigidBody(obj);
                    obj.RigidBody.Collider = ColliderFactory.CreateBoxFor(obj);
                    obj.RigidBody.Type = RigidBodyType.TileObj;
                }
            }
        }
    }
}
