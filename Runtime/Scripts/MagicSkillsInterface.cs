using System;
using System.Collections;
using System.Collections.Generic;
using MagicSystem;
using UnityEngine;

namespace MagicSystem
{
    public class MagicSkillsInterface : MonoBehaviour
    {
        public Action<MagicStats[], int[]> onMagicSkillsChange;

        public void MagicSkillChange(MagicStats[] magicStats, int[] valids)
        {
            onMagicSkillsChange?.Invoke(magicStats, valids);
        }
    }
}
