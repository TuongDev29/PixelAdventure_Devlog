using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBehaviorConfig", menuName = "SO/Config/Enemy")]
public class EnemyBehaviorConfig : ScriptableObject
{
    public float patrolWaitTime;
    public float detectionRange;
}
