using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataSO", menuName = "SO/Character/Player")]
public class PlayerDataSO : CharacterDataSO
{
    public float jumpForce;
    public int maxJump;
    public float moveSpeed;
    public int damage;
}
