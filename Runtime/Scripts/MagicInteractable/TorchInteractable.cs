using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicSystem
{
    public class TorchInteractable : MagicInteractable
    {
        private SpriteRenderer m_renderer;
        [SerializeField] private Sprite unlightSprite;
        [SerializeField] private Sprite lightSprite;

        public override void OnInteract(MagicStats stats)
        {
            canInteract = false;
            m_renderer.sprite = lightSprite;
        }

        public void Awake()
        {
            m_renderer = GetComponent<SpriteRenderer>();
            m_renderer.sprite = unlightSprite;
        }
    }
}