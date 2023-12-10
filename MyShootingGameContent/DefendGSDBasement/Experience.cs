using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefendGSDBasement
{
    public class Experience : Sprite
    {
        public float Lifespan { get; private set; }
        private const float LIFE = 5f;

        public Experience(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            Lifespan = LIFE;
        }

        public void Update()
        {
            Lifespan -= Globals.TotalSeconds;
            Scale = 0.33f + (Lifespan / LIFE * 0.66f);
        }

        public void Collect()
        {
            Lifespan = 0;
        }
    }
}
