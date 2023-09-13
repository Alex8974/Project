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
        public abstract int Health { get; set; }

        public abstract int Speed { get; set; }

        public abstract int Damage { get; set; }

        public abstract int Armor { get; }

        public abstract bool IsAlive { get; set; }

        public abstract BoundingRectangle Bounds { get; }

        public abstract Vector2 Position { get; set; }

        public abstract void Move(GameTime gT);

        public abstract void Update(GameTime gT);

        public abstract void Draw(SpriteBatch s);

    }
}
