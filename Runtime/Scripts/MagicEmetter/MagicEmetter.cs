using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MagicSystem
{
    public enum MagicType { MagicAttack, MagicDefense, MagicAOE, MagicMovement }
    public enum SkillNumber { Skill1, Skill2, Skill3, Skill4 }

    [Serializable]
    public class MagicParticles
    {
        public ParticleSystem FireParticles;
        public ParticleSystem AirParticles;
        public ParticleSystem EarthParticles;
        public ParticleSystem WaterParticles;

        public ParticleSystem GetParticles(Affinity affinity)
        {
            switch (affinity)
            {
                default:
                    return FireParticles;
                case Affinity.Air:
                    return AirParticles;
                case Affinity.Earth:
                    return EarthParticles;
                case Affinity.Water:
                    return WaterParticles;
            }
        }

        public void SetMagicStats(MagicStats stats)
        {
            FireParticles.GetComponentInChildren<MagicParticle>().magicStats = stats;
            AirParticles.GetComponentInChildren<MagicParticle>().magicStats = stats;
            EarthParticles.GetComponentInChildren<MagicParticle>().magicStats = stats;
            WaterParticles.GetComponentInChildren<MagicParticle>().magicStats = stats;
        }

        public void Play()
        {
            FireParticles.Play();
            AirParticles.Play();
            EarthParticles.Play();
            WaterParticles.Play();
        }

        public void Stop()
        {
            FireParticles.Stop();
            AirParticles.Stop();
            EarthParticles.Stop();
            WaterParticles.Stop();
        }

        public void AddCollider(Collider2D collider2D)
        {
            FireParticles.trigger.AddCollider(collider2D);
            AirParticles.trigger.AddCollider(collider2D);
            EarthParticles.trigger.AddCollider(collider2D);
            WaterParticles.trigger.AddCollider(collider2D);
        }

        public void RemoveCollider(Collider2D collider2D)
        {
            FireParticles.trigger.RemoveCollider(collider2D);
            AirParticles.trigger.RemoveCollider(collider2D);
            EarthParticles.trigger.RemoveCollider(collider2D);
            WaterParticles.trigger.RemoveCollider(collider2D);
        }
    }

    [Serializable]
    public class EmetterProperties
    {
        public CircleCollider2D Collider2D;
        public float Radius = 1f;
        public float GetRadius() { return Collider2D != null ? Collider2D.radius : Radius; }
    }

    public abstract class MagicEmetter : MonoBehaviour
    {
        #region Settings
        private MagicSystemSettings settings;
        public string MagicInteractableTag => settings.MagicInteractableTag;
        #endregion

        protected MagicCollector collector;
        protected MagicType MagicType;
        [SerializeField] protected SkillNumber Skill;

        [SerializeField] protected MagicShape MagicShape;
        [SerializeField] protected MagicStats MagicStats;

        [SerializeField] protected MagicParticles Particles;
        [field: SerializeField] public EmetterProperties Properties { get; private set; }

        [SerializeField] protected MagicSkillsInterface SkillsInterface;
        [field: SerializeField] public Transform magicParent { get; private set; }
        [SerializeField] protected MagicController magicController;

        [SerializeField] protected bool reverseDirection = false;
        public virtual float ParticleVelocity
        {
            get => MagicStats.MagicIntensity * MagicShape.DefaultParticleVelocity * 0.5f * (reverseDirection ? -1f : 1f);
        }

        protected bool isMagicPlaying = false;
        protected bool offsetMagicFromPlayer = true;
        protected bool magicInBackOfPlayer = false;

        protected HashSet<Collider2D> _possibleInteractables = new HashSet<Collider2D>();

        #region Monobehavior
        protected virtual void Awake()
        {
            settings = Resources.Load<MagicSystemSettings>("MagicSystemSettings");

            Properties.Collider2D = GetComponentInParent<CircleCollider2D>();
        }

        private void Start()
        {
            magicController = FindObjectOfType<MagicController>();

            SkillsInterface = FindFirstObjectByType<MagicSkillsInterface>();
            if (SkillsInterface != null)
                SkillsInterface.onMagicSkillsChange += UpdateMagicStats;
            EquipSkill();
        }

        private void OnDisable()
        {
            if (SkillsInterface != null)
                SkillsInterface.onMagicSkillsChange -= UpdateMagicStats;
            DisequipSkill();
        }

        private void Update()
        {
            Vector2 direction = (magicController.Inputs.mousePosition - (Vector2)transform.position).normalized;

            magicParent.up = direction;
            magicParent.position = transform.position;

            if (offsetMagicFromPlayer)
                magicParent.position += (magicInBackOfPlayer ? -1f : 1f) * Properties.GetRadius() * magicParent.up;
        }

        private void OnDrawGizmosSelected() => collector?.DrawCollectionArea(isMagicPlaying);

        protected virtual void FixedUpdate() { if (isMagicPlaying) collector?.CollectColliders(Particles); }

        #endregion

        public void EquipSkill() => GetSkillInputAction(Skill).performed += Activate;
        public void DisequipSkill() => GetSkillInputAction(Skill).performed -= Activate;
        protected virtual void Activate(InputAction.CallbackContext context) { if (!isMagicPlaying && MagicStats != null) StartCoroutine(StartMagic()); }

        private void UpdateMagicStats(MagicStats[] stats, int[] valids)
        {
            int index = GetSkillNumber(Skill);
            if (!valids.Contains(index)) return;

            MagicStats = stats[index];
            if (MagicStats != null) Particles.SetMagicStats(MagicStats);
        }


        #region Magic
        protected void SetParticleSize()
        {
            float GetAffinitySize(Affinity affinity)
            {
                float size = 0f;
                if (MagicStats.MainAffinity == affinity) size += MagicShape.MainAffinityStartSize;
                size += MagicShape.OtherAffinityStartSize * MagicStats.AffinityValue(affinity);
                size *= MagicStats.MagicIntensity;
                return size;
            }
            void SetAffinitySize(Affinity affinity)
            {
                ParticleSystem.MainModule main = Particles.GetParticles(affinity).main;
                main.startSize = GetAffinitySize(affinity);
            }

            SetAffinitySize(Affinity.Fire);
            SetAffinitySize(Affinity.Air);
            SetAffinitySize(Affinity.Earth);
            SetAffinitySize(Affinity.Water);
        }

        protected virtual void SetParticleVelocity()
        {
            void SetAffinityVelocity(Affinity affinity)
            {
                ParticleSystem.VelocityOverLifetimeModule velocity =
                    Particles.GetParticles(affinity).velocityOverLifetime;
                velocity.speedModifier = ParticleVelocity;
            }

            SetAffinityVelocity(Affinity.Fire);
            SetAffinityVelocity(Affinity.Air);
            SetAffinityVelocity(Affinity.Earth);
            SetAffinityVelocity(Affinity.Water);
        }

        private IEnumerator StartMagic()
        {
            OnStartMagicBegin();
            yield return OnMagic();
            OnStartMagicEnd();
        }

        private void OnStartMagicBegin()
        {
            isMagicPlaying = true;

            SetParticleSize();
            SetParticleVelocity();
            Particles.Play();
        }

        private void OnStartMagicEnd()
        {
            isMagicPlaying = false;

            Particles.Stop();

            collector.EmptyColliderCache(Particles);
        }

        protected virtual IEnumerator OnMagic()
        {
            yield return new WaitForSeconds(MagicStats.MagicDuration);
        }

        #endregion

        private InputAction GetSkillInputAction(SkillNumber number)
        {
            switch (number)
            {
                default:
                    return magicController.magicInput.Magic.Skill1;
                case SkillNumber.Skill2:
                    return magicController.magicInput.Magic.Skill2;
                case SkillNumber.Skill3:
                    return magicController.magicInput.Magic.Skill3;
                case SkillNumber.Skill4:
                    return magicController.magicInput.Magic.Skill4;
            }
        }
        private int GetSkillNumber(SkillNumber number)
        {
            switch (number)
            {
                default:
                    return 0;
                case SkillNumber.Skill2:
                    return 1;
                case SkillNumber.Skill3:
                    return 2;
                case SkillNumber.Skill4:
                    return 3;
            }
        }
    }
}