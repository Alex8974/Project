using System;
using System.Collections.Generic;
using System.Linq;
//using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using CollisionExample.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project
{
    public class Arrow
    {
        public Vector2 ShotFrom;

        public int Speed = 40;

        public Arrow(Vector2 shotFrom)
        {
            ShotFrom = shotFrom;
        }
    }
}
