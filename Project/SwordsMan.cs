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
//using SharpDX.Direct2D1;

namespace Project
{
    public class SwordsMan : Person
    {
        //the texture
        public Texture2D texture;

        public override Vector2 Position { get; set; }

        public override int Health { get; set; } = 8;

        public override int Speed { get; set; } = 30;
        public override int Damage { get; set; } = 4;
        public override int Armor { get; } = 2;
        public override double AttackCoolDown { get; set; } = 0;


        public override bool IsAlive { get; set; } = true;


        private BoundingRectangle bounds;
        /// <summary>
        /// the collision rectangle
        /// </summary>
        public override BoundingRectangle Bounds { get { return bounds; } }


        public SwordsMan(ContentManager c, string Team)
        {
            if(Team == "Team1") texture = c.Load<Texture2D>("Team1Knight");
            if (Team == "Team2") texture = c.Load<Texture2D>("Team2Knight");



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

        public override void Attack(Person other)
        {
            if(AttackCoolDown > 0.5)
            {
                frameRow = SpriteSheetPicker.BowRight;
                other.Health -= (this.Damage - other.Armor);
                AttackCoolDown = 0;
            }
        }

        public override void Update(GameTime gT)
        {
            this.AttackCoolDown += gT.ElapsedGameTime.TotalSeconds;
            bounds.X = Position.X;
            bounds.Y = Position.Y;      
            
            if(Speed > 0) { Move(gT); }
            

            Speed = 30;

        }

        public override void Draw(SpriteBatch s, GameTime gT)
        {
            Rectangle source = new Rectangle(animationFrame * 16 , (int)frameRow * 16, 16, 16);
            animationTimer += gT.ElapsedGameTime.TotalSeconds;

            if((int)frameRow > 3)
            {
                if (animationTimer > ANIMATION_TIMER)
                {
                    if (animationFrame < 3)
                    {
                        animationFrame++;
                    }
                    else
                    {
                        animationFrame = 0;
                    }
                    animationTimer = 0;
                }
            }
            else
            {
                source = new Rectangle(0, (int)frameRow * 16, 16, 16);
            }

            

            if (this.IsAlive)
            {
                if(team == 1) s.Draw(texture, Position, source ,Color.White, 0 , new Vector2(0,0), 2f, SpriteEffects.None, 0);
                if(team == -1) s.Draw(texture, Position, source ,Color.White, 0 , new Vector2(0,0), 2f, SpriteEffects.FlipHorizontally, 0);
      //spriteBatch.Draw(texture, position, null, Color, 0, new Vector2(64, 64), 0.25f, spriteEffects, 0);
            }
        }
    }
}
