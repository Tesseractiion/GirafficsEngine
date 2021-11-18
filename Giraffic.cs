using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Numerics;
using System.Threading;
using System.Drawing.Drawing2D;
//using Giraffics.GirafficsEngine.Draw;

// Source: https://www.youtube.com/watch?v=JnGM1p2vsbE&t=8235s

namespace GirafficsEngine
{
    public class Canvas : Form
    {
        public Canvas(Vector2 size, string title)
        {
            this.Size = new Size((int)size.X, (int)size.Y + 40);
            this.Text = title;
            this.DoubleBuffered = true;
        }
    }

    public abstract class Giraffic
    {
        private Vector2 screenPos;
        private string title { get; }
        private Canvas window;
        private Thread gameLoopThread;
        private Graphics g;
        private int fps;
        private DateTime lastRenderTime;
        private SmoothingMode smoothingMode = SmoothingMode.Default;

        public List<Draw.Shape> pendingShapes = new List<Draw.Shape>();

        public Vector2 screenSize;
        public DateTime startTime { get; }
        public int frame = 0;
        public float dT = 0.01f;
        public Point mousePos = new Point(0, 0);

        public Color backgroundColor = Color.AliceBlue;

        public Giraffic(Vector2 screenSize, string title, int fps = 60, String icon_dir = "", bool antialiasing = false, bool resizable = true)
        {
            this.screenSize = screenSize;
            this.title = title;
            this.fps = fps;
            if (antialiasing)
                smoothingMode = SmoothingMode.AntiAlias;

            window = new Canvas(screenSize, title);
            window.Paint += Renderer; // make render function get called when form paints

            if (icon_dir.Length > 0)
                window.Icon = new Icon(icon_dir);

            // Make window not resizable if applicable
            if (!resizable)
            {
                window.MaximumSize = new Size((int)screenSize.X, (int)screenSize.Y);
                window.MinimumSize = new Size((int)screenSize.X, (int)screenSize.Y);
                window.MaximizeBox = false;
            }

            startTime = DateTime.Now;
            lastRenderTime = DateTime.Now;
            
            // Start thread to handle game looping stuff
            gameLoopThread = new Thread(GameLoop);
            gameLoopThread.Start();

            // Window events
            window.FormClosed += Window_FormClosed;
            window.SizeChanged += Window_SizeChanged;
            window.Move += Window_Move;

            // Window Input events
            window.KeyUp += OnKeyReleased;
            window.KeyDown += OnKeyIsDown;
            window.MouseMove += Window_MouseMove;
            window.MouseDoubleClick += OnMouseDoubleClick;
            window.MouseUp += OnMouseUp;
            
            Application.Run(window);

            screenPos = new Vector2(window.Location.X, window.Location.Y);
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            mousePos = e.Location;
        }

        private void Window_Move(object sender, EventArgs e)
        {
            Vector2 newPos = new Vector2(window.Location.X, window.Location.Y);
            OnWindowMove(screenPos, newPos);
            screenPos = newPos;
        }

        private void Window_SizeChanged(object sender, EventArgs e)
        {
            Vector2 newSize = new Vector2(window.Width, window.Height - 40);
            OnWindowResize(screenSize, newSize);
            screenSize = newSize;
        }

        private void Window_FormClosed(object sender, FormClosedEventArgs e)
        {
            gameLoopThread.Abort();
            OnStop();
            g.Dispose();
        }

        void GameLoop()
        {
            OnStart();
            while (gameLoopThread.IsAlive)
            {
                try
                {
                    DateTime loopStartTime = DateTime.Now;

                    frame++;

                    // Force windows forms screen to update (no idea how this works yet)
                    window.BeginInvoke((MethodInvoker)delegate { 
                        if ((loopStartTime - lastRenderTime).TotalSeconds >= (double)1/fps)
                            window.Refresh();
                    });
                    
                    OnUpdate(dT);

                    // Give the window 1 millisecond to refresh
                    Thread.Sleep(1);

                    dT = (float)(DateTime.Now - loopStartTime).TotalSeconds;

                } catch { }
            }
        }

        private void Renderer(object sender, PaintEventArgs e)
        {
            // Set graphics, clear screen, and call OnDraw method
            g = e.Graphics;

            // Set smoothing mode
            g.SmoothingMode = smoothingMode;

            g.Clear(backgroundColor);
            OnDraw();
            
            // Draw pending shapes
            foreach (Draw.Shape shape in pendingShapes)
                shape.Render(g);

            // Clear pending shapes so they aren't redrawn
            pendingShapes.Clear();
            
            // Set now as last time to be rendered
            lastRenderTime = DateTime.Now;
        }

        public Graphics GetGraphics()
        {
            return g;
        }

        public Canvas GetWindow()
        {
            return window;
        }

        public virtual void OnStart() { }
        public virtual void OnStop() { }
        public virtual void OnUpdate(float dT) { }
        public virtual void OnDraw() { }
        public virtual void OnWindowResize(Vector2 oldSize, Vector2 newSize) { }
        public virtual void OnWindowMove(Vector2 oldPos, Vector2 newPos) { }

        // Input Events
        public virtual void OnKeyIsDown(object sender, KeyEventArgs e) { }
        public virtual void OnKeyReleased(object sender, KeyEventArgs e) { }
        public virtual void OnMouseUp(object sender, MouseEventArgs e) { }
        public virtual void OnMouseDoubleClick(object sender, MouseEventArgs e) { }
    }
}
