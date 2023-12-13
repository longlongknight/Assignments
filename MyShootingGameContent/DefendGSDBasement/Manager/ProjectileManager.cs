﻿using System.Collections.Generic;

namespace DefendGSDBasement
{
    public static class ProjectileManager
    {
        private static Texture2D _texture;
        private static Texture2D _texture2;
        public static List<Projectile> Projectiles { get; } = new();

        public static void Init(Texture2D tex, Texture2D tex2)
        {
            _texture = tex;
            _texture2 = tex2;
        }

        public static void Reset()
        {
            Projectiles.Clear();
        }

        public static void AddProjectile(ProjectileData data)
        {
            Projectiles.Add(new(_texture, data));
        }

        public static void Update(List<Zombie> zombies)
        {
            foreach (var p in Projectiles)
            {
                p.Update();
                foreach (var z in zombies)
                {
                    if (z.HP <= 0) continue;
                    if ((p.Position - z.Position).Length() < 32)
                    {
                        z.TakeDamage(p.Damage);
                        p.Destroy();
                        break;
                    }
                }
            }
            Projectiles.RemoveAll((p) => p.Lifespan <= 0);
        }

        public static void Draw()
        {
            foreach (var p in Projectiles)
            {
                p.Draw();
            }
        }
    }
}
