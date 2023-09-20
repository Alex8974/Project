using CollisionExample.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public abstract class Person
    {
        protected const float ANIMATION_TIMER = 0.1f;
        protected int size = 16;
        public SpriteSheetPicker frameRow = SpriteSheetPicker.WalkingRight;
        protected int animationFrame = 0;
        protected double animationTimer;
        protected bool attacking;
        public int team { get; set; }

        /// <summary>
        /// the health of the person
        /// </summary>
        public abstract int Health { get; set; }

        /// <summary>
        /// the time since last attck
        /// </summary>
        public abstract double AttackCoolDown { get; set; }

        /// <summary>
        /// their movement spped
        /// </summary>
        public abstract int Speed { get; set; }

        /// <summary>
        /// the attack damage
        /// </summary>
        public abstract int Damage { get; set; }

        /// <summary>
        /// the armor negates some of the damage
        /// </summary>
        public abstract int Armor { get; }

        /// <summary>
        /// if they are still alive
        /// </summary>
        public abstract bool IsAlive { get; set; }

        /// <summary>
        /// the collision box
        /// </summary>
        public abstract BoundingRectangle Bounds { get; }

        /// <summary>
        /// the position in the top left corner
        /// </summary>
        public abstract Vector2 Position { get; set; }

        /// <summary>
        /// moves the person 
        /// </summary>
        /// <param name="gT">the current game time</param>
        public void Move(GameTime gT)
        {
            frameRow = SpriteSheetPicker.WalkingRight;
            Position += (new Vector2(Speed, 0) * (float)gT.ElapsedGameTime.TotalSeconds * team);
        }

        /// <summary>
        /// makes the person attack 
        /// </summary>
        /// <param name="other">the person beign attacked</param>
        public abstract void Attack(Person other);


        public abstract void CheckForAttack(List<Person> otherp);

        /// <summary>
        /// updates the person 
        /// </summary>
        /// <param name="gT">the game time</param>
        public abstract void Update(GameTime gT);

        /// <summary>
        /// draws the person
        /// </summary>
        /// <param name="s">the spritebatch</param>
        /// <param name="gT">the game time</param>
        public abstract void Draw(SpriteBatch s, GameTime gT);
               

    }
}
