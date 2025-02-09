using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsDrawer : MonoBehaviour
{
    [SerializeField] private List<ActiveItemSO> actifList = new List<ActiveItemSO>();
    [SerializeField] private List<PassifItemSO> passifList = new List<PassifItemSO>();

    [SerializeField] private WeaponPlaceholder placeholderPrefab;
    [SerializeField] private Transform ChoiceDisplayer;
    private const int MAX_CHOICES = 3;

    private void Awake()
    {
        GameManager.OnItemSelection += SetItemChoices;
    }

    private void OnDestroy()
    {
        GameManager.OnItemSelection -= SetItemChoices;
    }

    private List<ItemSO> GetAllAvailableItems()
    {
        List<ItemSO> availableItems = new List<ItemSO>();

        // Get all actifs items
        List<ActiveItemSO> availableActifs = GetAvailableActifs();

        foreach (ActiveItemSO item in availableActifs)
            availableItems.Add(item);

        // Get all passifs items
        List<PassifItemSO> availablePassifs = GetAvailablePassifs();

        foreach (PassifItemSO item in availablePassifs)
            availableItems.Add(item);

        return availableItems;
    }

    private List<ActiveItemSO> GetAvailableActifs()
    {
        return actifList
            .Where(element =>
            {
                // The player dont have the item and have enough space
                if (!ItemManager.actifList.ContainsKey(element) && ItemManager.actifList.Count < ItemManager.MAX_ACTIVE_COUNT)
                    return true;

                // Return true if the player can upgrade the item
                return ItemManager.actifList[element].level < element.levelStats.Count - 1;
            }).ToList();
    }

    private List<PassifItemSO> GetAvailablePassifs()
    {
        return passifList
            .Where(element =>
            {
                // The player dont have the item and have enough space
                if (!ItemManager.passifList.ContainsKey(element) && ItemManager.passifList.Count < ItemManager.MAX_PASSIVE_COUNT)
                    return true;

                // Return true if the player can upgrade the item
                return ItemManager.passifList[element] < element.maxLevel;
            }).ToList();
    }


    private void SetItemChoices()
    {
        List<ItemSO> availableWeapons = GetAllAvailableItems();
        availableWeapons = ShuffleList(availableWeapons);

        ReplaceWeaponChoices(availableWeapons);
    }

    /// <summary>
    /// Replace the choices in the displayer with new random ones.
    /// </summary>
    /// <param name="_availableChoices"></param>
    private void ReplaceWeaponChoices(List<ItemSO> _availableChoices)
    {
        // Remove old choices
        foreach (Transform child in ChoiceDisplayer)
            Destroy(child.gameObject);


        for (int choiceIndex = 0; choiceIndex < MAX_CHOICES; choiceIndex++)
        {
            // If there is no more choice, get out of the for
            if (_availableChoices.Count < choiceIndex + 1) break;

            ItemSO item = _availableChoices[choiceIndex];

            WeaponPlaceholder placeHolder = Instantiate(placeholderPrefab, ChoiceDisplayer);

            if (item is ActiveItemSO)
            {
                // Get the level zero of the wepon, if the player already have the weapon, get the next available level
                int weaponLevel = 0;
                if (ItemManager.actifList.ContainsKey((ActiveItemSO)item))
                    weaponLevel = ItemManager.actifList[(ActiveItemSO)item].level + 1;

                placeHolder.SetActif((ActiveItemSO)item, weaponLevel);
            }
            else if (item is PassifItemSO)
            {
                placeHolder.SetPassif((PassifItemSO)item);
            }
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