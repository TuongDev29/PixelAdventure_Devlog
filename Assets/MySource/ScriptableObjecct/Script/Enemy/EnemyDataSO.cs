using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "SO/Character/Enemy")]
public class EnemyDataSO : CharacterDataSO
{
    public float moveSpeed;
    public int damage;

    [Header("Behavior Config")]
    public float patrolWaitTime;
    public float detectionRange;
    public float detectionHeightRange;
}
