using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicSystem
{
    public class MagicDefense : MagicEmetter
    {
        public MagicDefense()
        {
            MagicType = MagicType.MagicDefense;
            offsetMagicFromPlayer = false;
        }

        protected override void Awake()
        {
            base.Awake();
            collector = new MagicCollectorOnRadius(this);
        }

        #region Magic

        protected override void SetParticleVelocity()
        {
            void SetAffinityRadius(Affinity affinity)
            {
                ParticleSystem.ShapeModule main = Particles.GetParticles(affinity).shape;
                main.radius = ParticleVelocity;
            }
            SetAffinityRadius(Affinity.Fire);
            SetAffinityRadius(Affinity.Air);
            SetAffinityRadius(Affinity.Earth);
            SetAffinityRadius(Affinity.Water);
        }

        #endregion
    }
}
