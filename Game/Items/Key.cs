using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Aiv.Audio;

namespace ProgFineAnno
{
    class Key : GameObject
    {
        protected AudioSource source;
        protected AudioClip keyClip;
        public Key(Vector2 position) : base("key", DrawLayer.Playground, Game.PixelsToUnits(90), Game.PixelsToUnits(90))
        {
            IsActive = true;
            sprite.position = position;
            RigidBody = new RigidBody(this);
            RigidBody.Collider = ColliderFactory.CreateBoxFor(this);
            RigidBody.Type = RigidBodyType.Key;
            RigidBody.AddCollisionType((uint)RigidBodyType.Player);
            source = new AudioSource();
            source.Volume += 1;
            keyClip = AudioMgr.GetClip("keyClip");
        }
        public override void OnCollide(Collision collisionInfo)
        {
            if(collisionInfo.Collider is Player)
            {
                source.Play(keyClip);
                IsActive = false;
            }
        }
    }
}
