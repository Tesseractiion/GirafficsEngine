using System.Drawing;

namespace GirafficsEngine
{
    partial class Draw
    {
        private class RectDef : Shape
        {
            Color color;
            int x;
            int y;
            int width;
            int height;
            bool isFilled;

            public RectDef(Giraffic giraffic, Color color, int x, int y, int width, int height, bool isFilled)
            {
                this.color = color;
                this.x = x;
                this.y = y;
                this.width = width;
                this.height = height;
                this.isFilled = isFilled;

                RequestRender(giraffic);
            }

            public override void Render(Graphics g)
            {
                if (isFilled)
                    g.FillRectangle(new SolidBrush(color), new Rectangle(x, y, width, height));
                else
                    g.DrawRectangle(new Pen(color), new Rectangle(x, y, width, height));
            }
        }
    }
}
