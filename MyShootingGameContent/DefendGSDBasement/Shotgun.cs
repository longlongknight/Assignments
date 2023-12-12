using Microsoft.Xna.Framework.Audio;

namespace DefendGSDBasement
{
    public class Shotgun : Weapon
    {
        private SoundEffect _soundEffect;

        public Shotgun()
        {
            _soundEffect = Globals.Content.Load<SoundEffect>("Shotgun");
            cooldown = 0.75f;
            maxAmmo = 10;
            Ammo = maxAmmo;
            reloadTime = 2f;
        }

        protected override void CreateProjectiles(Player player)
        {
            const float angleStep = (float)(Math.PI / 16);

            ProjectileData pd = new()
            {
                Position = player.Position,
                Rotation = player.Rotation - (3 * angleStep),
                Lifespan = 0.5f,
                Speed = 800,
                Damage = 2
            };

            for (int i = 0; i < 5; i++)
            {
                pd.Rotation += angleStep;
                ProjectileManager.AddProjectile(pd);
            }
            _soundEffect.Play(0.15f, 0f, 0f);
        }
    }
}
