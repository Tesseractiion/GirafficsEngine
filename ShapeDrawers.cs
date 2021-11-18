using System.Drawing;

namespace GirafficsEngine
{
    public partial class Draw
    {
        // Definition that a shape must follow
        public abstract class Shape
        {
            public abstract void Render(Graphics g);

            protected void RequestRender(Giraffic giraffic)
            {
                giraffic.pendingShapes.Add(this);
            }
        }

        // Draw methods for shapes
        public static void Rect(Giraffic giraffic, Color color, int x, int y, int width, int height, bool isFilled = true)
        {
            new RectDef(giraffic, color, x, y, width, height, isFilled);
        }

        public static void Oval(Giraffic giraffic, Color color, int x, int y, int width, int height, bool isFilled = true)
        {
            new OvalDef(giraffic, color, x, y, width, height, isFilled);
        }

        public static void Poly(Giraffic giraffic, Color color, Point[] points, bool isFilled = true)
        {
            new PolyDef(giraffic, color, points, isFilled);
        }

        public static void Line(Giraffic giraffic, Color color, int x1, int y1, int x2, int y2, int width=1)
        {
            new LineDef(giraffic, color, x1, y1, x2, y2, width);
        }

        public static void Image(Giraffic giraffic, Image image, int x, int y)
        {
            new ImageDef(giraffic, image, x, y);
        }

        public static void Pixel(Giraffic giraffic, Color color, int x, int y)
        {
            new PixelDef(giraffic, color, x, y);
        }

        public static void Text(Giraffic giraffic, Color color, int x, int y, string text, FontFamily family, float fontSize)
        {
            new TextDef(giraffic, color, text, family, fontSize, x, y);
        }
    }
}
