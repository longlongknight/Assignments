using DefendGSDBasement.States;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace DefendGSDBasement
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameManager _gameManager;

        private Song _bgm;
        private EventHandler<EventArgs> MediaPlayer_MediaStateChanged;

        private State _currentState;
        private State _nextState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            Globals.Bounds = new(1536, 1024);
            _graphics.PreferredBackBufferWidth = Globals.Bounds.X;
            _graphics.PreferredBackBufferHeight = Globals.Bounds.Y;
            _graphics.ApplyChanges();

            Globals.Content = Content;
            _gameManager = new();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _bgm = Content.Load<Song>("ImageCorruptor");
            MediaPlayer.Play(_bgm);
            MediaPlayer.Volume = 0.15f;
            MediaPlayer.IsRepeating = true;

            _currentState = new MenuState(this);
            _currentState.LoadContent();
            _nextState = null;

            Globals.SpriteBatch = _spriteBatch;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (_nextState != null)
            {
                 _currentState = _nextState;
                 _currentState.LoadContent();

                 _nextState = null;
            }

            _currentState.Update(gameTime);

            Globals.Update(gameTime);
            //_gameManager.Update();

            base.Update(gameTime);
        }

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            //_spriteBatch.Begin();
            //_gameManager.Draw();
            _currentState.Draw(gameTime, _spriteBatch);
            //_spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
