using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefendGSDBasement.States
{
    internal class MenuState : State
    {
        private List<Button> _buttons;

        private Texture2D MenuBackgroundTexture;

        public MenuState(Game1 game) : base(game)
        {
            game.IsMouseVisible = true;
        }

        public override void LoadContent()
        {
            _buttons = new List<Button>();
            var buttonTexture1 = Globals.Content.Load<Texture2D>("Button");
            var buttonTexture2 = Globals.Content.Load<Texture2D>("Button4");
            var title = Globals.Content.Load<Texture2D>("title");
            var buttonFont = Globals.Content.Load<SpriteFont>("font");
            MenuBackgroundTexture = Globals.Content.Load<Texture2D>("MenuBG");

            var WindowCenter = new Vector2(Globals.Bounds.X / 2, Globals.Bounds.Y / 2);

            var titleButton = new Button(WindowCenter + new Vector2(0, -300), title, buttonFont) { };
            var newGameButton = new Button(WindowCenter + new Vector2(0, -50), buttonTexture1, buttonFont)
            {
                Click = new EventHandler(Button_newGame_Clicked),
                TextColor = Color.Black
            };

            var exitGameButton = new Button(WindowCenter + new Vector2(0, 200), buttonTexture2, buttonFont) { };

            exitGameButton.Click += Button_Replay_Click;

            _buttons.Add(titleButton);
            _buttons.Add(newGameButton);
            _buttons.Add(exitGameButton);
        }

        public void Button_newGame_Clicked(object sender, EventArgs args)
        {
            _game.ChangeState(new GameState(_game));
        }

        public void Button_Replay_Click(object sender, EventArgs args)
        {
            _game.Exit();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _buttons)
            {
                component.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(MenuBackgroundTexture, Vector2.Zero, Color.White);

            foreach (var component in _buttons)
            {
                component.Draw();
            }

            spriteBatch.End();
        }
    }
}
