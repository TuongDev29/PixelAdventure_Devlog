using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataSO", menuName = "SO/Item")]
public class ItemDataSO : ScriptableObject
{
    [Header("Character Stats")]
    public string itemName;
    public EItemType itemType;
    public string description;
    public int maxSlot;
}
