using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "SO/Character/Enemy")]
public class EnemyDataSO : CharacterDataSO
{
    public float walkSpeed;
    public int damage;

    [Header("Enemy AI Configurations")]
    public float runSpeed;
    public float attackRange;
}
