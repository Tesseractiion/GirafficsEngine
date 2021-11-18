using System.Drawing;

namespace GirafficsEngine
{
    partial class Draw
    {
        private class TextDef : Shape
        {
            Color color;
            Font font;
            string text;
            int x;
            int y;

            public TextDef(Giraffic giraffic, Color color, string text, FontFamily family, float fontSize, int x, int y)
            {
                this.text = text;
                this.color = color;
                this.x = x;
                this.y = y;

                font = new Font(family, fontSize);

                RequestRender(giraffic);
            }

            public override void Render(Graphics g)
            {
                g.DrawString(text, font, new SolidBrush(color), new PointF(x, y));
            }
        }
    }
}
