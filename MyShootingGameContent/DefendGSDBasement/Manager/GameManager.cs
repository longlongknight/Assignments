using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace DefendGSDBasement
{
    public class GameManager
    {
        public Player Player {  get; set; }
        private readonly Background _bg;

        public GameManager()
        {
            _bg = new();
            var texture = Globals.Content.Load<Texture2D>("bullet");
            ProjectileManager.Init(texture);
            UIManager.Init(texture);
            ExperienceManager.Init(Globals.Content.Load<Texture2D>("FireSparks"));

            Player = new(Globals.Content.Load<Texture2D>("player_9mmhandgun"));
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
            _bg.Draw();
            ExperienceManager.Draw();
            ProjectileManager.Draw();
            Player.Draw();
            ZombieManager.Draw();
            UIManager.Draw(Player);
        }
    }
}
