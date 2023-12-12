namespace DefendGSDBasement
{
    public class Sprite
    {
        public readonly Texture2D texture;
        public readonly Vector2 origin;
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Scale { get; set; }
        public Color Color { get; set; }

        public Color[] data { get; set; }

        public Rectangle BoundingRect;

        public Sprite(Texture2D tex, Vector2 pos)
        {
            texture = tex;
            Position = pos;
            origin = new(tex.Width / 2, tex.Height / 2);
            Scale = 1f;
            Color = Color.White;

            data = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(data);
        }

        public void UpdateBoundingRect()
        {
            Rectangle Rect = new Rectangle(0, 0, texture.Width, texture.Height);
            Matrix Transform = Matrix.CreateTranslation(new Vector3(-origin, 0.0f)) *
                               Matrix.CreateRotationZ(Rotation) *
                               Matrix.CreateTranslation(new Vector3(Position, 0.0f));
            BoundingRect = Collision.CalculateBoundingRectangle(Rect, Transform);
        }

        public void UpdateBoundingRect(int frameWidth, int frameHeight)
        {
            Rectangle Rect = new Rectangle(0, 0, frameWidth, frameHeight);
            Matrix Transform = Matrix.CreateTranslation(new Vector3(frameWidth / 2f, frameHeight / 2f, 0.0f)) *
                               Matrix.CreateRotationZ(Rotation) *
                               Matrix.CreateTranslation(new Vector3(Position, 0.0f));
            BoundingRect = Collision.CalculateBoundingRectangle(Rect, Transform);
        }

        public virtual void Draw()
        {
            Globals.SpriteBatch.Draw(texture, Position, null, Color, Rotation, origin, Scale, SpriteEffects.None, 1);
        }
    }
}
