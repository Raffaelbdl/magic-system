using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MagicSystem
{
    public class MagicAttack : MagicEmetter
    {
        public MagicAttack() : base()
        {
            MagicType = MagicType.MagicAttack;
        }

        protected override void Awake()
        {
            base.Awake();
            collector = new MagicCollectorOnConeRays(this, 10, 0.1f, 45f);
        }
    }
}