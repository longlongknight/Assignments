using DefendGSDBasement;

namespace Collide
{
    internal class Collision
    {
        public static Rectangle CalculateBoundingRectangle(Rectangle rectangle, Matrix transform)
        {

            Vector2 leftTop = new Vector2(rectangle.Left, rectangle.Top);
            Vector2 rightTop = new Vector2(rectangle.Right, rectangle.Top);
            Vector2 leftBottom = new Vector2(rectangle.Left, rectangle.Bottom);
            Vector2 rightBottom = new Vector2(rectangle.Right, rectangle.Bottom);

            Vector2.Transform(ref leftTop, ref transform, out leftTop);
            Vector2.Transform(ref rightTop, ref transform, out rightTop);
            Vector2.Transform(ref leftBottom, ref transform, out leftBottom);
            Vector2.Transform(ref rightBottom, ref transform, out rightBottom);

            Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop), Vector2.Min(leftBottom, rightBottom));
            Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop), Vector2.Max(leftBottom, rightBottom));

            return new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
        }

        //Overloading PixelCollision for animated sprite against normal sprite
        public static bool PixelCollision(
        Matrix transformA, Rectangle rectA, int widthA, ref Color[] dataA,
        Matrix transformB, int widthB, int heightB, ref Color[] dataB)
        {
            Matrix AToB = transformA * Matrix.Invert(transformB);
            Vector2 stepX = Vector2.TransformNormal(Vector2.UnitX, AToB); Vector2 stepY = Vector2.TransformNormal(Vector2.UnitY, AToB);
            Vector2 yPosInB = Vector2.Transform(Vector2.Zero, AToB);
            for (int yA = rectA.Top; yA < rectA.Bottom; yA++)
            {   // For each row in A
                Vector2 posInB = yPosInB; // At the beginning of the row
                for (int xA = rectA.Left; xA < rectA.Right; xA++)
                { // For each pixel in the row
                    int xB = (int)Math.Round(posInB.X); int yB = (int)Math.Round(posInB.Y);
                    if (0 <= xB && xB < widthB && 0 <= yB && yB < heightB)
                    {
                        Color colorA = dataA[xA + yA * widthA];
                        Color colorB = dataB[xB + yB * widthB];
                        if (colorA.A != 0 && colorB.A != 0) return true;
                    }
                    posInB += stepX; // Move to next pixel in the row
                }
                yPosInB += stepY; // Move to the next row
            }
            return false; // No intersection found
        }


        //Pixel Collision for transfromed sprite
        public static bool PixelCollision(Sprite A, Sprite B)
        {
            Matrix transformA = Matrix.CreateTranslation(new Vector3(-A.origin, 0.0f)) *
                                Matrix.CreateRotationZ(A.Rotation) *
                                Matrix.CreateTranslation(new Vector3(A.Position, 0.0f));
            Matrix transformB = Matrix.CreateTranslation(new Vector3(-B.origin, 0.0f)) *
                                Matrix.CreateRotationZ(B.Rotation) *
                                Matrix.CreateTranslation(new Vector3(B.Position, 0.0f));
            int widthA = A.texture.Width;
            int heightA = A.texture.Height;

            int widthB = B.texture.Width;
            int heightB = B.texture.Height;

            Matrix AToB = transformA * Matrix.Invert(transformB);
            Vector2 stepX = Vector2.TransformNormal(Vector2.UnitX, AToB); Vector2 stepY = Vector2.TransformNormal(Vector2.UnitY, AToB);
            Vector2 yPosInB = Vector2.Transform(Vector2.Zero, AToB);
            for (int yA = 0; yA < heightA; yA++)
            {   // For each row in A
                Vector2 posInB = yPosInB; // At the beginning of the row
                for (int xA = 0; xA < widthA; xA++)
                { // For each pixel in the row
                    int xB = (int)Math.Round(posInB.X); int yB = (int)Math.Round(posInB.Y);
                    if (0 <= xB && xB < widthB && 0 <= yB && yB < heightB)
                    {
                        Color colorA = A.data[xA + yA * widthA]; Color colorB = B.data[xB + yB * widthB];
                        if (colorA.A != 0 && colorB.A != 0) return true;
                    }
                    posInB += stepX; // Move to next pixel in the row
                }
                yPosInB += stepY; // Move to the next row
            }
            return false; // No intersection found
        }

    }
}