using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsManager : Singleton<GameSettingsManager>
{
    [Header("Damage Settings")]
    [Tooltip("Dot product threshold for determining if damage is dealt from 0 Deg to 90 Deg")]
    [Range(0, 1)] public float damageDotThreshold = 0.84f;
}
