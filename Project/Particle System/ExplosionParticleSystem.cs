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
    public class ExplosionParticleSystem : ParticleSystem
    {
        public ExplosionParticleSystem(Game game, int maxExplosions) : base(game, maxExplosions *20)
        {

        }

        protected override void InitializeConstants()
        {
            textureFilename = "explosion";
            minNumParticles = 15;
            maxNumParticles = 20;


            blendState = BlendState.Additive;
            DrawOrder = AdditiveBlendDrawOrder;
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            var velocity = RandomHelper.NextDirection() * RandomHelper.NextFloat(0.1f, 0.5f);

            var lifetime = RandomHelper.NextFloat(0.5f, 1.0f);

            var acceleration = -velocity / lifetime;

            var rotation = RandomHelper.NextFloat(0, MathHelper.TwoPi);

            var angularVelocity = RandomHelper.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver4);

            p.Initialize(where, velocity / 4, acceleration/4, lifetime: lifetime, rotation: rotation, angularVelocity: angularVelocity);
        }

        protected override void UpdateParticle(ref Particle particle, float dt)
        {
            base.UpdateParticle(ref particle, dt);

            float normalizedLifeTime = particle.TimeSinceStart / particle.Lifetime;

            float alpha = 4 * normalizedLifeTime * (1 - normalizedLifeTime);

            particle.Color = Color.White * alpha;

            particle.Scale = .1f + .025f * normalizedLifeTime;
        }

        public void PlaceExplosion(Vector2 where) => AddParticles(where);

    }
}
