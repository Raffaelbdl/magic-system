using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicSystem
{
    public abstract class MagicInteractable : MonoBehaviour
    {
        [SerializeField] protected MagicConditions magicConditions;
        protected bool canInteract = true;

        public void Interact(MagicStats stats)
        {
            if (!CheckCanInteract(stats)) return;
            OnInteract(stats);
        }

        private bool CheckCanInteract(MagicStats stats)
        {
            if (!canInteract) return false;
            if (!magicConditions.MeetsThresholds(stats)) return false;
            return true;
        }

        public abstract void OnInteract(MagicStats stats);
    }
}