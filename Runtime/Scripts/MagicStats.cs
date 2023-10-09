using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MagicSystem
{
    public enum Affinity { Fire, Air, Earth, Water }

    [Serializable]
    public class MagicStats
    {
        public float MagicIntensity = 0f;
        public float MagicDuration = 0f;

        public Affinity MainAffinity;
        [Range(0f, 1f)] public float FireAffinity = 0f;
        [Range(0f, 1f)] public float AirAffinity = 0f;
        [Range(0f, 1f)] public float EarthAffinity = 0f;
        [Range(0f, 1f)] public float WaterAffinity = 0f;

        public float AffinityValue(Affinity affinity)
        {
            switch (affinity)
            {
                case Affinity.Fire:
                    return FireAffinity;
                case Affinity.Air:
                    return AirAffinity;
                case Affinity.Water:
                    return WaterAffinity;
                case Affinity.Earth:
                    return EarthAffinity;
                default:
                    return 0f;
            }
        }
    }

    [Serializable]
    public class MagicConditions
    {
        [Range(0f, 1f)] public float FireThreshold;
        [Range(0f, 1f)] public float AirThreshold;
        [Range(0f, 1f)] public float EarthThreshold;
        [Range(0f, 1f)] public float WaterThreshold;

        public bool MeetsThresholds(MagicStats stats)
        {
            if (stats.FireAffinity < FireThreshold && FireThreshold > 0) return false;
            if (stats.AirAffinity < AirThreshold && AirThreshold > 0) return false;
            if (stats.EarthAffinity < EarthThreshold && EarthThreshold > 0) return false;
            if (stats.WaterAffinity < WaterThreshold && WaterThreshold > 0) return false;
            return true;
        }
    }

    [Serializable]
    public class MagicShape
    {
        public float MainAffinityStartSize = 0.3f;
        public float OtherAffinityStartSize = 0.2f;
        public float DefaultParticleVelocity = 0.6f;
    }
}