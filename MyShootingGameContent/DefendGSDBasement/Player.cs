using Microsoft.Xna.Framework.Audio;

namespace DefendGSDBasement
{

    public class Player : MovingSprite
    {
        public Weapon Weapon { get; set; }
        private Weapon _weapon1;
        private Weapon _weapon2;
        private float HitCD, MaxHitCD = 1.5f;
        public bool Dead { get; private set; }
        public int Experience { get; private set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public string gunMsg = "MG";
        public float msgTimer;
        public string levelupMsg;
        private SpriteFont _font;
        public int level = 1;
        public float difficultyEditor = 0.15f;
        private int expReq = 20;

        public Player(Texture2D tex) : base(tex, GetStartPosition())
        {
            _font = Globals.Content.Load<SpriteFont>("font");
            Reset();
        }

        private static Vector2 GetStartPosition()
        {
            return new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2);
        }

        public void GetExperience(int exp)
        {
            Experience += exp;
            if(Experience % expReq == 0)
            {
                levelupMsg = "Level acheived, ++Speed & Maximum HP";
                Speed += 30;
                msgTimer = 2f;
                level++;
                MaxHP += 10;
                HP = MaxHP;
                ZombieManager.spawnCooldown -= difficultyEditor;
                expReq += expReq;
                if (level % 4 == 0)
                {
                    _weapon1.maxAmmo += 20;
                    levelupMsg = "Laser Blaster Update";
                }
                else if (level % 6 == 0)
                {
                    _weapon2.burstAmount += 2;
                    levelupMsg = "Burst Cannon Update";
                }
                    
            }
        }

        public void Reset()
        {
            _weapon1 = new LaserBlaster();
            _weapon2 = new BurstCannon();
            Dead = false;
            Weapon = _weapon1;
            Position = GetStartPosition();
            Experience = 0;
            MaxHP = 10;
            HP = MaxHP;
            HitCD = 0f;
        }

        public void SwapWeapon()
        {
            if (Weapon != _weapon1)
            {
                Weapon = _weapon1;
                gunMsg = "MG";
            }
            else if (Weapon != _weapon2)
            {
                Weapon = _weapon2;
                gunMsg = "Shotgun";
            }

        }

        public void TakeDamage(List<Zombie> zombies)
        {
            if (HitCD > 0) return;
                Color = Color.White;

            foreach (Zombie z in zombies)
            {
                if (z.HP <= 0) continue;
                if (this.BoundingRect.Intersects(z.BoundingRect))
                {
                    if (Collision.PixelCollision(this, z))
                    {
                        HP--;
                        Color = Color.Red;
                        HitCD = MaxHitCD;

                        if (HP <= 0)
                        {
                            Dead = true;
                            break;
                        }
                    }
                }
            }
        }

        public void Update(List<Zombie> zombies)
        {
            if (InputManager.Direction != Vector2.Zero)
            {
                var dir = Vector2.Normalize(InputManager.Direction);
                Position = new(
                    MathHelper.Clamp(Position.X + (dir.X * Speed * Globals.TotalSeconds), 0, Globals.Bounds.X),
                    MathHelper.Clamp(Position.Y + (dir.Y * Speed * Globals.TotalSeconds), 0, Globals.Bounds.Y)
                );
            }

            var toMouse = InputManager.MousePosition - Position;
            Rotation = (float)Math.Atan2(toMouse.Y, toMouse.X);

            Weapon.Update();

            if (InputManager.SpacePressed)
            {
                SwapWeapon();
            }

            if (InputManager.MouseLeftDown)
            {
                Weapon.Fire(this);
            }

            if (InputManager.MouseRightClicked)
            {
                Weapon.Reload();
            }


            if (HitCD > 0)
            {
                HitCD -= Globals.TotalSeconds;
            }


            UpdateBoundingRect();

            msgTimer -= Globals.TotalSeconds;

            TakeDamage(zombies);
        }

        public override void Draw()
        {
            base.Draw();
            if (msgTimer > 0)
            {
                var msgPos = Position;
                Globals.SpriteBatch.DrawString(_font, levelupMsg, new(msgPos.X - 200, msgPos.Y), Color.Aqua);
            }
        }
    }
}
