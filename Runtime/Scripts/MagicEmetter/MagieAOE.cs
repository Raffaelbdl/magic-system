using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MagicSystem
{
    public class MagicAOE : MagicEmetter
    {
        public MagicAOE()
        {
            MagicType = MagicType.MagicAOE;
            offsetMagicFromPlayer = false;
        }

        protected override void Awake()
        {
            base.Awake();
            collector = new MagicCollectorOnRadius(this);
        }

        protected override IEnumerator OnMagic()
        {
            float _time = 0f;
            float duration = 0.5f * MagicStats.MagicDuration;

            while (_time < duration)
            {
                _time += Time.deltaTime;
                if (_time < 0.2f * duration)
                {
                    float scaleUp = Mathf.InverseLerp(0f, 0.2f * duration, _time);
                    float signX = Mathf.Sign(transform.root.localScale.x);
                    Vector3 scaler = (1f + scaleUp * MagicStats.AirAffinity) * Vector3.one;
                    scaler.x = signX * Mathf.Abs(scaler.x);
                    transform.root.localScale = scaler;
                }
                else if (_time > 0.9f * duration)
                {
                    float scaleDown = 1f - Mathf.InverseLerp(0.9f * duration, duration, _time);
                    float signX = Mathf.Sign(transform.root.localScale.x);
                    Vector3 scaler = (1f + scaleDown * MagicStats.AirAffinity) * Vector3.one;
                    scaler.x = signX * Mathf.Abs(scaler.x);
                    transform.root.localScale = scaler;
                }
                yield return new WaitForEndOfFrame();
            }

            transform.root.localScale = Vector3.one;
        }
    }
}