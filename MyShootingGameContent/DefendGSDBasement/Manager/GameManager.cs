using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace DefendGSDBasement
{
    public class GameManager
    {
        public Player Player {  get; set; }
        private Texture2D _bg, _bg2;

        public GameManager()
        {
            _bg = Globals.Content.Load<Texture2D>("wallpaper1");
            _bg2 = Globals.Content.Load<Texture2D>("wallpaper2");
            var laserTex = Globals.Content.Load<Texture2D>("projectile_1");
            var bulletTex = Globals.Content.Load<Texture2D>("bullet");
            ProjectileManager.Init(laserTex, bulletTex);
            UIManager.Init(bulletTex);
            ExperienceManager.Init(Globals.Content.Load<Texture2D>("FireSparks"));

            Player = new(Globals.Content.Load<Texture2D>("player_3"));
            ZombieManager.Init();
        }

        public void Restart()
        {
            ProjectileManager.Reset();
            ZombieManager.Reset();
            ExperienceManager.Reset();
            Player.Reset();
        }

        public void Update()
        {
            InputManager.Update();
            ExperienceManager.Update(Player);
            Player.Update(ZombieManager.Zombies);
            ZombieManager.Update(Player);
            ProjectileManager.Update(ZombieManager.Zombies);
        }

        public void Draw()
        {
            if (Player.level % 2 == 0) 
            {
                Globals.SpriteBatch.Draw(_bg, Vector2.Zero, Color.White);
            }
            else
            {
                Globals.SpriteBatch.Draw(_bg2, Vector2.Zero, Color.White);
            }
            ExperienceManager.Draw();
            ProjectileManager.Draw();
            ZombieManager.Draw();
            Player.Draw();
            UIManager.Draw(Player);
        }
    }
}
