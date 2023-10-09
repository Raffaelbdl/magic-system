using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicSystem
{
    public abstract class MagicCollector
    {
        protected readonly MagicEmetter magicEmetter;
        protected readonly string interactableTag;
        protected readonly Transform magicParent;
        protected readonly Collider2D emetterCollider;

        public MagicCollector(MagicEmetter _emetter)
        {
            magicEmetter = _emetter;
            interactableTag = _emetter.MagicInteractableTag;
            magicParent = _emetter.magicParent;
            emetterCollider = _emetter.Properties.Collider2D;
        }

        protected HashSet<Collider2D> collectedColliders = new HashSet<Collider2D>();

        public void EmptyColliderCache(MagicParticles magicParticles)
        {
            foreach (Collider2D EmetterCollider in collectedColliders)
                magicParticles.RemoveCollider(EmetterCollider);
            collectedColliders = new HashSet<Collider2D>();
        }

        public abstract void CollectColliders(MagicParticles magicParticles);
        public abstract void DrawCollectionArea(bool magicPlaying);
        protected void CollectColliderOnRaycastHit(MagicParticles magicParticles, RaycastHit2D hit)
        {
            if (hit.collider == null) return;
            if (hit.collider == emetterCollider) return;
            if (!hit.collider.CompareTag(interactableTag)) return;
            if (collectedColliders.Contains(hit.collider)) return;

            collectedColliders.Add(hit.collider);
            magicParticles.AddCollider(hit.collider);
        }
    }

    public class MagicCollectorOnStraightRays : MagicCollector
    {
        private readonly int nRays;
        private readonly float halfExtent;

        public MagicCollectorOnStraightRays(MagicEmetter parent, int _nRays, float _halfExtent) : base(parent)
        {
            nRays = _nRays;
            halfExtent = _halfExtent;
        }

        public override void CollectColliders(MagicParticles magicParticles)
        {
            for (int i = 0; i < nRays; i++)
            {
                float offset = halfExtent * (i - nRays / 2);
                RaycastHit2D hit = Physics2D.Raycast(
                    magicParent.position + offset * magicParent.right,
                    magicParent.up,
                    magicEmetter.ParticleVelocity
                );
                CollectColliderOnRaycastHit(magicParticles, hit);
            }
        }

        public override void DrawCollectionArea(bool magicPlaying)
        {
            Gizmos.color = magicPlaying ? Color.red : Color.white;
            for (int i = 0; i < nRays; i++)
            {
                float offset = halfExtent * (i - nRays / 2);
                Gizmos.DrawRay(
                    magicParent.position + magicParent.right * offset,
                    magicParent.up * magicEmetter.ParticleVelocity
                );
            }
        }
    }

    public class MagicCollectorOnConeRays : MagicCollector
    {
        private readonly int nRays;
        private readonly float halfExtent;
        private readonly float maxAngle;

        public MagicCollectorOnConeRays(MagicEmetter _emetter, int _nRays, float _halfExtent, float _maxAngle) : base(_emetter)
        {
            nRays = _nRays;
            halfExtent = _halfExtent;
            maxAngle = _maxAngle;
        }

        public override void CollectColliders(MagicParticles magicParticles)
        {
            for (int i = 0; i < nRays; i++)
            {
                float offset = halfExtent * (i - nRays / 2);
                float angle = RayAngle(offset);
                RaycastHit2D hit = Physics2D.Raycast(
                    magicParent.position + magicParent.right * offset,
                    Quaternion.Euler(0f, 0f, angle) * magicParent.up,
                    magicEmetter.ParticleVelocity
                );
                CollectColliderOnRaycastHit(magicParticles, hit);
            }
        }

        public override void DrawCollectionArea(bool magicPlaying)
        {
            Gizmos.color = magicPlaying ? Color.red : Color.white;
            for (int i = 0; i < nRays; i++)
            {
                float offset = halfExtent * (i - nRays / 2);
                Vector3 start = magicParent.position + offset * magicParent.right;
                float angle = RayAngle(offset);
                Vector3 direction = Quaternion.Euler(0f, 0f, angle) * magicParent.up * magicEmetter.ParticleVelocity;

                Gizmos.DrawRay(start, direction);
            }
        }

        private float RayAngle(float offset)
        {
            return -Mathf.Sign(offset) * maxAngle * Mathf.InverseLerp(0, halfExtent * nRays / 2, Mathf.Abs(offset));
        }
    }

    public class MagicCollectorOnRadius : MagicCollector
    {
        public MagicCollectorOnRadius(MagicEmetter _emetter) : base(_emetter) { }

        public override void CollectColliders(MagicParticles magicParticles)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(
                magicParent.position, magicEmetter.ParticleVelocity + 1f);

            foreach (Collider2D _col in colliders)
            {
                if (_col == emetterCollider) continue;
                if (!_col.CompareTag(interactableTag)) continue;
                if (collectedColliders.Contains(_col)) continue;

                collectedColliders.Add(_col);
                magicParticles.AddCollider(_col);
            }
        }

        public override void DrawCollectionArea(bool magicPlaying)
        {
            Gizmos.color = magicPlaying ? Color.red : Color.white;
            Gizmos.DrawWireSphere(magicEmetter.transform.position, magicEmetter.ParticleVelocity + 1f);
        }
    }
}