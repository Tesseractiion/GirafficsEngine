using System.Drawing;

namespace GirafficsEngine
{
    partial class Draw
    {
        private class PixelDef : Shape
        {
            Color color;
            int x;
            int y;

            public PixelDef(Giraffic giraffic, Color color, int x, int y)
            {
                this.color = color;
                this.x = x;
                this.y = y;

                RequestRender(giraffic);
            }

            public override void Render(Graphics g)
            {
                g.FillRectangle(new SolidBrush(color), new Rectangle(x, y, 1, 1));
            }
        }
    }
}
