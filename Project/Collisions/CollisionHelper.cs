using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollisionExample.Collisions
{
    public static class CollisionHelper
    {
        /// <summary>
        /// detects collision between two bounding circles
        /// </summary>
        /// <param name="a">the first circle</param>
        /// <param name="b">the second circle</param>
        /// <returns>returns true for collision</returns>
        public static bool Collides(BoundingCircle a, BoundingCircle b)
        {
            return Math.Pow( a.Radius + b.Radius, 2) >= 
                Math.Pow(a.Center.X - b.Center.X, 2) + 
                Math.Pow(a.Center.Y - b.Center.Y, 2);
        }

        /// <summary>
        /// detects collisions between two rectangles 
        /// </summary>
        /// <param name="a">the first rectangle</param>
        /// <param name="b"> the second rectange</param>
        /// <returns></returns>
        public static bool Collides(BoundingRectangle a, BoundingRectangle b)
        {
            return !(a.Right < b.Left || a.Left > b.Right ||
                     a.Top > b.Bottom || a.Bottom < b.Top);
        }

        /// <summary>
        /// detects a collision between a rectangle and a circle
        /// </summary>
        /// <param name="c">the circle</param>
        /// <param name="r">the rectangle </param>
        /// <returns>true if there is a collision, false otherwise</returns>
        public static bool Collides(BoundingCircle c, BoundingRectangle r)
        {
            float naerestX = MathHelper.Clamp(c.Center.X, r.Left, r.Right);
            float nearestY = MathHelper.Clamp(c.Center.Y, r.Top, r.Bottom);
            return Math.Pow(c.Radius, 2) >=
                Math.Pow(c.Center.X - naerestX, 2) +
                Math.Pow(c.Center.Y - nearestY, 2);
        }

        public static bool Collides(BoundingRectangle r, BoundingCircle c) => Collides(c, r);
    }
}
