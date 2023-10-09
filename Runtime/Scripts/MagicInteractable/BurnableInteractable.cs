using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using MagicSystem;

public class BurnableInteractable : MagicInteractable
{
    private SpriteRenderer m_renderer;
    private ParticleSystem fireParticles;
    [SerializeField] private Gradient burnGradient;
    [SerializeField] private float timeToDestroy = 1f;

    public override void OnInteract(MagicStats stats)
    {
        canInteract = false;
        if (fireParticles) fireParticles.Play();
        StartCoroutine(ChangeColorUntilDestroy());
    }

    public void Awake()
    {
        m_renderer = GetComponent<SpriteRenderer>();
        fireParticles = GetComponent<ParticleSystem>();
    }

    private IEnumerator ChangeColorUntilDestroy()
    {
        float t = 0f;
        while (t <= timeToDestroy)
        {
            float invLerp = Mathf.InverseLerp(0, timeToDestroy, t);
            if (invLerp > 0.8f && fireParticles) fireParticles.Stop();
            m_renderer.color = burnGradient.Evaluate(invLerp);
            t += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }
}
