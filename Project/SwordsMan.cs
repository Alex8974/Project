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
using SharpDX.DirectWrite;
using Microsoft.Xna.Framework.Audio;
//using SharpDX.Direct2D1;

namespace Project
{
    public class SwordsMan : Person
    {
        //the texture
        private Random r = new Random();
        private SoundEffect swordSlash;

        //public Vector2 Position { get; set; }

        public override int Health { get; set; } = 8;

        public override int Speed { get; set; } = 30;
        public override int Damage { get; } = 4;
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
            if(Team == "Team1") texture = c.Load<Texture2D>("Team1KnightAllWalkingAnimations");
            if (Team == "Team2") texture = c.Load<Texture2D>("Team2KnightAllanimaitons");

            if (Team == "Team1")
            {
                Position = new Vector2(50, 329);
                team = 1;
            }
            else if (Team == "Team2") 
            {
                Position = new Vector2(715, 329);
                team = -1; 
            }
            else throw new Exception($"bad team name: {Team}");
            swordSlash = c.Load<SoundEffect>("hitHurtProject");

            bounds = new BoundingRectangle(Position, size, size);
        }

        public override void CheckForAttack(List<Person> otherp)
        {
 
            for(int i = 0; i < otherp.Count; i++)
            {
                if( CollisionHelper.Collides(this.Bounds, otherp[i].Bounds) && this.IsAlive && otherp[i].IsAlive)
                {
                    Speed = 0;
                    Attack(otherp[i]);
                }
            }

        }

        public override void Attack(Person other)
        {            
            if(AttackCoolDown > 0.5)
            {
                frameRow = SpriteSheetPicker.SwordRight;
                int hold = r.Next(Damage - 2, Damage+1);
                other.Health -= (hold - other.Armor);
                AttackCoolDown = 0;
                swordSlash.Play();
            }
        }

        public override void Update(GameTime gT)
        {
            if (this.Health <= 0) this.IsAlive = false;

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
            }
        }
    }
}
