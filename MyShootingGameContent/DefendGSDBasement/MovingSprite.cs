using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefendGSDBasement
{
   public class MovingSprite : Sprite
    {
        public int Speed { get; set; }

        public MovingSprite(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            Speed = 300;
        }
    }
}
