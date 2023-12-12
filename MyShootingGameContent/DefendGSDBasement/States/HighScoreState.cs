using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefendGSDBasement.States
{
    public class HighscoresState : State
    {
        private List<Button> components;

        private SpriteFont font;

        private GameManager _gameManager;
        private Texture2D MenuBackgroundTexture;

        public HighscoresState(Game1 game) : base(game)
        {
            game.IsMouseVisible = true;
            var buttonTexture1 = Globals.Content.Load<Texture2D>("Button2");
            var buttonTexture2 = Globals.Content.Load<Texture2D>("Button3");
            var buttonFont = Globals.Content.Load<SpriteFont>("font");
            MenuBackgroundTexture = Globals.Content.Load<Texture2D>("MenuBG2");

            var WindowCenter = new Vector2(Globals.Bounds.X / 2, Globals.Bounds.Y / 2);

            var replayButton = new Button(WindowCenter + new Vector2(0, -200), buttonTexture1, buttonFont) { };

            replayButton.Click += Button_Replay_Click;

            var mainMenuButton = new Button(WindowCenter + new Vector2(0, 200), buttonTexture2, buttonFont) { };

            mainMenuButton.Click += Button_MainMenu_Click;

            components = new List<Button>()
            {
                replayButton,
                mainMenuButton,
            };
        }

        public override void LoadContent()
        {
            font = _content.Load<SpriteFont>("font");
        }


        private void Button_Replay_Click(object sender, EventArgs args)
        {
            _game.ChangeState(new GameState(_game));
        }

        private void Button_MainMenu_Click(object sender, EventArgs args)
        {
            _game.ChangeState(new MenuState(_game));
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Button_MainMenu_Click(this, new EventArgs());

            foreach (var component in components)
                component.Update(gameTime);
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);
            spriteBatch.Draw(MenuBackgroundTexture, Vector2.Zero, Color.White);

            foreach (var component in components)
                component.Draw();
            spriteBatch.DrawString(font, "You Dead!", new Vector2(Globals.Bounds.X / 2 - 180, 0), Color.Red, 0, new Vector2(0, 0), 2, SpriteEffects.None, 1);
            spriteBatch.DrawString(font, "Record: " + ExperienceManager._playerExp, new Vector2(400, 100), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 1);

            spriteBatch.End();
        }
    }
}
