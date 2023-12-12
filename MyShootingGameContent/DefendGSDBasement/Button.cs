using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefendGSDBasement
{
    public class Button : Sprite
    {
        #region Fields
        private SpriteFont _font;
        private bool _isHovering;
        private Texture2D _texture;

        private MouseState _currentMouse;

        private MouseState _prevMouse;
        #endregion

        #region Properties

        public EventHandler Click;
        public bool Clicked { get; private set; }

        public float layer { get; set; }

        public string Text;

        public Color TextColor;
        #endregion
        public Button(Vector2 pos, Texture2D tex, SpriteFont font) : base(tex, pos)
        {
            _texture = tex;
            _font = font;
        }

        public override void Draw()
        {
            var color = Color.White;

            if (_isHovering)
            {
                color = Color.Gray;
            }
            Globals.SpriteBatch.Draw(_texture, Position, null, color, 0f, origin, 1f, SpriteEffects.None, layer);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = Position.X - (_font.MeasureString(Text).X / 2);
                var y = Position.Y - (_font.MeasureString(Text).Y / 2);

                Globals.SpriteBatch.DrawString(_font, Text, new Vector2(x, y), TextColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, layer + 0.001f);

            }
        }

        public void Update(GameTime gameTime)
        {
            _prevMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle((int)InputManager.MousePosition.X, (int)InputManager.MousePosition.Y, 1, 1);
            _isHovering = false;

            base.UpdateBoundingRect();

            if (mouseRectangle.Intersects(BoundingRect))
            {
                _isHovering = true;
                if (_currentMouse.LeftButton == ButtonState.Released && _prevMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}
