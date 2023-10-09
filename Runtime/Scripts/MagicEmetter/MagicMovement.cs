using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDashSpeedReceiver
{
    void OnDashSpeedReceived(float speed);
}

namespace MagicSystem
{
    public class MagicMovement : MagicEmetter
    {
        public MagicMovement()
        {
            MagicType = MagicType.MagicMovement;
            magicInBackOfPlayer = true;
            reverseDirection = true;
        }

        protected override void Awake()
        {
            base.Awake();
            collector = new MagicCollectorOnStraightRays(this, 10, 0.1f);
        }

        protected override IEnumerator OnMagic()
        {
            MessageDashSpeed(-5f * ParticleVelocity);

            float _time = 0f;
            float duration = MagicStats.MagicDuration;

            while (_time < MagicStats.MagicDuration)
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

            MessageDashSpeed(0f);
            transform.root.localScale = Vector3.one;
        }

        private void MessageDashSpeed(float value)
            => SendMessageUpwards("OnDashSpeedReceived", value, SendMessageOptions.DontRequireReceiver);
    }
}