using CollisionExample.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Archer : Person
    {
        public override int Health { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override int Speed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override int Damage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override int Armor => throw new NotImplementedException();

        public override Vector2 Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override BoundingRectangle Bounds { get; }
        public override bool IsAlive { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Draw(SpriteBatch s)
        {
            throw new NotImplementedException();
        }

        public override void Move(GameTime gT)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gT)
        {
            throw new NotImplementedException();
        }
    }
}
