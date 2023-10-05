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
using SharpDX.DirectWrite;
using SharpDX.MediaFoundation;

namespace Project
{
    public class Arrow
    {
        public Texture2D texture;
        public Vector2 Position;
        public int damage = 4;

        public bool active = true;
        public int Speed = 10;
        public int team;
        private Color color;

        public BoundingRectangle bounds;

        public Arrow(Vector2 shotFrom, ContentManager c,int team)
        {
            Position = shotFrom;
            Position.Y += 8;

            bounds = new BoundingRectangle(Position, 8, 8);

            texture = c.Load<Texture2D>("ArrowBlue");
            this.team = team;
            if(team == 1)
            {
                color = Color.Blue;
            }
            else
            {
                color = Color.Red;
            }

        }

        public void Update(GameTime gT)
        {
            if(team == 1) Position += new Vector2(Speed, 0);
            if(team != 1) Position -= new Vector2(Speed, 0);
        }

        public void CheckForHit(List<Person> otherTeam)
        {
            bool hold = false;
            Person holdp = null;
            foreach(Person p in otherTeam)
            {
                if (p.Position.X <= this.Position.X) hold = true;
                if (hold)
                {
                    holdp = p;
                    break;
                }
            }

            if (hold)
            {
                holdp.Health -= (this.damage - holdp.Armor);
                this.active = false;
            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
}
