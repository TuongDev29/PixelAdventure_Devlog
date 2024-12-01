using UnityEngine;

public abstract class CharacterDataSO : ScriptableObject
{
    [Header("Character Stats")]
    public string CharacterName;
    public int maxHealth;
}
