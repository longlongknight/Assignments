using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DefendGSDBasement
{
    public static class ZombieManager
    {
        public static List<Zombie> Zombies { get; } = new();
        private static Texture2D _texture;
        public static float spawnCooldown { get; set; }
        private static float _spawnTime;
        private static Random _random;
        private static int _padding, _prevExp;


        public static void Init()
        {
            _texture = Globals.Content.Load<Texture2D>("purple_03");
            spawnCooldown = 1f;
            _spawnTime = spawnCooldown;
            _random = new();
            _padding = _texture.Width / 2;
        }

        public static void Reset()
        {
            Zombies.Clear();
            spawnCooldown = 1f;
            _prevExp = 0;
            _spawnTime = spawnCooldown;
        }

        private static Vector2 RandomPosition()
        {
            float w = Globals.Bounds.X;
            float h = Globals.Bounds.Y;
            Vector2 pos = new();

            if (_random.NextDouble() < w / (w + h))
            {
                pos.X = (int)(_random.NextDouble() * w);
                pos.Y = (int)(_random.NextDouble() < 0.5 ? -_padding : h + _padding);
            }
            else
            {
                pos.Y = (int)(_random.NextDouble() * h);
                pos.X = (int)(_random.NextDouble() < 0.5 ? -_padding : w + _padding);
            }

            return pos;
        }

        public static void AddZombie()
        {
            Zombies.Add(new(_texture, RandomPosition()));
        }

        public static void Update(Player player)
        {
            _spawnTime -= Globals.TotalSeconds;
            while (_spawnTime <= 0)
            {
                _spawnTime += spawnCooldown;
                AddZombie();
            }

            foreach (var z in Zombies)
            {
                z.Update(player);
            }
            Zombies.RemoveAll((z) => z.HP <= 0);
        }

        public static void Draw()
        {
            foreach (var z in Zombies)
            {
                z.Draw();
            }
        }
    }
}
