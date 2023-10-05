using CollisionExample.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public class Archer : Person
    {
        private Texture2D texture;

        public List<Arrow> arrows = new List<Arrow>();

        private ContentManager cm;
        public override int Health { get; set; } = 8;
        public override int Speed { get; set; } = 20;
        public override int Damage { get; set; } = 3;

        public override int Armor { get; } = 1;

        //public Vector2 Position { get; set; }

        private BoundingRectangle bounds;
        public override BoundingRectangle Bounds { get { return bounds; } }
        public override bool IsAlive { get; set; } = true;

        public override double AttackCoolDown { get; set; }

        public Archer(ContentManager c, string Team)
        {
            if (Team == "Team1") texture = c.Load<Texture2D>("Team1KnightAllWalkingAnimations");
            if (Team == "Team2") texture = c.Load<Texture2D>("Team2KnightAllanimaitons");


            if (Team == "Team1")
            {
                Position = new Vector2(50, 328);
                team = 1;
            }
            else if (Team == "Team2")
            {
                Position = new Vector2(650, 328);
                team = -1;
            }
            else throw new Exception($"bad team name: {Team}");
            bounds = new BoundingRectangle(Position, size, size);
            cm = c;
        }

        public override void CheckForAttack(List<Person> otherp)
        {
            Person holdp = null;
            bool hold = false;
            int i = 0;
            while (i < 10 && hold == false)
            {
                if (team == 1) bounds.X += (i * 10);
                else if (team == -1) bounds.X -= (i * 10);
                
                foreach(var p in otherp)
                {
                    hold = CollisionHelper.Collides(this.Bounds, p.Bounds);
                    holdp = p;
                    if(hold) break;
                }
                i++;
                bounds.X = Position.X;
            }
            if(hold == true)
            {
                Attack(holdp);
                Speed = 0;
            }

            foreach (Arrow a in arrows) a.CheckForHit(otherp);

            for (int j = 0; j < arrows.Count; j++) if (arrows[j].active == false) arrows.RemoveAt(j);
        }

        public override void Attack(Person other)
        {
            if (AttackCoolDown > 2.0)
            {
                if(team == 1) arrows.Add(new Arrow(Position + new Vector2(16,0), cm, team));
                if(team == 2) arrows.Add(new Arrow(Position, cm, team));
                AttackCoolDown = 0;
            }
        }

        public override void Update(GameTime gT)
        {
            if (Health <= 0) IsAlive = false;
            this.AttackCoolDown += gT.ElapsedGameTime.TotalSeconds;
            bounds.X = Position.X;
            bounds.Y = Position.Y;

            if (Speed > 0) 
            { 
                Move(gT);
                frameRow = SpriteSheetPicker.WalkingRighBow;
            }
            foreach (Arrow a in arrows) a.Update(gT);

            Speed = 20;
        }

        public override void Draw(SpriteBatch s, GameTime gT)
        {
            Rectangle source = new Rectangle(animationFrame * 16, (int)frameRow * 16, 16, 16);
            animationTimer += gT.ElapsedGameTime.TotalSeconds;

            if ((int)frameRow > 3)
            {
                if (animationTimer > ANIMATION_TIMER)
                {
                    if (animationFrame < 3) animationFrame++;                  
                    else animationFrame = 0;
                    
                    animationTimer = 0;
                }
            }
            else source = new Rectangle(0, (int)frameRow * 16, 16, 16);

            foreach (Arrow a in arrows) a.Draw(s);

            if (this.IsAlive)
            {
                if (team == 1) s.Draw(texture, Position, source, Color.White, 0, new Vector2(0, 0), 2f, SpriteEffects.None, 0);
                if (team == -1) s.Draw(texture, Position, source, Color.White, 0, new Vector2(0, 0), 2f, SpriteEffects.FlipHorizontally, 0);
                //spriteBatch.Draw(texture, position, null, Color, 0, new Vector2(64, 64), 0.25f, spriteEffects, 0);
            }
        }

        
    }
}
