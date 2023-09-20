using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
//using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using CollisionExample.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;

namespace Project
{
    public class Arrow
    {
        public Texture2D texture;
        public Vector2 Position;

        public int Speed = 40;

        public BoundingRectangle bounds;

        public Arrow(Vector2 shotFrom, ContentManager c)
        {
            Position = shotFrom;
            texture = c.Load<Texture2D>("ArrowBlue");
        }

        public void Move(GameTime gT)
        {
            Position += new Vector2(Speed, 0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
}
