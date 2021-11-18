using System.Drawing;

namespace GirafficsEngine
{
    partial class Draw
    {
        private class ImageDef : Shape
        {
            Image image;
            int x;
            int y;

            public ImageDef(Giraffic giraffic, Image image, int x, int y)
            {;
                this.image = image;
                this.x = x;
                this.y = y;

                RequestRender(giraffic);
            }

            public override void Render(Graphics g)
            {
                g.DrawImage(image, x, y);
            }
        }
    }
}
