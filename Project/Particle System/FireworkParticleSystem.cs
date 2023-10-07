using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSystemExample
{
    public class FireworkParticleSystem : ParticleSystem
    {
        Color[] colors = new Color[]
        {
            Color.Fuchsia,
            Color.Red,
            Color.Crimson,
            Color.CadetBlue,
            Color.Aqua,
            Color.HotPink,
            Color.LimeGreen
        };

        Color color;

        public FireworkParticleSystem(Game game, int maxExplosions) : base(game, maxExplosions *25)
        {

        }

        protected override void InitializeConstants()
        {
            textureFilename = "circle";
            minNumParticles = 20;
            maxNumParticles = 25;


            blendState = BlendState.Additive;
            DrawOrder = AdditiveBlendDrawOrder;
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            var velocity = RandomHelper.NextDirection() * RandomHelper.NextFloat(40, 200);

            var lifetime = RandomHelper.NextFloat(0.5f, 1.0f);

            var acceleration = -velocity / lifetime;

            var rotation = RandomHelper.NextFloat(0, MathHelper.TwoPi);

            var angularVelocity = RandomHelper.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver4);

            var scale = RandomHelper.NextFloat(4, 6);

            p.Initialize(where, velocity, acceleration, color, lifetime: lifetime, rotation: rotation, angularVelocity: angularVelocity, scale: scale);
        }

        protected override void UpdateParticle(ref Particle particle, float dt)
        {
            base.UpdateParticle(ref particle, dt);

            float normalizedLifeTime = particle.TimeSinceStart / particle.Lifetime;

            particle.Scale = .1f + .25f * normalizedLifeTime;
        }

        public void PlaceFireWork(Vector2 where)
        {
            color = colors[RandomHelper.Next(colors.Length)];
            AddParticles(where);
        }


    }
}
