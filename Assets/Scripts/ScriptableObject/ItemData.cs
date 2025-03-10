using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Consumable,
    InfinityConsumable,
    Equipable
}

public enum ConsumableType
{
    SpeedUp,
    JumpPower,
    God,
    Random,
    Heal
}

[Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
    public float duration;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

}
