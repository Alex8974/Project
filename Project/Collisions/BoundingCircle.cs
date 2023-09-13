using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollisionExample.Collisions
{
    /// <summary>
    /// a sturct representing circular bounds
    /// </summary>
    public struct BoundingCircle
    {
        /// <summary>
        /// the center of the bounding circle
        /// </summary>
        public Vector2 Center;

        /// <summary>
        /// the radius of the bounding circle
        /// </summary>
        public float Radius;

        /// <summary>
        /// constructs a new bounding circle
        /// </summary>
        /// <param name="center">the center</param>
        /// <param name="radius">the radius</param>
        public BoundingCircle(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        /// <summary>
        /// tests for a collision between this and another bounding circle
        /// </summary>
        /// <param name="other">the other circle </param>
        /// <returns>if there is a collision true, otherwise false</returns>
        public bool CollidesWith(BoundingCircle other)
        {
            return CollisionHelper.Collides(this, other);
        }

        public bool CollidesWith(BoundingRectangle other)
        {
            return CollisionHelper.Collides(this, other);
        }
    }
}
