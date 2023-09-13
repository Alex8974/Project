using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using CollisionExample.Collisions;

namespace Project
{
    public class SwordsMan : Person
    {
        //the texture
        public Texture2D texture;

        // the team that the person is on
        private int team;

        //the size of the sprite 
        private int size = 50;

        /// <summary>
        /// the position from the top left cornner
        /// </summary>
        public override Vector2 Position { get; set; }

        /// <summary>
        /// the health of the person
        /// </summary>
        public override int Health { get; set; } = 8;

        /// <summary>
        /// the speed of the person
        /// </summary>
        public override int Speed { get; set; } = 30;
        public override int Damage { get; set; } = 4;
        public override int Armor { get; } = 2;

        public override bool IsAlive { get; set; } = true;

        private BoundingRectangle bounds;
        public override BoundingRectangle Bounds { 
            get 
            {
                return bounds;
            } 
        }

        public SwordsMan(ContentManager c, string Team)
        {
            texture = c.Load<Texture2D>("Penguin2");
            

            if (Team == "Team1")
            {
                Position = new Vector2(50, 300);
                team = 1;
            }
            else if (Team == "Team2") 
            {
                Position = new Vector2(650, 300);
                team = -1; 
            }
            else throw new Exception($"bad team name: {Team}");

            bounds = new BoundingRectangle(Position, size, size);
        }

        public override void Move(GameTime gT)
        {
            Position += (new Vector2(Speed, 0) * (float)gT.ElapsedGameTime.TotalSeconds * team);
        }

        public override void Update(GameTime gT)
        {
            bounds.X = Position.X;
            bounds.Y = Position.Y;
            

            
            Move(gT);

            Speed = 30;

        }

        public override void Draw(SpriteBatch s)
        {
            if(this.IsAlive) s.Draw(texture, Position, Color.White);
        }
    }
}
