using Microsoft.Xna.Framework.Audio;

namespace DefendGSDBasement
{
    class MachineGun : Weapon
    {
        private SoundEffect _soundEffect;

        public MachineGun()
        {
            _soundEffect = Globals.Content.Load<SoundEffect>("gunfire");
            cooldown = 0.1f;
            maxAmmo = 30;
            Ammo = maxAmmo;
            reloadTime = 1f;
        }

        protected override void CreateProjectiles(Player player)
        {
            ProjectileData pd = new()
            {
                Position = player.Position,
                Rotation = player.Rotation,
                Lifespan = 3f,
                Speed = 800,
                Damage = 1
            };

            _soundEffect.Play(0.2f, 0f, 0f);
            ProjectileManager.AddProjectile(pd);
        }
    }
}
