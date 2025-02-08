using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsDrawer : MonoBehaviour
{
    [SerializeField] private List<WeaponBaseDataSO> weaponsList = new List<WeaponBaseDataSO>();

    [SerializeField] private WeaponPlaceholder placeholderPrefab;
    [SerializeField] private Transform ChoiceDisplayer;
    private const int MAX_CHOICES = 3;

    private void Awake()
    {
        GameManager.OnItemSelection += SetWeaponChoices;
    }

    private void OnDestroy()
    {
        GameManager.OnItemSelection -= SetWeaponChoices;
    }

    private List<WeaponBaseDataSO> GetAvailableWeapons()
    {
        List<WeaponBaseDataSO> availableWeapons = weaponsList
            .Where(element =>
            {
                // The player dont have the weapon and have enough space
                if (!WeaponManager.weaponList.ContainsKey(element) && WeaponManager.weaponList.Count < WeaponManager.MAX_WEAPON_COUNT)
                    return true;

                // Return true if the player can upgrade the weapon
                return WeaponManager.weaponList[element].level < element.levelStats.Count - 1;
            }).ToList();

        return availableWeapons;
    }

    private void SetWeaponChoices()
    {
        List<WeaponBaseDataSO> availableWeapons = GetAvailableWeapons();
        availableWeapons = ShuffleList(availableWeapons);

        ReplaceWeaponChoices(availableWeapons);
    }

    /// <summary>
    /// Replace the choices in the displayer with new random ones.
    /// </summary>
    /// <param name="_availableChoices"></param>
    private void ReplaceWeaponChoices(List<WeaponBaseDataSO> _availableChoices)
    {
        // Remove old choices
        foreach (Transform child in ChoiceDisplayer)
            Destroy(child.gameObject);


        for (int weaponIndex = 0; weaponIndex < MAX_CHOICES; weaponIndex++)
        {
            // If there is no more choice, get out of the for
            if (_availableChoices.Count < weaponIndex + 1) break;

            WeaponPlaceholder placeHolder = Instantiate(placeholderPrefab, ChoiceDisplayer);

            // Get the level zero of the wepon, if the player already have the weapon, get the next available level
            int weaponLevel = 0;
            if (WeaponManager.weaponList.ContainsKey(_availableChoices[weaponIndex]))
                weaponLevel = WeaponManager.weaponList[_availableChoices[weaponIndex]].level + 1;

            placeHolder.SetWeapon(_availableChoices[weaponIndex], weaponLevel);
        }
    }

    /// <summary>
    /// Shuffle a given list of any type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_listToShuffle"></param>
    /// <returns></returns>
    private List<T> ShuffleList<T>(List<T> _listToShuffle)
    {
        for (int indexInList = _listToShuffle.Count - 1; indexInList >= 0; indexInList--)
        {
            int randomIndex = UnityEngine.Random.Range(0, indexInList + 1);
            var temp = _listToShuffle[indexInList];
            _listToShuffle[indexInList] = _listToShuffle[randomIndex];
            _listToShuffle[randomIndex] = temp;
        }

        return _listToShuffle;
    }
}