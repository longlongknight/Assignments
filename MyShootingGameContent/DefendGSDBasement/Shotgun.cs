namespace DefendGSDBasement
{
    public class Shotgun : Weapon
    {
        public Shotgun()
        {
            cooldown = 0.75f;
            maxAmmo = 8;
            Ammo = maxAmmo;
            reloadTime = 3f;
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
        }
    }
}
