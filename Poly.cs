using System.Drawing;

namespace GirafficsEngine
{
    partial class Draw
    {
        private class PolyDef : Shape
        {
            Color color;
            Point[] points;
            bool isFilled;

            public PolyDef(Giraffic giraffic, Color color, Point[] points, bool isFilled)
            {
                this.color = color;
                this.points = points;
                this.isFilled = isFilled;

                RequestRender(giraffic);
            }

            public override void Render(Graphics g)
            {
                if (isFilled)
                    g.FillPolygon(new SolidBrush(color), points);
                else
                    g.DrawPolygon(new Pen(color), points);
            }
        }
    }
}
