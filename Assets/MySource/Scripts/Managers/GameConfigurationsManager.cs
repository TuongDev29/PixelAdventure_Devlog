using UnityEngine;
using DevLog;

namespace DevLog
{
    public class GameConfigurationsManager : Singleton<GameSettingsManager>
    {
        [Header("Damage Configs")]

        [Tooltip("Dot product threshold for determining if damage is dealt from 0 Deg to 180 Deg")]
        [Range(0, 1)] public float damageDotThreshold = 0.72f;
    }
}
