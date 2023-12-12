using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefendGSDBasement
{
    public static class ExperienceManager
    {
        private static Texture2D _texture;
        public static List<Experience> Experience { get; } = new();
        private static SpriteFont _font;
        private static Vector2 _position;
        private static Vector2 _textPosition;
        private static Animation _anim;
        public static string _playerExp;

        public static void Init(Texture2D tex)
        {
            _texture = tex;
            _font = Globals.Content.Load<SpriteFont>("font");
            _anim = new Animation(_texture, 4, 1, 0.2f, 1);
            _position = new(Globals.Bounds.X - (2 * _texture.Width), 0);
        }

        public static void Reset()
        {
            Experience.Clear();
        }

        public static void AddExperience(Vector2 pos)
        {
            Experience.Add(new(_texture, pos));
        }

        public static void Update(Player player)
        {
            foreach (var e in Experience)
            {
                e.Update();
                e.UpdateBoundingRect();

                if (player.BoundingRect.Intersects(e.BoundingRect))
                {
                    if (Collision.PixelCollision(player, e))
                    {
                        e.Collect();
                        player.GetExperience(1);
                    }
                }
            }

            Experience.RemoveAll((e) => e.Lifespan <= 0);

            _playerExp = player.Experience.ToString();
            var x = _font.MeasureString(_playerExp).X / 2;
            _textPosition = new(Globals.Bounds.X - x - 32, 14);
            _anim.Update();
        }

        public static void Draw()
        {
            _anim.Draw(new Vector2(1500 - _texture.Width / 4, 20 - _texture.Height), new Vector2(2f, 2f));
            Globals.SpriteBatch.DrawString(_font, _playerExp, new Vector2(_textPosition.X, _textPosition.Y + 15), Color.Black);

            foreach (var e in Experience)
            {
                e.Draw();
            }
        }
    }
}
