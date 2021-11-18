using System.Drawing;

namespace GirafficsEngine
{
    partial class Draw
    {
        private class LineDef : Shape
        {
            Color color;
            int x1;
            int y1;
            int x2;
            int y2;
            int width;

            public LineDef(Giraffic giraffic, Color color, int x1, int y1, int x2, int y2, int width)
            {
                this.color = color;
                this.x1 = x1;
                this.y1 = y1;
                this.x2 = x2;
                this.y2 = y2;
                this.width = width;

                RequestRender(giraffic);
            }

            public override void Render(Graphics g)
            {
                g.DrawLine(new Pen(color, width), x1, y1, x2, y2);
            }
        }
    }
}
