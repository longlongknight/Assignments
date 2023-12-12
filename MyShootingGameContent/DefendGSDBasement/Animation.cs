using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefendGSDBasement
{
    public class Animation
    {
        private readonly Texture2D _texture;
        private readonly List<Rectangle> _sourceRectangles = new();
        private readonly int _frames;
        private int _frame;
        private readonly float _frameTime;
        private float _frameTimeLeft;
        private bool _active = true;
        public int frameWidth { get; set; }
        public int frameHeight { get; set; }
        public Animation(Texture2D texture, int framesX, int framesY, float frameTime, int row = 1)
        {
            _texture = texture;
            _frameTime = frameTime;
            _frameTimeLeft = _frameTime;
            _frames = framesX;
            frameWidth = _texture.Width / framesX;
            frameHeight = _texture.Height / framesY;

            for (int i = 0; i < _frames; i++)
            {
                _sourceRectangles.Add(new(i * frameWidth, (row - 1) * frameHeight, frameWidth, frameHeight));
            }
        }

        public void Stop()
        {
            _active = false;
        }

        public void Start()
        {
            _active = true;
        }

        public void Reset()
        {
            _frame = 0;
            _frameTimeLeft = _frameTime;
        }

        public void Update()
        {
            if (!_active) return;

            _frameTimeLeft -= Globals.TotalSeconds;

            if (_frameTimeLeft <= 0)
            {
                _frameTimeLeft += _frameTime;
                _frame = (_frame + 1) % _frames;
            }
        }

        public void Draw(Vector2 pos, Vector2 scale)
        {
            Globals.SpriteBatch.Draw(_texture, pos, _sourceRectangles[_frame], Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);
        }

        public void Draw(Vector2 pos, float rotation)
        {
            Globals.SpriteBatch.Draw(_texture, pos, _sourceRectangles[_frame], Color.White, rotation, Vector2.Zero, 1, SpriteEffects.None, 1);
        }
    }
}
