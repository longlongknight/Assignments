using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DefendGSDBasement.States
{
    internal class GameState : State
    {
        public GameManager _gameManager { get; set; }
        private Texture2D _cursor, _mg, _shotgun, _heart;
        private SpriteFont _font;
        private List<Button> _buttons;

        public GameState(Game1 game) : base(game)
        {
            _gameManager = new GameManager();
            game.IsMouseVisible = false;
            _gameManager.Restart();
        }



        public override void LoadContent()
        {
            _buttons = new List<Button>();
            _font = Globals.Content.Load<SpriteFont>("font");
            _heart = Globals.Content.Load<Texture2D>("heart");
            _cursor = Globals.Content.Load<Texture2D>("pngwing");
            _mg = Globals.Content.Load<Texture2D>("mp5");
            _shotgun = Globals.Content.Load<Texture2D>("m870");
        }

        public override void Update(GameTime gameTime)
        {
            _gameManager.Update();
            if (_gameManager.Player.Dead)
            {
                _game.ChangeState(new HighscoresState(_game));
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            _gameManager.Draw();
            spriteBatch.DrawString(_font, "HP: " + _gameManager.Player.HP.ToString(), new Vector2(Globals.Bounds.X / 2 - 100, 0), Color.White);
            spriteBatch.Draw(_heart, new Vector2(Globals.Bounds.X / 2, -10), Color.White);
            spriteBatch.DrawString(_font, "Level: " + _gameManager.Player.level, new Vector2(Globals.Bounds.X / 2 + 100, 0), Color.White);
            if (_gameManager.Player.gunMsg == "MG")
                spriteBatch.Draw(_mg, _gameManager.Player.origin, new Rectangle(0, 0, _mg.Width, _mg.Height), Color.Black, 0, new Vector2(0, 0), 0.15f, SpriteEffects.None, 1);
            else if (_gameManager.Player.gunMsg == "Shotgun")
                spriteBatch.Draw(_shotgun, _gameManager.Player.origin, new Rectangle(0, 0, _shotgun.Width, _shotgun.Height), Color.Black, 0, new Vector2(0, 0), 0.15f, SpriteEffects.None, 1);
            spriteBatch.Draw(_cursor, InputManager.MousePosition, new Rectangle(0, 0, 50, 50), Color.White, _gameManager.Player.Rotation, new Vector2(_cursor.Width / 2, _cursor.Height / 2), 1, SpriteEffects.None, 1);
            spriteBatch.End();
        }
    }
}
