using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFineAnno
{
    static class GfxMgr
    {
        private static Dictionary<string, Texture> textures;

        static GfxMgr()
        {
            textures = new Dictionary<string, Texture>();
        }

        public static void AddTexture(string name, string path)
        {
            if (!textures.ContainsKey(name))
            {
                textures.Add(name, new Texture(path));

            }
        }
        public static Texture AddTextureT(string name, string path)
        {
            Texture t = new Texture(path);
            textures.Add(name, t);

            return t;
        }

        public static Texture GetTexture(string name)
        {
            Texture t = null;
            if (textures.ContainsKey(name))
            {
                t = textures[name];
            }

            return t;
        }

        internal static void AddTexture(object imgPath1, object imgPath2)
        {
            throw new NotImplementedException();
        }
    }
}
