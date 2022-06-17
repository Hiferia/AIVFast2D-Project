using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFineAnno
{
    class GameObject : IUpdatable, IDrawable
    {
        protected Sprite sprite;
        protected Texture texture;
        protected Dictionary<string, Component> components;


        public RigidBody RigidBody;
        public int TextureOffsetX;
        public int TextureOffsetY;

        public DrawLayer Layer { 
            get { return layer; } 
            set {
                DrawMgr.RemoveItem(this);
                layer = value;
                DrawMgr.AddItem(this);
            } 
        }
        private DrawLayer layer;
        public Vector2 Position
        {
            get { return sprite.position; }
            set { sprite.position = value; }
        }

        public bool IsActive;

        public float Width { get { return sprite.Width; } } //in caso rimetti sprite.width,prima era anche senza pixeltounit
        public float Height { get { return sprite.Height; } }

        public int X { get { return (int)sprite.position.X; } set { sprite.position.X = value; } }
        public int Y { get { return (int)sprite.position.Y; } set { sprite.position.Y = value; } }
        public Vector2 Forward
        {
            get { return new Vector2((float)Math.Cos(sprite.Rotation), (float)Math.Sin(sprite.Rotation)); }
            set { sprite.Rotation = (float)Math.Atan2(value.Y, value.X); }
        }
        public GameObject(string textureName, DrawLayer layer = DrawLayer.Playground, float width = 0, float height = 0)
        {
            Layer = layer;
            texture = GfxMgr.GetTexture(textureName);
            sprite = new Sprite(width > 0 ? width : Game.PixelsToUnits(texture.Width), height > 0 ? height : Game.PixelsToUnits(texture.Height));
            sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);
            components = new Dictionary<string, Component>();


            UpdateMgr.AddItem(this);
            DrawMgr.AddItem(this);
        }

        public virtual void Update()
        {
            if (IsActive)
            {
                foreach (var comp in components)
                {
                    if (comp.Value.IsActive)
                    {
                        comp.Value.Update();
                    }
                }
            }
        }

        public virtual void Draw()
        {
            if (IsActive)
            {
                //sprite.DrawTexture(texture);
                sprite.DrawTexture(texture, TextureOffsetX, TextureOffsetY, 16, 16);
            }
        }

        public virtual void OnCollide(Collision collisionInfo)
        {

        }
        public virtual void OnCollide(GameObject other)
        {

        }
        public virtual void Destroy()
        {
            sprite = null;
            texture = null;

            UpdateMgr.RemoveItem(this);
            DrawMgr.RemoveItem(this);

            if (RigidBody != null)
            {
                RigidBody.Destroy();
                RigidBody = null;
            }
        }
    }
}
