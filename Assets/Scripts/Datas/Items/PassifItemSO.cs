using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/new Passive Item")]
public class PassifItemSO : ItemSO
{
    public int maxLevel;

    public EntityStats levelStats;

    public string description;
}