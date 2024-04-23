﻿using Cascade.Core.Configs;

namespace Cascade.Core.Graphics.GraphicalObjects.Particles
{
    /// <summary>
    /// A wrapper of Luminance's <see cref="Particle"/> class which some of its own extra built-in utilities, such as foreground parallax and
    /// caching data for trail drawing.
    /// </summary>
    public abstract class CasParticle : Particle
    {
        /// <summary>
        /// Controls how much the particle moves in the foreground. In order to use this effect,
        /// please use <see cref="GetDrawPositionWithParallax"/> when drawing your particle via
        /// <see cref="Particle.Draw(SpriteBatch)"/>. Clamps between 1f and 100f.
        /// </summary>
        public float ParallaxStrength;

        /// <summary>
        /// A constantly updating collection of old positions for this particle. Can be primarily 
        /// used for drawing trails on particles. MUST be used with <see cref="TrailingLength"/>
        /// in order to properly function.
        /// </summary>
        public List<Vector2> OldPositions;

        /// <summary>
        /// A constantly updating collection of old rotations for this particle. Can be primarily 
        /// used for drawing trails on particles. MUST be used with <see cref="TrailingLength"/>
        /// in order to properly function.
        /// </summary>
        public List<float> OldRotations;

        /// <summary>
        /// A constantly updating collection of old directions for this particle. Can be primarily 
        /// used for drawing trails on particles. MUST be used with <see cref="TrailingLength"/>
        /// in order to properly function.
        /// </summary>
        public List<int> OldDirections;

        /// <summary>
        /// The type of trailing mode this particle should use if you're drawing a trail.
        /// <br />Trailing Mode -1: Default value; nothing is remembered.
        /// <br />Trailing Mode 0: Position, Rotation and Direction data are remembered.
        /// <br />Trailing Mode 1: Same as 1, but attempts to smooth out old data via interpolation.
        /// </summary>
        public virtual int TrailingMode => -1;

        /// <summary>
        /// The length of the trail you'd like to draw on this particle.
        /// </summary>
        public virtual int TrailingLength => 0;

        /// <summary>
        /// ONLY use this method when spawning <see cref="CasParticle"/> instances. 
        /// </summary>
        /// <returns></returns>
        public CasParticle SpawnCasParticle()
        {
            Spawn();
            if (CasParticleManager.ActiveCasParticles.Count > GraphicalConfig.Instance.ParticleLimit)
                CasParticleManager.ActiveCasParticles.First().Kill();

            CasParticleManager.ActiveCasParticles.Add(this);

            OldPositions = [];
            OldRotations = [];
            OldDirections = [];
            for (int i = 0; i < TrailingLength; i++)
            {
                OldPositions[i] = Position;
                OldRotations[i] = Rotation;
                OldDirections[i] = Direction;
            }

            return this;
        }

        public Vector2 GetDrawPositionWithParallax() => Position - Main.screenPosition * ParallaxStrength;
    }
}