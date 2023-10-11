using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Taskbar;

namespace Project
{
    public class Camera
    {
        public Matrix Transform { get; private set; }

        /// <summary>
        /// the middle of the screen
        /// </summary>
        public Vector2 Position;
        public float Scale { get; private set; }

        private int backgroundWidth = 800;
        private int backgroundHeight = 480;

        private Rectangle Bounds;

        private float minScale = 1.0f;
        private float maxScale = 2.0f;

        Viewport viewport;

        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
            Scale = 1.0f;
            Position = new Vector2(viewport.Width /2, viewport.Height/2);
            Bounds = new Rectangle(0, 0, backgroundWidth - viewport.Width, backgroundHeight - viewport.Height);
            UpdateTransform(viewport);
        }

        public void UpdateTransform(Viewport viewport)
        {
            Transform = Matrix.CreateTranslation(-Position.X, -Position.Y, 0) *
                        Matrix.CreateScale(Scale) *
                        Matrix.CreateTranslation(viewport.Width / 2, viewport.Height / 2, 0);
        }

        public void Move(Vector2 delta)
        {

            Position += delta;

            float leftSideOfScreen = Position.X - (viewport.Width / 2/ Scale);
            float rightSideOfScreen = Position.X + (viewport.Width / 2 / Scale);
            float TopOfScreen = Position.Y - (viewport.Height / 2 / Scale);
            float BottomOfScreen = Position.Y + (viewport.Height / 2 / Scale);

            float holdx = MathHelper.Clamp(leftSideOfScreen, 0, 800);
            float holdy = MathHelper.Clamp(BottomOfScreen, 0, 480);
            Position = new Vector2(holdx + (viewport.Width / 2 / Scale), holdy - (viewport.Height / 2 / Scale));
        }

        public void Center(Viewport viewport)
        {
            Position = new Vector2(viewport.Width / 2, viewport.Height / 2);
        }

        public void ZoomIn(float factor)
        {
            Scale *= factor;
            Scale = Math.Min(Scale, maxScale);
        }

        public void ZoomOut(float factor)
        {
            Scale /= 1.1f; // Adjust the zoom factor as needed
            Scale = Math.Max(Scale, minScale); // Ensure a minimum scale
        }
    }

}
