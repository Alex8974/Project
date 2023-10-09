using CollisionExample.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ParticleSystemExample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Dragon : Person
    {
        SpriteFont Font;

        /// <summary>
        /// how long it takes to attack
        /// </summary>
        private double AttackTime;

        /// <summary>
        /// the texture of the dragon and animation frame
        /// </summary>
        private int textureRow;
        private int textureCol = 1;

        private Texture2D healthTexture;

        public override int Health { get; set; }

        public override double AttackCoolDown { get; set; }

        public override int Speed { get; set; } = 10;
        public override int Damage { get; }

        public override int Armor { get; }

        public override bool IsAlive { get; set; }

        private BoundingRectangle bounds;
        public override BoundingRectangle Bounds { get { return bounds; } }

        private BoundingRectangle attackbounds;
        public BoundingRectangle AttackBounds { get { return attackbounds; } }

        private ExplosionParticleSystem _explosion;


        public Dragon(ContentManager c, int Team, ExplosionParticleSystem _explosion)
        {
            Font = c.Load<SpriteFont>("bangers");
            Health = 48;
            Speed = 10;
            Damage = 10;
            Armor = 3;
            IsAlive = true;
            Position = new Vector2(650, 200);
            this.team = Team;
            bounds = new BoundingRectangle(Position, 144, 100);
            texture = c.Load<Texture2D>("flying_dragon-red");
            healthTexture = c.Load<Texture2D>("HealthBar");
            attackbounds = new BoundingRectangle(Position, 16, 16);
            attackbounds.Y = 328;
            this._explosion = _explosion;
            AttackTime = 3;
        }

        public override void Attack(Person other)
        {
            attacking = true;

            if(AttackCoolDown > AttackTime)
            {
                Vector2 attackSpot = new Vector2(attackbounds.X + 5, attackbounds.Y + 8);
                _explosion.PlaceExplosion(attackSpot);
                attackSpot.Y -= 8;
                attackSpot.X += 4;
                _explosion.PlaceExplosion(attackSpot);
                attackSpot.Y -= 16;
                _explosion.PlaceExplosion(attackSpot);
                attackSpot.Y -= 16;
                _explosion.PlaceExplosion(attackSpot);
                AttackCoolDown = 0;
                other.Health -= this.Damage;
            }            
        }

        public override void CheckForAttack(List<Person> otherp)
        {
            Person holdp = null;
            bool hold = false;
            foreach (var p in otherp)
            {
                hold = CollisionHelper.Collides(AttackBounds, p.Bounds);
                holdp = p;
                if (hold) break;
            }
            if (hold == true)
            {
                Attack(holdp);
                Speed = 0;
            }
            else attacking = false;
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

                Rectangle ss = new Rectangle( 0 , (int)(Health / 6 * 6), 17 , 6 );                
                Vector2 healtbarpos = Position + new Vector2(20, 6);
                s.Draw(healthTexture, healtbarpos , ss ,Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0);
                //s.DrawString(Font, $"{this.Health}", new Vector2(200, 200), Color.Black);
            }
        }

        public override void Update(GameTime gT)
        {
            if (Health <= 0) IsAlive = false;
            if (attacking == false) 
            {
                Speed = 10;
                Move(gT); 
            }

            this.AttackCoolDown += gT.ElapsedGameTime.TotalSeconds;
            bounds.X = Position.X;
            bounds.Y = Position.Y;
            attackbounds.X = Position.X;
        }
    }
}
