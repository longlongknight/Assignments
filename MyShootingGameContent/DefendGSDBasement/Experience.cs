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
        private Vector2 _position;
        private readonly Animation _anim;

        public Experience(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            _anim = new(tex, 4, 1, 0.2f, 1);
            Lifespan = LIFE;
        }

        public void Update()
        {
            Lifespan -= Globals.TotalSeconds;

            _anim.Update();
            UpdateBoundingRect();
        }

        public void Collect()
        {
            Lifespan = 0;
        }

        public override void Draw()
        {
            _anim.Draw(Position, new Vector2(Scale, Scale));
        }
    }
}
