using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/new Active Item")]
public class ActiveItemSO : ItemSO
{
    public GameObject attackPrefab;
    public GameObject weaponSystem;
    public Vector3 attackOffset;

    public List<WeaponStats> levelStats = new List<WeaponStats>();

    public WeaponSystem InitWeapon(Entity _bearer)
    {
        WeaponSystem system = Instantiate(weaponSystem, _bearer.weaponHolder.transform).GetComponent<WeaponSystem>();
        system.InitWeapon(this, 0, _bearer);

        return system;
    }
}