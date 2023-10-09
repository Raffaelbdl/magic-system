using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicSystem
{
    public class MagicParticle : MonoBehaviour
    {
        public string MagicInteractableTag = "MagicInteractable";
        public MagicStats magicStats;
        private ParticleSystem ps;
        private void Awake() { ps = GetComponentInParent<ParticleSystem>(); }

        private void OnParticleTrigger()
        {
            List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
            int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter, out ParticleSystem.ColliderData enterData);
            for (int i = 0; i < numEnter; i++)
            {
                int nCols = enterData.GetColliderCount(i);
                for (int c = 0; c < nCols; c++)
                {
                    Component col = enterData.GetCollider(i, c);
                    if (col.CompareTag(MagicInteractableTag))
                        col.GetComponent<MagicInteractable>().Interact(magicStats);
                }
            }
            ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        }
    }
}