using CollisionExample.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Dragon : Person
    {
        /// <summary>
        /// how long it takes to attack
        /// </summary>
        private double AttackTime;

        /// <summary>
        /// the texture of the dragon and animation frame
        /// </summary>
        private Texture2D texture;
        private int textureRow;
        private int textureCol = 1;


        public override int Health { get; set; }

        public override double AttackCoolDown { get; set; }

        public override int Speed { get; set; } = 10;
        public override int Damage { get; }

        public override int Armor { get; }

        public override bool IsAlive { get; set; }

        private BoundingRectangle bounds;
        public override BoundingRectangle Bounds { get { return bounds; } }

        public Dragon(ContentManager c, int Team)
        {
            Health = 20;
            Speed = 10;
            Damage = 10;
            Armor = 3;
            IsAlive = true;
            Position = new Vector2(650, 200);
            this.team = Team;
            bounds = new BoundingRectangle(Position, 144, 100);
            texture = c.Load<Texture2D>("flying_dragon-red");
        }

        public override void Attack(Person other)
        {
            throw new NotImplementedException();
        }

        public override void CheckForAttack(List<Person> otherp)
        {
            //throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch s, GameTime gT)
        {
            Rectangle source = new Rectangle(textureRow *144 , (int)textureCol * 128, 144, 128);
            animationTimer += gT.ElapsedGameTime.TotalSeconds;

            if ((int)frameRow > 3)
            {
                if (animationTimer > 0.2f)
                {
                    if (textureRow < 2)
                    {
                        textureRow++;
                    }
                    else
                    {
                        textureRow = 0;
                    }
                    animationTimer = 0;
                }
            }
            else
            {
                //source = new Rectangle(0, (int)frameRow * 16, 16, 16);
            }



            if (this.IsAlive)
            {
                if (team == 1) s.Draw(texture, Position, source, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
                if (team == -1) s.Draw(texture, Position, source, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.FlipHorizontally, 0);
            }
//            s.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y, (int)bounds.X, (int)bounds.Y), new Rectangle(0,0,144,128),  Color.Green);
        }

        public override void Update(GameTime gT)
        {
            
            Move(gT);
            bounds.X = Position.X;
            bounds.Y = Position.Y;
        }
    }
}
