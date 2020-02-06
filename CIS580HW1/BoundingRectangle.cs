using Microsoft.Xna.Framework;

namespace CIS580HW1
{
    class BoundingRectangle
    {
        public float X;

        public float Y;

        public float Width;

        public float Height;

        public BoundingRectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Cast operator for casting into a Rectangle
        /// </summary>
        /// <param name="br"></param>
        public static implicit operator Rectangle(BoundingRectangle br)
        {
            return new Rectangle(
                (int) br.X,
                (int) br.Y,
                (int) br.Width,
                (int) br.Height);
        }
    }

}
