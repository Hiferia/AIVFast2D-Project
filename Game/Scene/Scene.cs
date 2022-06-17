using System;
using System.Xml;
using System.Collections.Generic;
using OpenTK;
using Aiv.Audio;


namespace ProgFineAnno
{
    abstract class Scene
    {
        protected AudioSource source;
        protected AudioClip themeClip;

        public bool IsPlaying { get;  set; } //era protected set
        public List<Vector2> TilePositions;


        public Scene NextScene;

        public Scene()
        {
            source = new AudioSource();
            source.Volume -= 0.7f;
        }

        public virtual void Start()
        {
            IsPlaying = true;
            TilePositions = new List<Vector2>();
        }
        protected virtual void LoadClips()
        {

        }
        public virtual Scene OnExit()
        {
            IsPlaying = false;
            return NextScene;
        }
        
        public abstract void Input();
        public virtual void Update()
        {

        }
        public abstract void Draw();
    }
}
